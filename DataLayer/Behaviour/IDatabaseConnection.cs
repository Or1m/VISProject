namespace DataLayer.Behaviour
{
    interface IDatabaseConnection
    {
        bool Connect();
        bool Connect(string connectionString);
        void Close(); 
        void BeginTransaction(); 
        void EndTransaction(); 
        void Rollback();
    }
}