using BusinessLayer.BusinessObjects;
using BusinessLayer.Enums;
using DataLayer.TableDataGateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Controllers
{
    public class GamesManager
    {
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


        public List<Game> LoadGamesHeadersWithCategories()
        {
            var gamesDTO = GameGateway.Instance.SelectGamesWithCategories();

            foreach(var DTO in gamesDTO)
                Games.Add(new Game(DTO));

            return Games;
        }

        public Game LoadGame(int gameIndex)
        {
            return new Game(GameGateway.Instance.SelectGame(gameIndex));
        }

        public EnCreateGame CreateAndInsert(string name, string developer, string rating, string date, string categories, string description)
        {
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

            return GameGateway.Instance.Insert(newGame.ToDTO()) == 1 ? EnCreateGame.inserted : EnCreateGame.somethingWrong;
        }

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
    }
}
