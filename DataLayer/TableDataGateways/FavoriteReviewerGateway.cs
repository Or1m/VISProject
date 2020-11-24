
namespace DataLayer.TableDataGateways
{
    public class FavoriteReviewerGateway
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Favorit_reviewer (user_user_id, reviewer_reviewer_id) VALUES (:user_user_id, :reviewer_reviewer_id)";

        public static string SQL_DELETE = "DELETE FROM Favorit_reviewer WHERE user_user_id=:user_user_id and reviewer_reviewer_id=:reviewer_reviewer_id";

        // Methods
        public int insertNew(int userId, int reviewer_id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewer_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int delete(int userId, int reviewer_id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":reviewer_reviewer_id", reviewer_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
    }
}
