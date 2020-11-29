using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
        public int registerNewReviewer(ReviewerDTO reviewer)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_REGISTER_NEW);
            PrepareCommand(command, reviewer);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }
        
        public int updateReviewer(ReviewerDTO reviewer)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, reviewer);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public int deleteReviewerById(int id)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue(":reviewer_id", id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public List<ReviewerDTO> selectReviewers(IDatabaseConnection pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL_REVIEWERS_HEADER);
            SqlDataReader reader = db.Select(command);

            List<ReviewerDTO> reviewers = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers;
        }

        public List<ReviewerDTO> selectFavoritReviewers(int userId, IDatabaseConnection pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_FAVORIT_REVIEWERS_FOR_USER);
            command.Parameters.AddWithValue(":user_id", userId);
            SqlDataReader reader = db.Select(command);

            List<ReviewerDTO> reviewers = ReadHeader(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers;
        }

        public ReviewerDTO selectReviewer(int id, IDatabaseConnection pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_REVIEWER);
            command.Parameters.AddWithValue(":reviewer_id", id);
            SqlDataReader reader = db.Select(command);

            List<ReviewerDTO> reviewers = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers.ElementAt(0);
        }

        public ReviewerDTO selectReviewerByIdWithCategory(int id, IDatabaseConnection pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_REVIEWER_WITH_CATEGORY);
            command.Parameters.AddWithValue(":reviewer_id", id);
            SqlDataReader reader = db.Select(command);

            List<ReviewerDTO> reviewers = Read(reader, true);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return reviewers.ElementAt(0);
        }

        private static void PrepareCommand(SqlCommand command, ReviewerDTO reviewer)
        {
            //command.Parameters.AddWithValue(":reviewer_id", reviewer.ReviewerId);
            //command.Parameters.AddWithValue(":first_name", reviewer.FirstName);
            //command.Parameters.AddWithValue(":last_name", reviewer.LastName);
            //command.Parameters.AddWithValue(":gender", reviewer.Gender);
            //command.Parameters.AddWithValue(":country", reviewer.Country);
            //command.Parameters.AddWithValue(":work", reviewer.Work);
            //command.Parameters.AddWithValue(":date_of_birth", reviewer.DateOfBirth);
            //command.Parameters.AddWithValue(":registration_date", reviewer.RegistrationDate);
            //command.Parameters.AddWithValue(":favorit_category_id", reviewer.FavoriteCategoryId == null ? DBNull.Value : (object)reviewer.FavoriteCategoryId);
        }

        private static List<ReviewerDTO> ReadHeader(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<ReviewerDTO> Reviewers = new List<ReviewerDTO>();

            while (reader.Read())
            {
                int i = -1;
                //ReviewerDTO reviewer = new ReviewerDTO();
                //reviewer.ReviewerId = reader.GetInt32(++i);
                //reviewer.FirstName = reader.GetString(++i);
                //reviewer.LastName = reader.GetString(++i);
                //reviewer.Country = reader.GetString(++i);
                //reviewer.Work = reader.GetString(++i);
                //Reviewers.Add(reviewer);
            }
            return Reviewers;
        }

        private static List<ReviewerDTO> Read(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<ReviewerDTO> Reviewers = new List<ReviewerDTO>();
            bool hasFavorit = false;

            while (reader.Read())
            {
                //int i = -1;
                //ReviewerDTO reviewer = new ReviewerDTO();
                //reviewer.ReviewerId = reader.GetInt32(++i);
                //reviewer.FirstName = reader.GetString(++i);
                //reviewer.LastName = reader.GetString(++i);
                //reviewer.Gender = reader.GetString(++i)[0];
                //reviewer.Country = reader.GetString(++i);
                //reviewer.Work = reader.GetString(++i);
                //reviewer.DateOfBirth = reader.GetDateTime(++i);
                //reviewer.RegistrationDate = reader.GetDateTime(++i);
                //if (!reader.IsDBNull(++i))
                //{
                //    reviewer.FavoriteCategoryId = reader.GetInt32(i);
                //    hasFavorit = true;
                //}
                //if (!reader.IsDBNull(++i))
                //{
                //    reviewer.Deleted = reader.GetInt32(i);
                //}
                //if (withFavoritCategory && hasFavorit)
                //{
                //    CategoryDTO category = new CategoryDTO();
                //    category.CategoryId = (int)reviewer.FavoriteCategoryId;
                //    category.Name = reader.GetString(++i);
                //    reviewer.FavoriteCategory = category;
                //}

                //Reviewers.Add(reviewer);
            }
            return Reviewers;
        }
    }
}
