using BusinessLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Controllers
{
    class EmailManager
    {
        private static EmailManager instance = null;

        private static readonly object lockObj = new object();

        public List<Email<User, string, string>> Emails { get; }

        public static EmailManager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new EmailManager());
                }
            }
        }
    }
}
