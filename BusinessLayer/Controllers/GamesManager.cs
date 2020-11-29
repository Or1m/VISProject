using BusinessLayer.BusinessObjects;
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
    }
}
