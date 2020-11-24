
namespace DataLayer.TableDataGateways
{
    class GameCategoryGateway
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Game_category (game_game_id, category_category_id) VALUES (:game_game_id, :category_category_id)";

        public static string SQL_DELETE = "DELETE FROM Game_category WHERE game_game_id=:game_game_id and category_category_id=:category_category_id";

        // Methods
        public int insertNew(int game_game_id, int category_category_id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":game_game_id", game_game_id);
            command.Parameters.AddWithValue(":category_category_id", category_category_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int delete(int game_game_id, int category_category_id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue(":game_game_id", game_game_id);
            command.Parameters.AddWithValue(":category_category_id", category_category_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
    }
}
