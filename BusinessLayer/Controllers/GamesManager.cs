using BusinessLayer.BusinessObjects;
using BusinessLayer.Enums;
using DataLayer.TableDataGateways;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Controllers
{
    public class GamesManager
    {
        #region Constructor & Singleton Pattern
        private static GamesManager instance = null;

        private static readonly object lockObj = new object();

        public List<Game> Games { get; }


        public static GamesManager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new GamesManager());
                }
            }
        }

        private GamesManager()
        {
            Games = new List<Game>();
        }
        #endregion

        #region Public Methods
        public List<Game> LoadGamesHeadersWithCategories()
        {
            var gamesDTO = GameGateway.Instance.SelectGamesWithCategories();

            foreach(var DTO in gamesDTO)
                Games.Add(new Game(DTO));

            return Games;
        }

        public Game LoadGame(int gameId)
        {
            return new Game(GameGateway.Instance.SelectGame(gameId));
        }

        public List<Category> LoadCategoriesForGame(int gameId)
        {
            return CategoryGateway.Instance.SelectCategoriesForGame(gameId).ConvertAll(c => new Category(c));
        }

        public EnCreateGame CreateAndInsert(string name, string developer, string rating, string date, string categories, string description, out int newId)
        {
            newId = -1;

            string[] tempRating = rating.Split(' ');
            if (tempRating[0] != "PEGI" || !IsInRatingNumbers(tempRating[1]))
                return EnCreateGame.invalidRatingFormat;

            string[] tempCat = categories.Split(',');

            if (!ValidCategories(tempCat))
                return EnCreateGame.invalidCategoriesFormat;

            List<Category> cats = new List<Category>();

            foreach(string c in tempCat)
            {
                cats.Add(new Category(c));
            }

            Game newGame = new Game(null, name, description, developer, rating, DateTime.Parse(date));
            newGame.Categories.AddRange(cats);

            if (GameGateway.Instance.SelectGamesByName(name).Count != 0)
                return EnCreateGame.alreadyInDB;

            newId = GameGateway.Instance.InsertWithCategories(newGame.ToDTO());

            return newId > 0 ? EnCreateGame.inserted : EnCreateGame.somethingWrong;
        }
        #endregion

        #region Private Methods
        private bool ValidCategories(string[] tempCat)
        {
            bool flag = true;
            foreach(string s in tempCat)
            {
                if (s != "Action" && s != "RPG" && s != "Strategy" && s != "Adventure" && s != "Shooter" && s != "Racing" && s != "Fighting")
                    flag = false;
            }

            return flag;
        }

        private bool IsInRatingNumbers(string numStr)
        {
            int num;
            if (!int.TryParse(numStr, out num))
                return false;
            else
            {
                if (num == 12 || num == 16 || num == 18 || num == 3 || num == 7)
                    return true;
                else
                    return false;
            }
        }
        #endregion
    }
}