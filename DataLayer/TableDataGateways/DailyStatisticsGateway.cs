using System.Collections.Generic;
using System.Linq;

namespace DataLayer.TableDataGateways
{
    class DailyStatisticsGateway
    {
        public static string SQL_SELECT_ALL = "SELECT id, \"date\", number_of_reviews, type FROM DailyStatistics";

        public static string SQL_SELECT_BY_ID = "SELECT id, \"date\", number_of_reviews, type FROM DailyStatistics WHERE id=:id";

        public static string SQL_SELECT_BY_DATE = "SELECT id, \"date\", number_of_reviews, type FROM DailyStatistics WHERE \"date\"=:datee";

        // Methods
        public List<DailyStatistics> selectStatistics(DatabaseProxy pDb = null)
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

            List<DailyStatistics> statistics = Read(reader);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return statistics;
        }

        public DailyStatistics selectStatisticById(int id, DatabaseProxy pDb = null)
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
            command.Parameters.AddWithValue(":id", id);
            OracleDataReader reader = db.Select(command);

            List<DailyStatistics> statistics = Read(reader);

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

        private static List<DailyStatistics> Read(OracleDataReader reader)
        {
            List<DailyStatistics> statistics = new List<DailyStatistics>();

            while (reader.Read())
            {
                int i = -1;
                DailyStatistics statistic = new DailyStatistics();
                statistic.Id = reader.GetInt32(++i);
                statistic.Date = reader.GetDateTime(++i);
                statistic.number_of_reviews = reader.GetInt32(++i);
                statistic.Type = reader.GetString(++i);

                statistics.Add(statistic);
            }
            return statistics;
        }
    }
}
