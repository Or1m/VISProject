using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public int registerNewUser(UserDTO user)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_REGISTER_NEW);
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int updateUser(UserDTO user)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, user);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int deleteUserById(int id)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue(":user_id", id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public List<UserDTO> selectUsers(DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL_USERS_HEADER);
            SqlDataReader reader = db.Select(command);

            List<UserDTO> users = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }

        public UserDTO selectUserById(int id, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_USER_BY_ID);
            command.Parameters.AddWithValue(":user_id", id);
            SqlDataReader reader = db.Select(command);

            List<UserDTO> users = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users.ElementAt(0);
        }

        public UserDTO selectUserByNick(string nick, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_USER_BY_NICK);
            command.Parameters.AddWithValue(":nick", nick);
            SqlDataReader reader = db.Select(command);

            List<UserDTO> users = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users.ElementAt(0);
        }

        public UserDTO selectUserByIdWithCategory(int id, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_USER_BY_ID_WITH_CATEGORY);
            command.Parameters.AddWithValue(":user_id", id);
            SqlDataReader reader = db.Select(command);

            List<UserDTO> users = Read(reader, true);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users.ElementAt(0);
        }

        public UserDTO selectUserByNickWithCategory(string nick, DatabaseProxy pDb = null)
        {
            DatabaseConnection db;
            if (pDb == null)
            {
                db = DatabaseConnection.Instance;
                db.Connect();
            }
            else
            {
                db = (DatabaseConnection)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_USER_BY_NICK_WITH_CATEGORY);
            command.Parameters.AddWithValue(":nick", nick);
            SqlDataReader reader = db.Select(command);

            List<UserDTO> users = Read(reader, true);

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

        private static void PrepareCommand(SqlCommand command, UserDTO user)
        {
            //command.Parameters.AddWithValue(":user_id", user.UserId);
            //command.Parameters.AddWithValue(":nick", user.Nick);
            //command.Parameters.AddWithValue(":gender", user.Gender);
            //command.Parameters.AddWithValue(":country", user.Country);
            //command.Parameters.AddWithValue(":date_of_birth", user.DateOfBirth);
            //command.Parameters.AddWithValue(":registration_date", user.RegistrationDate);
            //command.Parameters.AddWithValue(":first_name", user.FirstName);
            //command.Parameters.AddWithValue(":last_name", user.LastName);
            //command.Parameters.AddWithValue(":favorit_category_id", user.FavoriteCategoryId == null ? DBNull.Value : (object)user.FavoriteCategoryId);
            //command.Parameters.AddWithValue(":deleted", user.Deleted == null ? DBNull.Value : (object)user.Deleted);
        }

        private static List<UserDTO> ReadHeader(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<UserDTO> users = new List<UserDTO>();

            while (reader.Read())
            {
                //int i = -1;
                //UserDTO user = new UserDTO();
                //user.UserId = reader.GetInt32(++i);
                //user.Nick = reader.GetString(++i);
                //user.Country = reader.GetString(++i);

                //users.Add(user);
            }
            return users;
        }

        private static List<UserDTO> Read(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<UserDTO> users = new List<UserDTO>();
            bool hasFavorit = false;

            while (reader.Read())
            {
                //int i = -1;
                //UserDTO user = new UserDTO();
                //user.UserId = reader.GetInt32(++i);
                //user.Nick = reader.GetString(++i);
                //user.Gender = reader.GetString(++i)[0];
                //user.Country = reader.GetString(++i);
                //user.DateOfBirth = reader.GetDateTime(++i);
                //user.RegistrationDate = reader.GetDateTime(++i);
                //user.FirstName = reader.GetString(++i);
                //user.LastName = reader.GetString(++i);
                //if (!reader.IsDBNull(++i))
                //{
                //    user.FavoriteCategoryId = reader.GetInt32(i);
                //    hasFavorit = true;
                //}
                //if (!reader.IsDBNull(++i))
                //{
                //    user.Deleted = reader.GetInt32(i);
                //}

                //if(withFavoritCategory && hasFavorit)
                //{
                //    CategoryDTO category = new CategoryDTO();
                //    category.CategoryId = (int)user.FavoriteCategoryId;
                //    category.Name = reader.GetString(++i);
                //    user.FavoriteCategory = category;
                //}

                //users.Add(user);
            }
            return users;
        }
    }
}
