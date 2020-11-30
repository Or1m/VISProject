using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class ReviewerReviewGateway
    {
        #region SQL Commands
        private static string SQL_INSERT                 = "INSERT INTO Reviewer_review (title, text_of_review, score, reviewer_reviewer_id, game_game_id, date, order_of_review) " +
                                                           " VALUES (@title, @text_of_review, @score, @reviewer_reviewer_id, @game_game_id, @datee, @order_of_review)";

        private static string SQL_DELETE                 = "DELETE FROM Reviewer_review WHERE reviewer_review_id=@reviewer_review_id";

        private static string SQL_SELECT_REVIEW          = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, date, order_of_review FROM Reviewer_review " +
                                                           "WHERE reviewer_review_id=@reviewer_review_id";

        private static string SQL_SELECT_ALL             = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, date, order_of_review FROM Reviewer_review";

        private static string SQL_SELECT_ALL_BY_REVIEWER = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, date, order_of_review FROM Reviewer_review " +
                                                           "WHERE reviewer_reviewer_id=@reviewer_reviewer_id";

        private static string SQL_SELECT_ALL_BY_GAME     = "SELECT title, text_of_review, score, reviewer_reviewer_id, game_game_id, date, order_of_review FROM Reviewer_review WHERE game_game_id=@game_game_id";
        #endregion


        private static readonly object lockObj = new object();
        private static ReviewerReviewGateway instance;

        public static ReviewerReviewGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ReviewerReviewGateway());
                }
            }
        }

        private ReviewerReviewGateway() { }


        #region Non Query Methods
        public int Insert(ReviewerReviewDTO review)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT);
            PrepareCommand(command, review);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Delete(int reviewerReviewId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue("@reviewer_review_id", reviewerReviewId);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public ReviewerReviewDTO SelectReview(int reviewerReviewId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_REVIEW);
            command.Parameters.AddWithValue("@reviewer_review_id", reviewerReviewId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader).ElementAt(0);
        }

        public List<ReviewerReviewDTO> SelectReviews()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader);
        }

        public List<ReviewerReviewDTO> SelectReviewsForReviewer(int reviewerId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_BY_REVIEWER);
            command.Parameters.AddWithValue("@reviewer_reviewer_id", reviewerId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader);
        }

        public List<ReviewerReviewDTO> SelectReviewsForGame(int gameId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_BY_GAME);
            command.Parameters.AddWithValue("@game_game_id", gameId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader);
        }
        #endregion

        #region Helpers
        private static void PrepareCommand(SqlCommand command, ReviewerReviewDTO review)
        {
            command.Parameters.AddWithValue("@title", review.Title);
            command.Parameters.AddWithValue("@text_of_review", review.TextOfReview);
            command.Parameters.AddWithValue("@score", review.Score);
            command.Parameters.AddWithValue("@reviewer_reviewer_id", review.Id);
            command.Parameters.AddWithValue("@game_game_id", review.Id);
            command.Parameters.AddWithValue("@datee", review.Date);
            command.Parameters.AddWithValue("@order_of_review", review.OrderOfReview);
        }

        private static List<ReviewerReviewDTO> Read(SqlDataReader reader)
        {
            List<ReviewerReviewDTO> ReviewerReviews = new List<ReviewerReviewDTO>();

            try
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        ReviewerReviewDTO reviewerReview = new ReviewerReviewDTO();
                        reviewerReview.Title = reader.GetString(++i);
                        reviewerReview.TextOfReview = reader.GetString(++i);
                        reviewerReview.Score = reader.GetInt32(++i);
                        reviewerReview.Id = reader.GetInt32(++i);
                        reviewerReview.Id = reader.GetInt32(++i);
                        reviewerReview.Date = reader.GetDateTime(++i);
                        reviewerReview.OrderOfReview = reader.GetInt32(++i);

                        ReviewerReviews.Add(reviewerReview);
                    }
                }
            }
            finally
            {
                reader.Close();
                DatabaseConnection.Instance.Close();
            }
            
            return ReviewerReviews;
        }
        #endregion
    }
}