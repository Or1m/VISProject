using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class GameGateway
    {
        #region SQL Commands
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

        public static string SQL_SELECT_FAVORITE_GAMES_FOR_USER = "SELECT game_id, name, developer FROM Game g JOIN Favorit_game fg ON g.game_id = fg.game_game_id WHERE user_user_id=:user_id";

        static string SQL_SELECT_GAMES_WITH_CATEGORIES = "SELECT g.game_id, g.name, g.developer, c.category_id, c.name FROM Game g " +
            "JOIN game_category gc ON g.game_id = gc.game_game_id JOIN category c ON c.category_id = gc.category_category_id";
        #endregion

        private static readonly object lockObj = new object();
        private static GameGateway instance;

        public static GameGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new GameGateway());
                }
            }
        }

        private GameGateway() { }


        #region Non Query Methods
        public int Insert(GameDTO game)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, game);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Update(GameDTO game)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, game);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int DeleteById(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue(":game_id", id);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public List<GameDTO> SelectGames()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_HEADERS);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadHeader(reader);
        }

        public List<GameDTO> SelectFavoriteGames(int userId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_FAVORITE_GAMES_FOR_USER);
            command.Parameters.AddWithValue(":user_id", userId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadHeader(reader);
        }

        public GameDTO SelectGame(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_GAME_BY_ID);
            command.Parameters.AddWithValue(":game_id", id);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader).ElementAt(0);
        }

        public List<GameDTO> SelectGamesByName(string name)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_GAMES_BY_NAME);
            command.Parameters.AddWithValue(":name", name);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }

        public List<GameDTO> SelectGamesByDeveloper(string developer)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_GAMES_BY_DEVELOPER_HEADER);
            command.Parameters.AddWithValue(":developer", developer);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadHeader(reader);
        }

        public List<GameDTO> SelectGamesWithCategories()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_GAMES_WITH_CATEGORIES);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadWithCategories(reader);
        }
        #endregion

        #region Helpers
        public static List<GameDTO> ReadWithCategories(SqlDataReader reader)
        {
            var categoryDict = new Dictionary<int, GameDTO>();

            while (reader.Read())
            {
                int gameId = int.Parse(reader["game_id"].ToString());

                if (!categoryDict.ContainsKey(gameId))
                {
                    categoryDict.Add(gameId, new GameDTO { Id = gameId, Name = reader["name"].ToString(), Developer = reader["developer"].ToString() });
                }

                if (categoryDict.TryGetValue(gameId, out var game))
                {
                    int i = 2;
                    game.Categories.Add(new CategoryDTO
                    {
                        CategoryId = reader.GetInt32(++i),
                        Name = reader.GetString(++i)
                    });
                }
            }

            return categoryDict.Select(c => c.Value).ToList();
        }

        private static void PrepareCommand(SqlCommand command, GameDTO game)
        {
            command.Parameters.AddWithValue(":game_id", game.Id);
            command.Parameters.AddWithValue(":name", game.Name);
            command.Parameters.AddWithValue(":description", game.Description);
            command.Parameters.AddWithValue(":developer", game.Developer);
            command.Parameters.AddWithValue(":rating", game.Rating);
            command.Parameters.AddWithValue(":release_date", game.ReleaseDate == null ? DBNull.Value : (object)game.ReleaseDate);
            command.Parameters.AddWithValue(":average_user_review", game.AverageUserScore == null ? DBNull.Value : (object)game.AverageUserScore);
            command.Parameters.AddWithValue(":average_reviewer_score", game.AverageReviewerScore == null ? DBNull.Value : (object)game.AverageReviewerScore);
        }

        private static List<GameDTO> ReadHeader(SqlDataReader reader)
        {
            List<GameDTO> games = new List<GameDTO>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        GameDTO game = new GameDTO();
                        game.Id = reader.GetInt32(++i);
                        game.Name = reader.GetString(++i);
                        game.Developer = reader.GetString(++i);

                        games.Add(game);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return games;
        }
        private static List<GameDTO> Read(SqlDataReader reader)
        {
            List<GameDTO> games = new List<GameDTO>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        GameDTO game = new GameDTO();
                        game.Id = reader.GetInt32(++i);
                        game.Name = reader.GetString(++i);
                        game.Description = reader.GetString(++i);
                        game.Developer = reader.GetString(++i);
                        game.Rating = reader.GetString(++i);
                        if (!reader.IsDBNull(++i))
                        {
                            game.ReleaseDate = reader.GetDateTime(i);
                        }
                        if (!reader.IsDBNull(++i))
                        {
                            game.AverageUserScore = reader.GetInt32(i);
                        }
                        if (!reader.IsDBNull(++i))
                        {
                            game.AverageReviewerScore = reader.GetInt32(i);
                        }

                        games.Add(game);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return games;
        }
        #endregion
    }
}
