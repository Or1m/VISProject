using System.Collections.Generic;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class CategoryGateway
    {
        public static string SQL_INSERT_NEW = "INSERT INTO Category (name) VALUES (:name)";

        public static string SQL_DELETE_ID = "DELETE FROM Category WHERE category_id=:category_id";

        public static string SQL_UPDATE = "UPDATE Category SET name=:name WHERE category_id=:category_id";

        public static string SQL_SELECT_ALL = "SELECT category_id, name from Category";

        public static string SQL_SELECT_BY_ID = "SELECT category_id, name from Category WHERE category_id=:category_id";

        public static string SQL_SELECT_BY_NAME = "SELECT category_id, name from Category WHERE name=:name";

        public static string SQL_SELECT_FOR_GAME = "SELECT category_id, name from Category c JOIN game_category gc ON c.category_id = gc.category_category_id WHERE game_game_id=:game_id";

        // Methods
        public int insertNew(string name)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT_NEW);
            command.Parameters.AddWithValue(":name", name);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int deleteById(int id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue(":category_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int update(int id, string name)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue(":name", name);
            command.Parameters.AddWithValue(":category_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public List<Category> selectCategories(DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
            OracleDataReader reader = db.Select(command);

            List<Category> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories;
        }

        public List<Category> selectCategoriesForGame(int gameId, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_FOR_GAME);
            command.Parameters.AddWithValue(":game_id", gameId);
            OracleDataReader reader = db.Select(command);

            List<Category> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories;
        }

        public Category selectCategory(int id, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_BY_ID);
            command.Parameters.AddWithValue(":category_id", id);
            OracleDataReader reader = db.Select(command);

            List<Category> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return categories.ElementAt(0);
        }
        public List<Category> selectCategoryByName(string name, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_BY_NAME);
            command.Parameters.AddWithValue(":name", name);
            OracleDataReader reader = db.Select(command);

            List<Category> categories = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            
            return categories;
            
        }

        private static List<Category> Read(OracleDataReader reader)
        {
            List<Category> categories = new List<Category>();

            while (reader.Read())
            {
                int i = -1;
                Category Category = new Category();
                Category.Category_id = reader.GetInt32(++i);
                Category.Name = reader.GetString(++i);

                categories.Add(Category);
            }
            return categories;
        }
    }
}
