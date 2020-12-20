using BusinessLayer.BusinessObjects;
using System.Collections.Generic;

namespace BusinessLayer.Controllers
{
    public class EmailManager
    {
        #region Private Fields
        private Queue<Email<User, string, string>>  emailsForAdmin;
        private Queue<Email<User, string, bool>>    emailsFromAdmin;
        #endregion


        #region Private Constructor & Singleton Pattern
        private static EmailManager instance    = null;
        private static readonly object lockObj  = new object();
        
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
            emailsForAdmin  = new Queue<Email<User, string, string>>();
            emailsFromAdmin = new Queue<Email<User, string, bool>>();
        }
        #endregion


        public bool IsEmailForAdminInMailbox()
        {
            return emailsForAdmin.Count > 0;
        }

        public bool IsEmailFromAdminInMailbox()
        {
            return emailsFromAdmin.Count > 0;
        }

        public Email<User, string, string> ReadLastEmailForAdmin()
        {
            return emailsForAdmin.Dequeue();
        }

        public Email<User, string, bool> CheckLastEmailFromAdmin(int id)
        {
            var mail = emailsFromAdmin.Peek();

            return mail.t.Id == id ? emailsFromAdmin.Dequeue() : null;
        }

        public void SendEmailToAdmin(Email<User, string, string> email)
        {
            emailsForAdmin.Enqueue(email);
        }

        public void SendEmailFromAdmin(User t, string msg, bool approved)
        {
            Email<User, string, bool> newEmail =
                new Email<User, string, bool>(t, msg, approved);

            SendEmailFromAdmin(newEmail);
        }

        public void SendEmailFromAdmin(Email<User, string, bool> email)
        {
            emailsFromAdmin.Enqueue(email);
        }
    }
}
