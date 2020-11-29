using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class UserGateway
    {
        #region SQL Commands
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
        #endregion

        private static readonly object lockObj = new object();
        private static UserGateway instance;

        public static UserGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new UserGateway());
                }
            }
        }

        private UserGateway() { }


        #region Non Query Methods
        public int Insert(UserDTO user)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_REGISTER_NEW);
            PrepareCommand(command, user);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Update(UserDTO user)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, user);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int DeleteById(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue(":user_id", id);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public List<UserDTO> SelectUsers()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_USERS_HEADER);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadHeader(reader);
        }

        public UserDTO SelectUserById(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_USER_BY_ID);
            command.Parameters.AddWithValue(":user_id", id);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader).ElementAt(0);
        }

        public UserDTO SelectUserByNick(string nick)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_USER_BY_NICK);
            command.Parameters.AddWithValue(":nick", nick);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader).ElementAt(0);
        }

        public UserDTO SelectUserByIdWithCategory(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_USER_BY_ID_WITH_CATEGORY);
            command.Parameters.AddWithValue(":user_id", id);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader, true).ElementAt(0);
        }

        public UserDTO SelectUserByNickWithCategory(string nick)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_USER_BY_NICK_WITH_CATEGORY);
            command.Parameters.AddWithValue(":nick", nick);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            List<UserDTO> users = Read(reader, true);

            return users.Count > 0 ? users.ElementAt(0) : null;
        }
        #endregion

        #region Helpers
        private static void PrepareCommand(SqlCommand command, UserDTO user)
        {
            command.Parameters.AddWithValue(":user_id", user.Id);
            command.Parameters.AddWithValue(":nick", user.Nick);
            command.Parameters.AddWithValue(":gender", user.Gender);
            command.Parameters.AddWithValue(":country", user.Country);
            command.Parameters.AddWithValue(":date_of_birth", user.DateOfBirth);
            command.Parameters.AddWithValue(":registration_date", user.RegistrationDate);
            command.Parameters.AddWithValue(":first_name", user.FirstName);
            command.Parameters.AddWithValue(":last_name", user.LastName);
            command.Parameters.AddWithValue(":favorite_category_id", user.FavoriteCategory == null ? DBNull.Value : (object)user.FavoriteCategory.CategoryId);
            command.Parameters.AddWithValue(":deleted", user.IsDeleted == null ? DBNull.Value : (object)user.IsDeleted);
        }

        private static List<UserDTO> ReadHeader(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<UserDTO> users = new List<UserDTO>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        UserDTO user = new UserDTO();
                        user.Id = reader.GetInt32(++i);
                        user.Nick = reader.GetString(++i);
                        user.Country = reader.GetString(++i);

                        users.Add(user);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return users;
        }

        private static List<UserDTO> Read(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<UserDTO> users = new List<UserDTO>();
            bool hasFavorite = false;

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        UserDTO user = new UserDTO();
                        user.Id = reader.GetInt32(++i);
                        user.Nick = reader.GetString(++i);
                        user.Gender = reader.GetString(++i)[0];
                        user.Country = reader.GetString(++i);
                        user.DateOfBirth = reader.GetDateTime(++i);
                        user.RegistrationDate = reader.GetDateTime(++i);
                        user.FirstName = reader.GetString(++i);
                        user.LastName = reader.GetString(++i);
                        if (!reader.IsDBNull(++i))
                        {
                            user.FavoriteCategory.CategoryId = reader.GetInt32(i);
                            hasFavorite = true;
                        }
                        if (!reader.IsDBNull(++i))
                        {
                            user.IsDeleted = reader.GetInt32(i) != 0 ? true : false;
                        }

                        if (withFavoritCategory && hasFavorite)
                        {
                            CategoryDTO category = new CategoryDTO();
                            category.CategoryId = user.FavoriteCategory.CategoryId;
                            category.Name = reader.GetString(++i);
                            user.FavoriteCategory = category;
                        }

                        users.Add(user);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return users;
        }
        #endregion
    }
}
