using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class UserReviewGateway
    {
        #region SQL Commands
        public static string SQL_INSERT_NEW = "INSERT INTO User_review (title, score, user_user_id, game_game_id, \"date\", order_of_review) "
            + " VALUES (:title, :score, :user_user_id, :game_game_id, :datee, :order_of_review)";

        public static string SQL_DELETE = "DELETE FROM User_review WHERE user_user_id=:user_user_id and game_game_id=:game_game_id and order_of_review=:order_of_review";

        public static string SQL_SELECT_REVIEW = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review " +
            "WHERE user_user_id=:user_user_id and game_game_id=:game_game_id and order_of_review=:order_of_review";

        public static string SQL_SELECT_ALL = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review";

        public static string SQL_SELECT_ALL_BY_USER = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review WHERE user_user_id=:user_user_id";

        public static string SQL_SELECT_ALL_BY_GAME = "SELECT title, score, user_user_id, game_game_id, \"date\", order_of_review FROM User_review WHERE game_game_id=:game_game_id";
        #endregion

        #region Non Query Methods
        public int Insert(UserReviewDTO review)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT_NEW);

            command.Parameters.AddWithValue(":title", review.Title);
            command.Parameters.AddWithValue(":score", review.Score);
            command.Parameters.AddWithValue(":user_user_id", review.Id);
            command.Parameters.AddWithValue(":game_game_id", review.Id);
            command.Parameters.AddWithValue(":datee", review.Date);
            command.Parameters.AddWithValue(":order_of_review", review.OrderOfReview);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int delete(int userReviewId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue(":user_review_id", userReviewId);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public UserReviewDTO selectReview(int userReviewId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_REVIEW);
            command.Parameters.AddWithValue(":user_review_id", userReviewId);

            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader).ElementAt(0);
        }

        public List<UserReviewDTO> selectReviews()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }

        public List<UserReviewDTO> selectReviewsForUser(int userId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_BY_USER);
            command.Parameters.AddWithValue(":user_user_id", userId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }

        public List<UserReviewDTO> selectReviewsForGame(int gameId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_BY_GAME);
            command.Parameters.AddWithValue(":game_game_id", gameId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }
        #endregion

        #region Helpers
        private static List<UserReviewDTO> Read(SqlDataReader reader)
        {
            List<UserReviewDTO> UserReviews = new List<UserReviewDTO>();

            try
            {
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        UserReviewDTO UserReview = new UserReviewDTO();
                        UserReview.Title = reader.GetString(++i);
                        UserReview.Score = reader.GetInt32(++i);
                        UserReview.Id = reader.GetInt32(++i);
                        UserReview.Id = reader.GetInt32(++i);
                        UserReview.Date = reader.GetDateTime(++i);
                        UserReview.OrderOfReview = reader.GetInt32(++i);

                        UserReviews.Add(UserReview);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            
            return UserReviews;
        }
        #endregion
    }
}
