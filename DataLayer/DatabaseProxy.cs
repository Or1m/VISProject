namespace DataLayer
{
    public abstract class DatabaseProxy
    {
        public abstract bool Connect();
        public abstract bool Connect(string connectionString);
        public abstract void Close(); 
        public abstract void BeginTransaction(); 
        public abstract void EndTransaction(); 
        public abstract void Rollback();
    }
}