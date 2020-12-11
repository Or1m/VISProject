using BusinessLayer.BusinessObjects;
using System.Collections.Generic;

namespace BusinessLayer.Controllers
{
    public class EmailManager
    {
        private static EmailManager instance = null;

        private static readonly object lockObj = new object();

        public Queue<Email<User, string, string>> EmailsToAdmin { get; }
        public Queue<Email<User, string, bool>> EmailsFromAdmin { get; }

        public static EmailManager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new EmailManager());
                }
            }
        }

        private EmailManager()
        {
            EmailsToAdmin = new Queue<Email<User, string, string>>();
            EmailsFromAdmin = new Queue<Email<User, string, bool>>();
        }
    }
}
