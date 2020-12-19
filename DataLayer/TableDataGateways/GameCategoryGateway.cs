using System.Data.SqlClient;

namespace DataLayer.TableDataGateways
{
    class GameCategoryGateway
    {
        #region SQL Commands
        public static string SQL_INSERT = "INSERT INTO Game_category (game_game_id, category_category_id) VALUES (@game_game_id, @category_category_id)";

        public static string SQL_DELETE = "DELETE FROM Game_category WHERE game_game_id=@game_game_id and category_category_id=@category_category_id";
        #endregion


        private static readonly object lockObj = new object();
        private static GameCategoryGateway instance;

        public static GameCategoryGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new GameCategoryGateway());
                }
            }
        }

        private GameCategoryGateway() { }


        #region Non Query Methods
        public int Insert(int gameId, int categoryId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@game_game_id", gameId);
            command.Parameters.AddWithValue("@category_category_id", categoryId);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Delete(int gameId, int categoryId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue("@game_game_id", gameId);
            command.Parameters.AddWithValue("@category_category_id", categoryId);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion
    }
}
