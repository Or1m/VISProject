using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class CategoryDTOGateway
    {
        #region SQL Commands
        private static string SQL_INSERT          = "INSERT INTO Category (name) VALUES (@name)";

        private static string SQL_DELETE_BY_ID    = "DELETE FROM Category WHERE Category_id=@Category_id";

        private static string SQL_UPDATE          = "UPDATE Category SET name=@name WHERE Category_id=@Category_id";

        private static string SQL_SELECT_ALL      = "SELECT Category_id, name from Category";

        private static string SQL_SELECT_BY_ID    = "SELECT Category_id, name from Category WHERE Category_id=@Category_id";

        private static string SQL_SELECT_BY_NAME  = "SELECT Category_id, name from Category WHERE name=@name";

        private static string SQL_SELECT_FOR_GAME = "SELECT Category_id, name from Category c JOIN game_Category gc ON c.Category_id = gc.Category_Category_id WHERE game_game_id=@game_id";
        #endregion


        private static readonly object lockObj = new object();
        private static CategoryDTOGateway instance;

        public static CategoryDTOGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new CategoryDTOGateway());
                }
            }
        }

        private CategoryDTOGateway() { }


        #region Non Query Methods
        public int Insert(string name)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@name", name);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int DeleteById(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE_BY_ID);
            command.Parameters.AddWithValue("@Category_id", id);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int Update(int id, string name)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@Category_id", id);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public List<CategoryDTO> SelectCategories()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }

        public List<CategoryDTO> SelectCategoriesForGame(int gameId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_FOR_GAME);
            command.Parameters.AddWithValue("@game_id", gameId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }

        public CategoryDTO SelectCategoryDTO(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_BY_ID);
            command.Parameters.AddWithValue("@CategoryDTO_id", id);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader).ElementAt(0);
        }
        public List<CategoryDTO> selectCategoryDTOByName(string name)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_BY_NAME);
            command.Parameters.AddWithValue("@name", name);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader);
        }
        #endregion

        #region Helpers
        private static List<CategoryDTO> Read(SqlDataReader reader)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        CategoryDTO CategoryDTO = new CategoryDTO();
                        CategoryDTO.CategoryId = reader.GetInt32(++i);
                        CategoryDTO.Name = reader.GetString(++i);

                        categories.Add(CategoryDTO);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return categories;
        }
        #endregion
    }
}