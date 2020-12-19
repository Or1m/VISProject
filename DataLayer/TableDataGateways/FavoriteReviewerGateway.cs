using System.Data.SqlClient;

namespace DataLayer.TableDataGateways
{
    public class FavoriteReviewerGateway
    {
        #region SQL Commands
        public static string SQL_INSERT = "INSERT INTO Favorite_reviewer (user_user_id, reviewer_reviewer_id) VALUES (@user_user_id, @reviewer_reviewer_id)";

        public static string SQL_DELETE = "DELETE FROM Favorite_reviewer WHERE user_user_id=@user_user_id and reviewer_reviewer_id=@reviewer_reviewer_id";
        #endregion


        private static readonly object lockObj = new object();
        private static FavoriteReviewerGateway instance;

        public static FavoriteReviewerGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new FavoriteReviewerGateway());
                }
            }
        }

        private FavoriteReviewerGateway() { }


        #region Non Query Methods
        public int Insert(int userId, int reviewer_id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewer_id);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int delete(int userId, int reviewer_id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewer_id);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion
    }
}
