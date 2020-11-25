using DataLayer;
using System;

namespace CMDTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DatabaseConnection.Instance.Connect());

            Console.ReadKey();
        }
    }
}
