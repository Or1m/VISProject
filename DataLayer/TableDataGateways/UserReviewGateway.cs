using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class UserReviewGateway
    {
        #region SQL Commands
        private static string SQL_INSERT_NEW         = "INSERT INTO User_review (title, score, user_user_id, game_game_id, date, order_of_review) " +
                                                       " VALUES (@title, @score, @user_user_id, @game_game_id, @datee, @order_of_review)";

        private static string SQL_DELETE             = "DELETE FROM User_review WHERE user_review_id=@user_review_id";

        private static string SQL_SELECT_REVIEW      = "SELECT title, score, user_user_id, game_game_id, date, order_of_review FROM User_review " +
                                                       "WHERE user_review_id=@user_review_id";

        private static string SQL_SELECT_ALL         = "SELECT title, score, user_user_id, game_game_id, date, order_of_review FROM User_review";

        private static string SQL_SELECT_ALL_BY_USER = "SELECT title, score, user_user_id, game_game_id, date, order_of_review FROM User_review WHERE user_user_id=@user_user_id";

        private static string SQL_SELECT_ALL_BY_GAME = "SELECT title, score, user_user_id, game_game_id, date, order_of_review FROM User_review WHERE game_game_id=@game_game_id";
        #endregion


        private static readonly object lockObj = new object();
        private static UserReviewGateway instance;

        public static UserReviewGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new UserReviewGateway());
                }
            }
        }

        private UserReviewGateway() { }


        #region Non Query Methods
        public int Insert(UserReviewDTO review)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT_NEW);
            PrepareCommand(command, review);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Delete(int userReviewId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE);

            command.Parameters.AddWithValue("@user_review_id", userReviewId);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public UserReviewDTO SelectReview(int userReviewId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_REVIEW);
            command.Parameters.AddWithValue("@user_review_id", userReviewId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader).ElementAt(0);
        }

        public List<UserReviewDTO> SelectReviews()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader);
        }

        public List<UserReviewDTO> SelectReviewsForUser(int userId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_BY_USER);
            command.Parameters.AddWithValue("@user_user_id", userId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

            return Read(reader);
        }

        public List<UserReviewDTO> SelectReviewsForGame(int gameId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_BY_GAME);
            command.Parameters.AddWithValue("@game_game_id", gameId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            command.Dispose();

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
                DatabaseConnection.Instance.Close();
            }
            
            return UserReviews;
        }

        private static void PrepareCommand(SqlCommand command, UserReviewDTO review)
        {
            command.Parameters.AddWithValue("@title", review.Title);
            command.Parameters.AddWithValue("@score", review.Score);
            command.Parameters.AddWithValue("@user_user_id", review.Id);
            command.Parameters.AddWithValue("@game_game_id", review.Id);
            command.Parameters.AddWithValue("@datee", review.Date);
            command.Parameters.AddWithValue("@order_of_review", review.OrderOfReview);
        }
        #endregion
    }
}