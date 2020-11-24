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
    public class ReviewerReviewTable
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Reviewer_review (title, text_of_review, score, reviewer_reviewer_id, game_game_id, \"date\", order_of_review) "
            + " VALUES (:title, :text_of_review, :score, :reviewer_reviewer_id, :game_game_id, :datee, :order_of_review)";

        public static string SQL_DELETE = "DELETE FROM Reviewer_review WHERE reviewer_reviewer_id=:reviewer_reviewer_id and game_game_id=:game_game_id and order_of_review=:order_of_review";

        public static string SQL_SELECT_REVIEW = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, \"date\", order_of_review FROM Reviewer_review " +
            "WHERE reviewer_reviewer_id=:reviewer_reviewer_id and game_game_id=:game_game_id and order_of_review=:order_of_review";

        public static string SQL_SELECT_ALL = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, \"date\", order_of_review FROM Reviewer_review";

        public static string SQL_SELECT_ALL_BY_REVIEWER = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, \"date\", order_of_review FROM Reviewer_review " +
            "WHERE reviewer_reviewer_id=:reviewer_reviewer_id";

        public static string SQL_SELECT_ALL_BY_GAME = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, \"date\", order_of_review FROM Reviewer_review WHERE game_game_id=:game_game_id";


        // Methods
        public int insertNew(Reviewer_review review)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, review);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int delete(int reviewerId, int gameId, int order)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewerId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public Reviewer_review selectReview(int reviewerId, int gameId, int order, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewerId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            OracleDataReader reader = db.Select(command);

            List<Reviewer_review> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews.ElementAt(0);
        }

        public List<Reviewer_review> selectReviews(DatabaseProxy pDb = null)
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

            List<Reviewer_review> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews;
        }

        public List<Reviewer_review> selectReviewsForReviewer(int reviewerId, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL_BY_REVIEWER);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewerId);
            OracleDataReader reader = db.Select(command);

            List<Reviewer_review> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews;
        }

        public List<Reviewer_review> selectReviewsForGame(int gameId, DatabaseProxy pDb = null)
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

            List<Reviewer_review> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews;
        }

        private static void PrepareCommand(OracleCommand command, Reviewer_review Review)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":title", Review.Title);
            command.Parameters.AddWithValue(":text_of_review", Review.Text_of_review);
            command.Parameters.AddWithValue(":score", Review.Score);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", Review.ReviewerId);
            command.Parameters.AddWithValue(":game_game_id", Review.GameId);
            command.Parameters.AddWithValue(":datee", Review.Date);
            command.Parameters.AddWithValue(":order_of_review", Review.Order_of_review);
        }

        private static List<Reviewer_review> Read(OracleDataReader reader)
        {
            List<Reviewer_review> Reviewer_reviews = new List<Reviewer_review>();

            while (reader.Read())
            {
                int i = -1;
                Reviewer_review reviewer_review = new Reviewer_review();
                reviewer_review.Title = reader.GetString(++i);
                reviewer_review.Text_of_review = reader.GetString(++i);
                reviewer_review.Score = reader.GetInt32(++i);
                reviewer_review.ReviewerId = reader.GetInt32(++i);
                reviewer_review.GameId = reader.GetInt32(++i);
                reviewer_review.Date = reader.GetDateTime(++i);
                reviewer_review.Order_of_review = reader.GetInt32(++i);

                Reviewer_reviews.Add(reviewer_review);
            }
            return Reviewer_reviews;
        }
    }
}
