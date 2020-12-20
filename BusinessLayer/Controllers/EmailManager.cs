using BusinessLayer.BusinessObjects;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Controllers
{
    public class EmailManager
    {
        private static EmailManager instance = null;

        private static readonly object lockObj = new object();

        public Queue<Email<User, string, string>> EmailsForAdmin { get; }
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
            EmailsForAdmin = new Queue<Email<User, string, string>>();
            EmailsFromAdmin = new Queue<Email<User, string, bool>>();
        }

        public bool IsEmailForAdminInMailbox()
        {
            return EmailsForAdmin.Count > 0;
        }

        public bool IsEmailFromAdminInMailbox()
        {
            return EmailsFromAdmin.Count > 0;
        }

        public Email<User, string, string> ReadLastEmailForAdmin()
        {
            return EmailsForAdmin.Dequeue();
        }

        public void SendEmailToAdmin(Email<User, string, string> email)
        {
            EmailsForAdmin.Enqueue(email);
        }

        public void SendEmailFromAdmin(User t, string msg, bool approved)
        {
            Email<User, string, bool> newEmail =
                new Email<User, string, bool>(t, msg, approved);

            SendEmailFromAdmin(newEmail);
        }

        public void SendEmailFromAdmin(Email<User, string, bool> email)
        {
            EmailsFromAdmin.Enqueue(email);
        }

        public Email<User, string, bool> CheckLastEmailFromAdmin(int id)
        {
            var mail = EmailsFromAdmin.Peek();

            if (mail.t.Id == id)
                return EmailsFromAdmin.Dequeue();
            else
                return null;
        }
    }
}
