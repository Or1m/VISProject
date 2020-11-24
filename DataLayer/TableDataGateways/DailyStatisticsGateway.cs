using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    class DailyStatisticsDTOGateway
    {
        public static string SQL_SELECT_ALL     = "SELECT id, \"date\", number_of_reviews, type FROM DailyStatistics";

        public static string SQL_SELECT_BY_ID   = "SELECT id, \"date\", number_of_reviews, type FROM DailyStatistics WHERE id=:id";

        public static string SQL_SELECT_BY_DATE = "SELECT id, \"date\", number_of_reviews, type FROM DailyStatistics WHERE \"date\"=:datee";

        // Methods
        public List<DailyStatisticsDTO> selectStatistics(DatabaseProxy pDb = null)
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

            List<DailyStatisticsDTO> statistics = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return statistics;
        }

        public DailyStatisticsDTO selectStatisticById(int id, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue(":id", id);
            SqlDataReader reader = db.Select(command);

            List<DailyStatisticsDTO> statistics = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            if (statistics.Count() != 0)
                return statistics.ElementAt(0);
            else
                return null;
        }

        private static List<DailyStatisticsDTO> Read(SqlDataReader reader)
        {
            List<DailyStatisticsDTO> statistics = new List<DailyStatisticsDTO>();

            while (reader.Read())
            {
                int i = -1;
                DailyStatisticsDTO statistic = new DailyStatisticsDTO();
                statistic.Id = reader.GetInt32(++i);
                statistic.Date = reader.GetDateTime(++i);
                statistic.NumberOfReviews = reader.GetInt32(++i);
                statistic.Type = reader.GetString(++i);

                statistics.Add(statistic);
            }
            return statistics;
        }
    }
}
