
namespace DataLayer.TableDataGateways
{
    public class ReviewerGateway
    {
        public static string SQL_REGISTER_NEW = "INSERT INTO Reviewer (first_name, last_name, gender, country, work, date_of_birth, registration_date, favorit_category_id) "
            + " VALUES (:first_name, :last_name, :gender, :country, :work, :date_of_birth, :registration_date, :favorit_category_id)";

        public static string SQL_UPDATE = "UPDATE Reviewer SET reviewer_id=:reviewer_id, first_name=:first_name, last_name=:last_name, gender=:gender," +
            "country=:country, work=:work, date_of_birth=:date_of_birth, registration_date=:registration_date," +
            " favorit_category_id=:favorit_category_id WHERE reviewer_id=:reviewer_id";

        public static string SQL_SELECT_ALL_REVIEWERS_HEADER = "SELECT reviewer_id, first_name, last_name, country, work FROM Reviewer";

        public static string SQL_SELECT_REVIEWER = "SELECT reviewer_id, first_name, last_name, gender, country, work, date_of_birth, registration_date, favorit_category_id, deleted FROM Reviewer WHERE reviewer_id=:reviewer_id";

        public static string SQL_SELECT_REVIEWER_WITH_CATEGORY = "SELECT r.reviewer_id, r.first_name, r.last_name, r.gender, r.country, r.work, r.date_of_birth, r.registration_date, r.favorit_category_id, r.deleted, name " +
            "FROM Reviewer r JOIN category on category_id = favorit_category_id WHERE reviewer_id=:reviewer_id";

        public static string SQL_DELETE_ID = "UPDATE Reviewer SET deleted=1 WHERE reviewer_id=:reviewer_id";

        public static string SQL_SELECT_FAVORIT_REVIEWERS_FOR_USER = "SELECT reviewer_id, first_name, last_name, country, work " +
            "FROM Reviewer r JOIN Favorit_reviewer fr ON r.reviewer_id = fr.reviewer_reviewer_id WHERE user_user_id=:user_id";

        // Methods
        public int registerNewReviewer(Reviewer reviewer)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_REGISTER_NEW);
            PrepareCommand(command, reviewer);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        
        public int updateReviewer(Reviewer reviewer)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, reviewer);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public int deleteReviewerById(int id)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue(":reviewer_id", id);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public List<Reviewer> selectReviewers(DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL_REVIEWERS_HEADER);
            OracleDataReader reader = db.Select(command);

            List<Reviewer> reviewers = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers;
        }

        public List<Reviewer> selectFavoritReviewers(int userId, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_FAVORIT_REVIEWERS_FOR_USER);
            command.Parameters.AddWithValue(":user_id", userId);
            OracleDataReader reader = db.Select(command);

            List<Reviewer> reviewers = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers;
        }

        public Reviewer selectReviewer(int id, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_REVIEWER);
            command.Parameters.AddWithValue(":reviewer_id", id);
            OracleDataReader reader = db.Select(command);

            List<Reviewer> reviewers = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers.ElementAt(0);
        }

        public Reviewer selectReviewerByIdWithCategory(int id, DatabaseProxy pDb = null)
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

            OracleCommand command = db.CreateCommand(SQL_SELECT_REVIEWER_WITH_CATEGORY);
            command.Parameters.AddWithValue(":reviewer_id", id);
            OracleDataReader reader = db.Select(command);

            List<Reviewer> reviewers = Read(reader, true);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers.ElementAt(0);
        }

        private static void PrepareCommand(OracleCommand command, Reviewer reviewer)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":reviewer_id", reviewer.Reviewer_id);
            command.Parameters.AddWithValue(":first_name", reviewer.First_name);
            command.Parameters.AddWithValue(":last_name", reviewer.Last_name);
            command.Parameters.AddWithValue(":gender", reviewer.Gender);
            command.Parameters.AddWithValue(":country", reviewer.Country);
            command.Parameters.AddWithValue(":work", reviewer.Work);
            command.Parameters.AddWithValue(":date_of_birth", reviewer.Date_of_birth);
            command.Parameters.AddWithValue(":registration_date", reviewer.Registration_date);
            command.Parameters.AddWithValue(":favorit_category_id", reviewer.Favorit_category_id == null ? DBNull.Value : (object)reviewer.Favorit_category_id);
        }

        private static List<Reviewer> ReadHeader(OracleDataReader reader, bool withFavoritCategory = false)
        {
            List<Reviewer> Reviewers = new List<Reviewer>();

            while (reader.Read())
            {
                int i = -1;
                Reviewer reviewer = new Reviewer();
                reviewer.Reviewer_id = reader.GetInt32(++i);
                reviewer.First_name = reader.GetString(++i);
                reviewer.Last_name = reader.GetString(++i);
                reviewer.Country = reader.GetString(++i);
                reviewer.Work = reader.GetString(++i);
                Reviewers.Add(reviewer);
            }
            return Reviewers;
        }

        private static List<Reviewer> Read(OracleDataReader reader, bool withFavoritCategory = false)
        {
            List<Reviewer> Reviewers = new List<Reviewer>();
            bool hasFavorit = false;

            while (reader.Read())
            {
                int i = -1;
                Reviewer reviewer = new Reviewer();
                reviewer.Reviewer_id = reader.GetInt32(++i);
                reviewer.First_name = reader.GetString(++i);
                reviewer.Last_name = reader.GetString(++i);
                reviewer.Gender = reader.GetString(++i)[0];
                reviewer.Country = reader.GetString(++i);
                reviewer.Work = reader.GetString(++i);
                reviewer.Date_of_birth = reader.GetDateTime(++i);
                reviewer.Registration_date = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    reviewer.Favorit_category_id = reader.GetInt32(i);
                    hasFavorit = true;
                }
                if (!reader.IsDBNull(++i))
                {
                    reviewer.Deleted = reader.GetInt32(i);
                }
                if (withFavoritCategory && hasFavorit)
                {
                    Category category = new Category();
                    category.Category_id = (int)reviewer.Favorit_category_id;
                    category.Name = reader.GetString(++i);
                    reviewer.Favorit_category = category;
                }

                Reviewers.Add(reviewer);
            }
            return Reviewers;
        }
    }
}
