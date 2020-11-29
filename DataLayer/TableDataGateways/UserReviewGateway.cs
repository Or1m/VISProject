using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class UserReviewGateway : BaseGateway
    {
        public static string SQL_INSERT_NEW = "INSERT INTO User_review (title, score, user_user_id, game_game_id, \"date\", order_of_review) "
            + " VALUES (:title, :score, :user_user_id, :game_game_id, :datee, :order_of_review)";

        public static string SQL_DELETE = "DELETE FROM User_review WHERE user_user_id=:user_user_id and game_game_id=:game_game_id and order_of_review=:order_of_review";

        public static string SQL_SELECT_REVIEW = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review " +
            "WHERE user_user_id=:user_user_id and game_game_id=:game_game_id and order_of_review=:order_of_review";

        public static string SQL_SELECT_ALL = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review";

        public static string SQL_SELECT_ALL_BY_USER = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review WHERE user_user_id=:user_user_id";

        public static string SQL_SELECT_ALL_BY_GAME = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review WHERE game_game_id=:game_game_id";

        // Methods
        public int Insert(UserReviewDTO review)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, review);

            return ExecuteNonQuery(command);
        }

        public int delete(int userId, int gameId, int order)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public UserReviewDTO selectReview(int userId, int gameId, int order, IDatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_REVIEW);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            SqlDataReader reader = db.Select(command);

            List<UserReviewDTO> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews.ElementAt(0);
        }

        public List<UserReviewDTO> selectReviews(IDatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            List<UserReviewDTO> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews;
        }

        public List<UserReviewDTO> selectReviewsForUser(int userId, IDatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL_BY_USER);
            command.Parameters.AddWithValue(":user_user_id", userId);
            SqlDataReader reader = db.Select(command);

            List<UserReviewDTO> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews;
        }

        public List<UserReviewDTO> selectReviewsForGame(int gameId, IDatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL_BY_GAME);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            SqlDataReader reader = db.Select(command);

            List<UserReviewDTO> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews;
        }

        private static void PrepareCommand(SqlCommand command, UserReviewDTO review)
        {
            //command.Parameters.AddWithValue(":title", review.Title);
            //command.Parameters.AddWithValue(":score", review.Score);
            //command.Parameters.AddWithValue(":user_user_id", review.UserId);
            //command.Parameters.AddWithValue(":game_game_id", review.GameId);
            //command.Parameters.AddWithValue(":datee", review.Date);
            //command.Parameters.AddWithValue(":order_of_review", review.OrderOfReview);
        }

        private static List<UserReviewDTO> Read(SqlDataReader reader)
        {
            List<UserReviewDTO> User_reviews = new List<UserReviewDTO>();

            while (reader.Read())
            {
                int i = -1;
                //UserReviewDTO User_review = new UserReviewDTO();
                //User_review.Title = reader.GetString(++i);
                //User_review.Score = reader.GetInt32(++i);
                //User_review.UserId = reader.GetInt32(++i);
                //User_review.GameId = reader.GetInt32(++i);
                //User_review.Date = reader.GetDateTime(++i);
                //User_review.OrderOfReview = reader.GetInt32(++i);

                //User_reviews.Add(User_review);
            }
            return User_reviews;
        }
    }
}
