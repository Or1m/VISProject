namespace DataLayer
{
    public interface IDatabaseProxy
    {
        bool Connect();
        bool Connect(string connectionString);
        void Close(); 
        void BeginTransaction(); 
        void EndTransaction(); 
        void Rollback();
    }
}