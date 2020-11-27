using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class Routines
    {
        public static bool SetUpConnection()
        {
            return DataLayer.Routines.SetUpConnection();
        }
    }
}
