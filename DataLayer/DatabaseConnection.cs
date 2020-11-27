using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DatabaseConnection : DatabaseProxy
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


        // Service methods
        public override bool Connect()
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

        public override bool Connect(string connectionString)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.ConnectionString = connectionString;
                Connection.Open();
            }

            return true;
        }

        public override void Close()
        {
            Connection.Close();
        }

        public override void BeginTransaction()
        {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public override void EndTransaction()
        {
            SqlTransaction.Commit();
            Close();
        }

        public override void Rollback()
        {
            SqlTransaction.Rollback();
        }


        // Database manipulation methods
        public int ExecuteNonQuery(SqlCommand command)
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
            return rowNumber;
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

        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
    }
}