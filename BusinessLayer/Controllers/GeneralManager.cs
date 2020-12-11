using BusinessLayer.BusinessObjects;
using BusinessLayer.Enums;
using System;

namespace BusinessLayer.Controllers
{
    public class GeneralManager
    {
        private static GeneralManager instance = null;

        private static readonly object lockObj = new object();

        public static GeneralManager Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new GeneralManager());
                }
            }
        }

        public EnEmailRequest CheckAndCreateEmail(string firstName, string lastName, string gender, string country, 
            DateTime birthDate, DateTime regDate, string work, string whyMe)
        {
            if (birthDate > regDate)
                return EnEmailRequest.dateMismatch;

            User user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender.ToCharArray()[0],
                Country = country,
                DateOfBirth = birthDate,
                RegistrationDate = regDate
            };

            Email<User, string, string> email = new Email<User, string, string>(user, work, whyMe);

            try
            {
                EmailManager.Instance.EmailsToAdmin.Enqueue(email);
            }
            catch
            {
                return EnEmailRequest.somethingWrong;
            }

            return EnEmailRequest.sended;
        }
    }
}
