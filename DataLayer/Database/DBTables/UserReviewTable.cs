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
    public class UserReviewTable
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
        public int insertNew(User_review review)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, review);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int delete(int userId, int gameId, int order)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public User_review selectReview(int userId, int gameId, int order, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_REVIEW);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            OracleDataReader reader = db.Select(command);

            List<User_review> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews.ElementAt(0);
        }

        public List<User_review> selectReviews(DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
            OracleDataReader reader = db.Select(command);

            List<User_review> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews;
        }

        public List<User_review> selectReviewsForUser(int userId, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL_BY_USER);
            command.Parameters.AddWithValue(":user_user_id", userId);
            OracleDataReader reader = db.Select(command);

            List<User_review> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews;
        }

        public List<User_review> selectReviewsForGame(int gameId, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL_BY_GAME);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            OracleDataReader reader = db.Select(command);

            List<User_review> User_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User_reviews;
        }

        private static void PrepareCommand(OracleCommand command, User_review Review)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":title", Review.Title);
            command.Parameters.AddWithValue(":score", Review.Score);
            command.Parameters.AddWithValue(":user_user_id", Review.UserId);
            command.Parameters.AddWithValue(":game_game_id", Review.GameId);
            command.Parameters.AddWithValue(":datee", Review.Date);
            command.Parameters.AddWithValue(":order_of_review", Review.Order_of_review);
        }

        private static List<User_review> Read(OracleDataReader reader)
        {
            List<User_review> User_reviews = new List<User_review>();

            while (reader.Read())
            {
                int i = -1;
                User_review User_review = new User_review();
                User_review.Title = reader.GetString(++i);
                User_review.Score = reader.GetInt32(++i);
                User_review.UserId = reader.GetInt32(++i);
                User_review.GameId = reader.GetInt32(++i);
                User_review.Date = reader.GetDateTime(++i);
                User_review.Order_of_review = reader.GetInt32(++i);

                User_reviews.Add(User_review);
            }
            return User_reviews;
        }
    }
}
