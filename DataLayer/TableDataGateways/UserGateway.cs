using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class UserGateway
    {
        public static string SQL_REGISTER_NEW = "INSERT INTO \"User\" (nick, gender, country, date_of_birth, registration_date, first_name, last_name, favorit_category_id) "
            + " VALUES (:nick, :gender, :country, :date_of_birth,:registration_date, :first_name, :last_name, :favorit_category_id)";

        public static string SQL_UPDATE = "UPDATE \"User\" SET nick=:nick, gender=:gender," +
            "country=:country, date_of_birth=:date_of_birth, registration_date=:registration_date," +
            "first_name=:first_name, last_name=:last_name, favorit_category_id=:favorit_category_id WHERE user_id=:user_id";

        public static string SQL_DELETE_ID = "UPDATE \"User\" SET deleted=1 where user_id=:user_id";

        public static string SQL_DELETE_NICK = "UPDATE \"User\" SET deleted=1 where nick=:nick";

        public static string SQL_SELECT_ALL_USERS_HEADER = "SELECT user_id, nick, country FROM \"User\"";

        public static string SQL_SELECT_USER_BY_ID = "SELECT user_id, nick, gender, country, date_of_birth, registration_date, first_name, last_name, favorit_category_id, deleted FROM \"User\" WHERE user_id=:user_id";

        public static string SQL_SELECT_USER_BY_NICK = "SELECT user_id, nick, gender, country, date_of_birth, registration_date, first_name, last_name, favorit_category_id, deleted FROM \"User\" WHERE nick=:nick";

        public static string SQL_SELECT_USER_BY_ID_WITH_CATEGORY = "SELECT u.user_id, u.nick, u.gender, u.country, u.date_of_birth, u.registration_date, u.first_name, u.last_name, u.favorit_category_id, u.deleted, name " +
            "FROM \"User\" u JOIN category on category_id = favorit_category_id WHERE user_id=:user_id";

        public static string SQL_SELECT_USER_BY_NICK_WITH_CATEGORY = "SELECT u.user_id, u.nick, u.gender, u.country, u.date_of_birth, u.registration_date, u.first_name, u.last_name, u.favorit_category_id, u.deleted, name " +
            "FROM \"User\" u JOIN category on category_id = favorit_category_id WHERE nick=:nick";

        // Methods
        public int registerNewUser(User user)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_REGISTER_NEW);
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int updateUser(User user)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int deleteUserById(int id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue(":user_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public List<User> selectUsers(DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = new DatabaseConnection();
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL_USERS_HEADER);
            OracleDataReader reader = db.Select(command);

            List<User> users = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }

        public User selectUserById(int id, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = new DatabaseConnection();
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_USER_BY_ID);
            command.Parameters.AddWithValue(":user_id", id);
            OracleDataReader reader = db.Select(command);

            List<User> users = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users.ElementAt(0);
        }

        public User selectUserByNick(string nick, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = new DatabaseConnection();
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_USER_BY_NICK);
            command.Parameters.AddWithValue(":nick", nick);
            OracleDataReader reader = db.Select(command);

            List<User> users = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users.ElementAt(0);
        }

        public User selectUserByIdWithCategory(int id, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = new DatabaseConnection();
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_USER_BY_ID_WITH_CATEGORY);
            command.Parameters.AddWithValue(":user_id", id);
            OracleDataReader reader = db.Select(command);

            List<User> users = Read(reader, true);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users.ElementAt(0);
        }

        public User selectUserByNickWithCategory(string nick, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = new DatabaseConnection();
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            OracleCommand command = db.CreateCommand(SQL_SELECT_USER_BY_NICK_WITH_CATEGORY);
            command.Parameters.AddWithValue(":nick", nick);
            OracleDataReader reader = db.Select(command);

            List<User> users = Read(reader, true);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            if (users.Count > 0)
                return users.ElementAt(0);
            else
                return null;
        }

        private static void PrepareCommand(OracleCommand command, User user)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":user_id", user.User_id);
            command.Parameters.AddWithValue(":nick", user.Nick);
            command.Parameters.AddWithValue(":gender", user.Gender);
            command.Parameters.AddWithValue(":country", user.Country);
            command.Parameters.AddWithValue(":date_of_birth", user.Date_of_birth);
            command.Parameters.AddWithValue(":registration_date", user.Registration_date);
            command.Parameters.AddWithValue(":first_name", user.First_name);
            command.Parameters.AddWithValue(":last_name", user.Last_name);
            command.Parameters.AddWithValue(":favorit_category_id", user.Favorit_category_id == null ? DBNull.Value : (object)user.Favorit_category_id);
            command.Parameters.AddWithValue(":deleted", user.Deleted == null ? DBNull.Value : (object)user.Deleted);
        }

        private static List<User> ReadHeader(OracleDataReader reader, bool withFavoritCategory = false)
        {
            List<User> users = new List<User>();

            while (reader.Read())
            {
                int i = -1;
                User user = new User();
                user.User_id = reader.GetInt32(++i);
                user.Nick = reader.GetString(++i);
                user.Country = reader.GetString(++i);

                users.Add(user);
            }
            return users;
        }

        private static List<User> Read(OracleDataReader reader, bool withFavoritCategory = false)
        {
            List<User> users = new List<User>();
            bool hasFavorit = false;

            while (reader.Read())
            {
                int i = -1;
                User user = new User();
                user.User_id = reader.GetInt32(++i);
                user.Nick = reader.GetString(++i);
                user.Gender = reader.GetString(++i)[0];
                user.Country = reader.GetString(++i);
                user.Date_of_birth = reader.GetDateTime(++i);
                user.Registration_date = reader.GetDateTime(++i);
                user.First_name = reader.GetString(++i);
                user.Last_name = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    user.Favorit_category_id = reader.GetInt32(i);
                    hasFavorit = true;
                }
                if (!reader.IsDBNull(++i))
                {
                    user.Deleted = reader.GetInt32(i);
                }

                if(withFavoritCategory && hasFavorit)
                {
                    Category category = new Category();
                    category.Category_id = (int)user.Favorit_category_id;
                    category.Name = reader.GetString(++i);
                    user.Favorit_category = category;
                }

                users.Add(user);
            }
            return users;
        }
    }
}
