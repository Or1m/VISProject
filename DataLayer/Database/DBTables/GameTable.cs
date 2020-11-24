using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDBS;
using UDBS.Oracle;
using UDBS.Proxy;

namespace DaisORM.UDBS.Oracle
{
    public class GameTable
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Game (name, description, developer, rating, release_date, average_user_review, average_reviewer_score) "
            + " VALUES (:name, :description, :developer, :rating, :release_date, :average_user_review, :average_reviewer_score)";

        public static string SQL_DELETE_ID = "DELETE FROM Game WHERE game_id=:game_id";

        public static string SQL_UPDATE = "UPDATE Game SET game_id=:game_id, name=:name, description=:description," +
            "developer=:developer, rating=:rating, release_date=:release_date," +
            "average_user_review=:average_user_review, average_reviewer_score=:average_reviewer_score WHERE game_id=:game_id";

        public static string SQL_SELECT_ALL_HEADERS = "SELECT game_id, name, developer from Game";

        public static string SQL_SELECT_GAME_BY_ID = "SELECT game_id, name, description, developer, rating, release_date, average_user_review, average_reviewer_score from Game where game_id=:game_id";

        public static string SQL_SELECT_GAMES_BY_NAME = "SELECT game_id, name, description, developer, rating, release_date, average_user_review, average_reviewer_score from Game where name=:name";

        public static string SQL_SELECT_GAMES_BY_DEVELOPER_HEADER = "SELECT game_id, name, developer from Game where developer=:developer";

        public static string SQL_SELECT_FAVORIT_GAMES_FOR_USER = "SELECT game_id, name, developer FROM Game g JOIN Favorit_game fg ON g.game_id = fg.game_game_id WHERE user_user_id=:user_id";

        static string SQL_SELECT_GAMES_WITH_CATEGORIES = "SELECT g.game_id, g.name, g.developer, c.category_id, c.name FROM Game g " +
            "JOIN game_category gc ON g.game_id = gc.game_game_id JOIN category c ON c.category_id = gc.category_category_id";

        // Methods
        public int insertNewGame(Game game)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, game);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int updateGame(Game game)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, game);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int deleteGameById(int id)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue(":game_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public List<Game> selectGames(DatabaseProxy pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL_HEADERS);
            OracleDataReader reader = db.Select(command);

            List<Game> games = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return games;
        }

        public List<Game> selectFavoritGames(int userId, DatabaseProxy pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_FAVORIT_GAMES_FOR_USER);
            command.Parameters.AddWithValue(":user_id", userId);
            OracleDataReader reader = db.Select(command);

            List<Game> games = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return games;
        }

        public Game selectGame(int id, DatabaseProxy pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_GAME_BY_ID);
            command.Parameters.AddWithValue(":game_id", id);
            OracleDataReader reader = db.Select(command);

            List<Game> games = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return games.ElementAt(0);
        }

        public List<Game> selectGamesByName(string name, DatabaseProxy pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_GAMES_BY_NAME);
            command.Parameters.AddWithValue(":name", name);
            OracleDataReader reader = db.Select(command);

            List<Game> games = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return games;
        }

        public List<Game> selectGamesByDeveloper(string developer, DatabaseProxy pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_GAMES_BY_DEVELOPER_HEADER);
            command.Parameters.AddWithValue(":developer", developer);
            OracleDataReader reader = db.Select(command);

            List<Game> games = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return games;
        }

        public List<Game> selectGamesWithCategories(DatabaseProxy pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_GAMES_WITH_CATEGORIES);
            OracleDataReader reader = db.Select(command);

            List<Game> games = ReadWithCategories(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return games;
        }

        public static List<Game> ReadWithCategories(OracleDataReader reader)
        {
            var categoryDict = new Dictionary<int, Game>();

            while (reader.Read())
            {
                int gameId = int.Parse(reader["game_id"].ToString());

                if (!categoryDict.ContainsKey(gameId))
                {
                    categoryDict.Add(gameId, new Game { Game_id = gameId, Name = reader["name"].ToString(), Developer = reader["developer"].ToString() });
                }

                if (categoryDict.TryGetValue(gameId, out var game))
                {
                    int i = 2;
                    game.Categories.Add(new Category
                    {
                        Category_id = reader.GetInt32(++i),
                        Name = reader.GetString(++i)
                    });
                }
            }

            return categoryDict.Select(c => c.Value).ToList();
        }

        private static void PrepareCommand(OracleCommand command, Game game)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":game_id", game.Game_id);
            command.Parameters.AddWithValue(":name", game.Name);
            command.Parameters.AddWithValue(":description", game.Description);
            command.Parameters.AddWithValue(":developer", game.Developer);
            command.Parameters.AddWithValue(":rating", game.Rating);
            command.Parameters.AddWithValue(":release_date", game.Release_date == null ? DBNull.Value : (object)game.Release_date);
            command.Parameters.AddWithValue(":average_user_review", game.Average_user_review == null ? DBNull.Value : (object)game.Average_user_review);
            command.Parameters.AddWithValue(":average_reviewer_score", game.Average_reviewer_score == null ? DBNull.Value : (object)game.Average_reviewer_score);
        }

        private static List<Game> ReadHeader(OracleDataReader reader)
        {
            List<Game> games = new List<Game>();

            while (reader.Read())
            {
                int i = -1;
                Game game = new Game();
                game.Game_id = reader.GetInt32(++i);
                game.Name = reader.GetString(++i);
                game.Developer = reader.GetString(++i);
                
                games.Add(game);
            }
            return games;
        }
        private static List<Game> Read(OracleDataReader reader)
        {
            List<Game> games = new List<Game>();

            while (reader.Read())
            {
                int i = -1;
                Game game = new Game();
                game.Game_id = reader.GetInt32(++i);
                game.Name = reader.GetString(++i);
                game.Description = reader.GetString(++i);
                game.Developer = reader.GetString(++i);
                game.Rating = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    game.Release_date = reader.GetDateTime(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    game.Average_user_review = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    game.Average_reviewer_score = reader.GetInt32(i);
                }

                games.Add(game);
            }
            return games;
        }
    }
}
