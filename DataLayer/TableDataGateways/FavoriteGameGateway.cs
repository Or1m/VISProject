
using System.Data.SqlClient;

namespace DataLayer.TableDataGateways
{
    public class FavoriteGameGateway
    {
        #region SQL Commands
        public static string SQL_INSERT     = "INSERT INTO Favorite_game (user_user_id, game_game_id) VALUES (@user_user_id, @game_game_id)";

        public static string SQL_DELETE     = "DELETE FROM Favorite_game WHERE user_user_id=@user_user_id and game_game_id=@game_game_id";
        #endregion


        private static readonly object lockObj = new object();
        private static FavoriteGameGateway instance;

        public static FavoriteGameGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new FavoriteGameGateway());
                }
            }
        }

        private FavoriteGameGateway() { }


        #region Non Query Methods
        public int Insert(int userId, int game_id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@user_user_id", userId);
            command.Parameters.AddWithValue("@game_game_id", game_id);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Delete(int userId, int game_id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue("@user_user_id", userId);
            command.Parameters.AddWithValue("@game_game_id", game_id);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion
    }
}