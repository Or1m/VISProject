using System;

namespace CMDTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DataLayer.DatabaseConnection.Instance.Connect());

            Console.ReadKey();
        }
    }
}
