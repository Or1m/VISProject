using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class CategoryDTOGateway
    {
        public static string SQL_INSERT_NEW      = "INSERT INTO Category (name) VALUES (:name)";

        public static string SQL_DELETE_ID       = "DELETE FROM Category WHERE Category_id=:Category_id";

        public static string SQL_UPDATE          = "UPDATE Category SET name=:name WHERE Category_id=:Category_id";

        public static string SQL_SELECT_ALL      = "SELECT Category_id, name from Category";

        public static string SQL_SELECT_BY_ID    = "SELECT Category_id, name from Category WHERE Category_id=:Category_id";

        public static string SQL_SELECT_BY_NAME  = "SELECT Category_id, name from Category WHERE name=:name";

        public static string SQL_SELECT_FOR_GAME = "SELECT Category_id, name from Category c JOIN game_Category gc ON c.Category_id = gc.Category_Category_id WHERE game_game_id=:game_id";

        // Methods
        public int insertNew(string name)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":name", name);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int deleteById(int id)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue(":Category_id", id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int update(int id, string name)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue(":name", name);
            command.Parameters.AddWithValue(":Category_id", id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public List<CategoryDTO> selectCategories(IDatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories;
        }

        public List<CategoryDTO> selectCategoriesForGame(int gameId, IDatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_FOR_GAME);
            command.Parameters.AddWithValue(":game_id", gameId);
            SqlDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories;
        }

        public CategoryDTO selectCategoryDTO(int id, IDatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_ID);
            command.Parameters.AddWithValue(":CategoryDTO_id", id);
            SqlDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories.ElementAt(0);
        }
        public List<CategoryDTO> selectCategoryDTOByName(string name, IDatabaseProxy pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_BY_NAME);
            command.Parameters.AddWithValue(":name", name);
            SqlDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            
            return categories;
            
        }

        private static List<CategoryDTO> Read(SqlDataReader reader)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();

            while (reader.Read())
            {
                int i = -1;
                CategoryDTO CategoryDTO = new CategoryDTO();
                CategoryDTO.CategoryId = reader.GetInt32(++i);
                CategoryDTO.Name = reader.GetString(++i);

                categories.Add(CategoryDTO);
            }
            return categories;
        }
    }
}
