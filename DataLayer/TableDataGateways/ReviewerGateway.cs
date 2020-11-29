using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    public class ReviewerGateway
    {
        #region SQL Commands
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
        #endregion

        private static readonly object lockObj = new object();
        private static ReviewerGateway instance;

        public static ReviewerGateway Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new ReviewerGateway());
                }
            }
        }

        private ReviewerGateway() { }

        #region Non Query Methods
        public int Insert(ReviewerDTO reviewer)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_REGISTER_NEW);
            PrepareCommand(command, reviewer);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        
        public int Update(ReviewerDTO reviewer)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, reviewer);
            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }

        public int DeleteById(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue(":reviewer_id", id);

            return DatabaseConnection.Instance.ExecuteNonQuery(command);
        }
        #endregion

        #region Query Methods
        public List<ReviewerDTO> SelectReviewers()
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_ALL_REVIEWERS_HEADER);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadHeader(reader);
        }

        public List<ReviewerDTO> SelectFavoriteReviewers(int userId)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_FAVORIT_REVIEWERS_FOR_USER);
            command.Parameters.AddWithValue(":user_id", userId);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return ReadHeader(reader);
        }

        public ReviewerDTO SelectReviewer(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_REVIEWER);
            command.Parameters.AddWithValue(":reviewer_id", id);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader).ElementAt(0);
        }

        public ReviewerDTO SelectReviewerByIdWithCategory(int id)
        {
            SqlCommand command = DatabaseConnection.Instance.CreateCommand(SQL_SELECT_REVIEWER_WITH_CATEGORY);
            command.Parameters.AddWithValue(":reviewer_id", id);
            SqlDataReader reader = DatabaseConnection.Instance.Select(command);

            return Read(reader, true).ElementAt(0);
        }
        #endregion

        #region Helpers
        private static void PrepareCommand(SqlCommand command, ReviewerDTO reviewer)
        {
            command.Parameters.AddWithValue(":reviewer_id", reviewer.Id);
            command.Parameters.AddWithValue(":first_name", reviewer.FirstName);
            command.Parameters.AddWithValue(":last_name", reviewer.LastName);
            command.Parameters.AddWithValue(":gender", reviewer.Gender);
            command.Parameters.AddWithValue(":country", reviewer.Country);
            command.Parameters.AddWithValue(":work", reviewer.Work);
            command.Parameters.AddWithValue(":date_of_birth", reviewer.DateOfBirth);
            command.Parameters.AddWithValue(":registration_date", reviewer.RegistrationDate);
            command.Parameters.AddWithValue(":favorit_category_id", reviewer.FavoriteCategory == null ? DBNull.Value : (object)reviewer.FavoriteCategory.CategoryId);
        }

        private static List<ReviewerDTO> ReadHeader(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<ReviewerDTO> Reviewers = new List<ReviewerDTO>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        ReviewerDTO reviewer = new ReviewerDTO();
                        reviewer.Id = reader.GetInt32(++i);
                        reviewer.FirstName = reader.GetString(++i);
                        reviewer.LastName = reader.GetString(++i);
                        reviewer.Country = reader.GetString(++i);
                        reviewer.Work = reader.GetString(++i);
                        Reviewers.Add(reviewer);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return Reviewers;
        }

        private static List<ReviewerDTO> Read(SqlDataReader reader, bool withFavoritCategory = false)
        {
            List<ReviewerDTO> Reviewers = new List<ReviewerDTO>();
            bool hasFavorite = false;

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = -1;
                        ReviewerDTO reviewer = new ReviewerDTO();
                        reviewer.Id = reader.GetInt32(++i);
                        reviewer.FirstName = reader.GetString(++i);
                        reviewer.LastName = reader.GetString(++i);
                        reviewer.Gender = reader.GetString(++i)[0];
                        reviewer.Country = reader.GetString(++i);
                        reviewer.Work = reader.GetString(++i);
                        reviewer.DateOfBirth = reader.GetDateTime(++i);
                        reviewer.RegistrationDate = reader.GetDateTime(++i);
                        if (!reader.IsDBNull(++i))
                        {
                            reviewer.FavoriteCategory.CategoryId = reader.GetInt32(i);
                            hasFavorite = true;
                        }
                        if (!reader.IsDBNull(++i))
                        {
                            reviewer.IsDeleted = reader.GetInt32(i) != 0 ? true : false;
                        }
                        if (withFavoritCategory && hasFavorite)
                        {
                            CategoryDTO category = new CategoryDTO();
                            category.CategoryId = reviewer.FavoriteCategory.CategoryId;
                            category.Name = reader.GetString(++i);
                            reviewer.FavoriteCategory = category;
                        }

                        Reviewers.Add(reviewer);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            return Reviewers;
        }
        #endregion
    }
}
