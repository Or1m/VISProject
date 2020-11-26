
using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class ReviewerReviewGateway
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
        public int insertNew(ReviewerReviewDTO review)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, review);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int delete(int reviewerId, int gameId, int order)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewerId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public ReviewerReviewDTO selectReview(int reviewerId, int gameId, int order, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewerId);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            command.Parameters.AddWithValue(":order_of_review", order);

            SqlDataReader reader = db.Select(command);

            List<ReviewerReviewDTO> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews.ElementAt(0);
        }

        public List<ReviewerReviewDTO> selectReviews(DatabaseProxy pDb = null)
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

            List<ReviewerReviewDTO> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews;
        }

        public List<ReviewerReviewDTO> selectReviewsForReviewer(int reviewerId, DatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL_BY_REVIEWER);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewerId);
            SqlDataReader reader = db.Select(command);

            List<ReviewerReviewDTO> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews;
        }

        public List<ReviewerReviewDTO> selectReviewsForGame(int gameId, DatabaseProxy pDb = null)
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

            List<ReviewerReviewDTO> Reviewer_reviews = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return Reviewer_reviews;
        }

        private static void PrepareCommand(SqlCommand command, ReviewerReviewDTO review)
        {
            //command.Parameters.AddWithValue(":title", review.Title);
            //command.Parameters.AddWithValue(":text_of_review", review.TextOfReview);
            //command.Parameters.AddWithValue(":score", review.Score);
            //command.Parameters.AddWithValue(":reviewer_reviewer_id", review.ReviewerId);
            //command.Parameters.AddWithValue(":game_game_id", review.GameId);
            //command.Parameters.AddWithValue(":datee", review.Date);
            //command.Parameters.AddWithValue(":order_of_review", review.OrderOfReview);
        }

        private static List<ReviewerReviewDTO> Read(SqlDataReader reader)
        {
            List<ReviewerReviewDTO> Reviewer_reviews = new List<ReviewerReviewDTO>();

            while (reader.Read())
            {
                //int i = -1;
                //ReviewerReviewDTO reviewer_review = new ReviewerReviewDTO();
                //reviewer_review.Title = reader.GetString(++i);
                //reviewer_review.TextOfReview = reader.GetString(++i);
                //reviewer_review.Score = reader.GetInt32(++i);
                //reviewer_review.ReviewerId = reader.GetInt32(++i);
                //reviewer_review.GameId = reader.GetInt32(++i);
                //reviewer_review.Date = reader.GetDateTime(++i);
                //reviewer_review.OrderOfReview = reader.GetInt32(++i);

                //Reviewer_reviews.Add(reviewer_review);
            }
            return Reviewer_reviews;
        }
    }
}
