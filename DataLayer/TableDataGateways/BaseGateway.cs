using System;
using System.Data.SqlClient;

namespace DataLayer.TableDataGateways
{
    public class BaseGateway
    {
        public int ExecuteNonQuery(SqlCommand command)
        {
            if (DatabaseConnection.Instance.Connect())
            {
                int ret = DatabaseConnection.Instance.ExecuteNonQuery(command);

                DatabaseConnection.Instance.Close();
                return ret;
            }
            else
                throw new Exception("Database is not connected");
        }
    }
}
