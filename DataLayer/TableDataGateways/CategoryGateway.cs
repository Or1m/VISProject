using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class CategoryDTOGateway
    {
        public static string SQL_INSERT_NEW = "INSERT INTO CategoryDTO (name) VALUES (:name)";

        public static string SQL_DELETE_ID = "DELETE FROM CategoryDTO WHERE CategoryDTO_id=:CategoryDTO_id";

        public static string SQL_UPDATE = "UPDATE CategoryDTO SET name=:name WHERE CategoryDTO_id=:CategoryDTO_id";

        public static string SQL_SELECT_ALL = "SELECT CategoryDTO_id, name from CategoryDTO";

        public static string SQL_SELECT_BY_ID = "SELECT CategoryDTO_id, name from CategoryDTO WHERE CategoryDTO_id=:CategoryDTO_id";

        public static string SQL_SELECT_BY_NAME = "SELECT CategoryDTO_id, name from CategoryDTO WHERE name=:name";

        public static string SQL_SELECT_FOR_GAME = "SELECT CategoryDTO_id, name from CategoryDTO c JOIN game_CategoryDTO gc ON c.CategoryDTO_id = gc.CategoryDTO_CategoryDTO_id WHERE game_game_id=:game_id";

        // Methods
        public int insertNew(string name)
        {
            var db = DatabaseConnection.Instance;
            db.Connect();

            var command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":name", name);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int deleteById(int id)
        {
            var db = DatabaseConnection.Instance;
            db.Connect();

            var command = db.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue(":CategoryDTO_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int update(int id, string name)
        {
            var db = DatabaseConnection.Instance;
            db.Connect();

            var command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue(":name", name);
            command.Parameters.AddWithValue(":CategoryDTO_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public List<CategoryDTO> selectCategories(DatabaseProxy pDb = null)
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

            var command = db.CreateCommand(SQL_SELECT_ALL);
            OracleDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories;
        }

        public List<CategoryDTO> selectCategoriesForGame(int gameId, DatabaseProxy pDb = null)
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

            var command = db.CreateCommand(SQL_SELECT_FOR_GAME);
            command.Parameters.AddWithValue(":game_id", gameId);
            OracleDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories;
        }

        public CategoryDTO selectCategoryDTO(int id, DatabaseProxy pDb = null)
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

            var command = db.CreateCommand(SQL_SELECT_BY_ID);
            command.Parameters.AddWithValue(":CategoryDTO_id", id);
            OracleDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories.ElementAt(0);
        }
        public List<CategoryDTO> selectCategoryDTOByName(string name, DatabaseProxy pDb = null)
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

            var command = db.CreateCommand(SQL_SELECT_BY_NAME);
            command.Parameters.AddWithValue(":name", name);
            OracleDataReader reader = db.Select(command);

            List<CategoryDTO> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            
            return categories;
            
        }

        private static List<CategoryDTO> Read(OracleDataReader reader)
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
