namespace BusinessLayer
{
    public static class Routines
    {
        public static bool IsConnected()
        {
            return DataLayer.DatabaseConnection.Instance.Connect();
        }
    }
}