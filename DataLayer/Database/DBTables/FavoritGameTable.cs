using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDBS.Oracle;

namespace DaisORM.UDBS.Oracle
{
    public class FavoritGameTable
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Favorit_game (user_user_id, game_game_id) VALUES (:user_user_id, :game_game_id)";

        public static string SQL_DELETE = "DELETE FROM Favorit_game WHERE user_user_id=:user_user_id and game_game_id=:game_game_id";

        // Methods
        public int insertNew(int userId, int game_id)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", game_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int delete(int userId, int game_id)
        {
            Database db = new Database();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue(":user_user_id", userId);
            command.Parameters.AddWithValue(":game_game_id", game_id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
    }
}
