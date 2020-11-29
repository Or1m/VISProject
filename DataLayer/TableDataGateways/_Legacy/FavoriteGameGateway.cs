﻿
using System.Data.SqlClient;

namespace DataLayer.TableDataGateways
{
    public class FavoriteGameGateway
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Favorite_game (user_user_id, game_game_id) VALUES (:user_user_id, :game_game_id)";

        public static string SQL_DELETE     = "DELETE FROM Favorite_game WHERE user_user_id=:user_user_id and game_game_id=:game_game_id";

        // Methods
        public int insertNew(int userId, int game_id)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", game_id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int delete(int userId, int game_id)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", game_id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }
    }
}