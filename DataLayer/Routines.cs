using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class Routines
    {
        public static bool SetUpConnection()
        {
            return DatabaseConnection.Instance.Connect();
        }
    }
}
