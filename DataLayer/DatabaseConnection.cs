using DataLayer.Behaviour;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private SqlConnection connection;
        private SqlTransaction sqlTransaction;

        private static readonly object lockObj = new object();
        private static DatabaseConnection instance = null;


        public SqlTransaction SqlTransaction {
            get => sqlTransaction;
            set => sqlTransaction = value;
        }

        public SqlConnection Connection {
            get => connection;
            set => connection = value;
        }

        public static DatabaseConnection Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new DatabaseConnection());
                }
            }
        }


        private DatabaseConnection()
        {
            connection = new SqlConnection();

            // Connection string je v konfiguraèním souboru xxxx.dll.config
            Connection.ConnectionString = Properties.Settings.Default.ConnectionString;
        }


        #region Service Metods
        public bool Connect()
        {
            if (Connection.State == ConnectionState.Open)
                return true;

            try
            {
                Connection.Open();
                return true;
            } 
            catch
            {
                return false;
            }
        }

        public  bool Connect(string connectionString)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.ConnectionString = connectionString;
                Connection.Open();
            }

            return true;
        }

        public  void Close()
        {
            Connection.Close();
        }

        public  void BeginTransaction()
        {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public  void EndTransaction()
        {
            SqlTransaction.Commit();
            Close();
        }

        public  void Rollback()
        {
            SqlTransaction.Rollback();
        }
        #endregion


        #region Database Manipulation Methods
        public int ExecuteNonQuery(SqlCommand command)
        {
            if (Connect())
            {
                int rowNumber = 0;
                try
                {
                    rowNumber = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    command.Dispose();
                    Close();
                }

                return rowNumber;
            }
            else
                throw new Exception("Database is not connected");
        }

        public int ExecuteScalar(SqlCommand command)
        {
            if (Connect())
            {
                int result = 0;
                try
                {
                    result = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    command.Dispose();
                    Close();
                }

                return result;
            }
            else
                throw new Exception("Database is not connected");
        }

        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader;

            if (Connect())
            {
                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (Exception e)
                {
                    throw e;
                }

                return sqlReader;
            }
            else
                throw new Exception("Database is not connected");
        }

        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Connection);

            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;
            }
            return command;
        }
        #endregion
    }
}