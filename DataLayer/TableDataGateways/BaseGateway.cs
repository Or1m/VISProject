using System;
using System.Data.SqlClient;

namespace DataLayer.TableDataGateways
{
    public class BaseGateway
    {
        private static readonly object lockObj = new object();
        private static BaseGateway m_Instance;

        public static BaseGateway Instance {
            get {
                lock (lockObj)
                {
                    return m_Instance ?? (m_Instance = new BaseGateway());
                }
            }
        }

        public static int Insert(string name)
        {
            DatabaseConnection db = DatabaseConnection.Instance;
            //if (db.Connect())
            //{
                //SqlCommand command = db.CreateCommand(SQL_INSERT_NEW);
                //command.Parameters.AddWithValue(":name", name);
                //int ret = db.ExecuteNonQuery(command);

                //db.Close();
                //return ret;
            //}
            //else
                throw new Exception("Database cannot be opened");
        }

        public int Update(int id, string name)
        {
            //DatabaseConnection db = DatabaseConnection.Instance;
            //db.Connect();

            //SqlCommand command = db.CreateCommand(SQL_UPDATE);
            //command.Parameters.AddWithValue(":name", name);
            //command.Parameters.AddWithValue(":Category_id", id);
            //int ret = db.ExecuteNonQuery(command);

            //db.Close();
            //return ret;
            return 0; //temp
        }

        public static int Delete(int id)
        {
            //DatabaseConnection db = DatabaseConnection.Instance;
            //db.Connect();

            //SqlCommand command = db.CreateCommand(SQL_DELETE_ID);
            //command.Parameters.AddWithValue(":Category_id", id);
            //int ret = db.ExecuteNonQuery(command);

            //db.Close();
            //return ret;
            return 0; //temp
        }
    }
}
