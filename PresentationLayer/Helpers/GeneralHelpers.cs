using BusinessLayer.Controllers;
using PresentationLayer.Enums;
using System;

namespace PresentationLayer.Helpers
{
    public class GeneralHelpers
    {
        #region Singleton Pattern
        private static GeneralHelpers instance = null;

        private static readonly object lockObj = new object();

        public static GeneralHelpers Instance {
            get {
                lock (lockObj)
                {
                    return instance ?? (instance = new GeneralHelpers());
                }
            }
        }
        #endregion

        #region Public Methods
        public EnRequest CheckRequest(string firstName, string lastName, string gender, string country,
            string dateOfBirth, string registrationDate, string work, out DateTime birthDate, out DateTime regDate)
        {
            birthDate = DateTime.MinValue;
            regDate = DateTime.MinValue;

            if (!Utils.StringIsValid(firstName))
                return EnRequest.invalidFName;

            if (!Utils.StringIsValid(lastName))
                return EnRequest.invalidLName;

            if (gender != "F" && gender != "M")
                return EnRequest.invalidGender;

            if (!Utils.StringIsValid(country))
                return EnRequest.invalidCountry;

            if (!Utils.StringIsValid(work))
                return EnRequest.invalidWork;

            if (!DateTime.TryParse(dateOfBirth, out DateTime birth))
                return EnRequest.invalidDateOfBirth;
                
            if (!DateTime.TryParse(registrationDate, out DateTime reg))
                return EnRequest.invalidRegistrationDate;

            birthDate = birth;
            regDate = reg;

            return EnRequest.valid;
        }

        public Enum CheckRequestAndSendFurther(string firstName, string lastName, string gender, string country,
            string dateOfBirth, string registrationDate, string work, string whyMe, int id)
        {
            EnRequest result = CheckRequest(firstName, lastName, gender, country, dateOfBirth, registrationDate, work, 
                out DateTime birthDate, out DateTime regDate);
            
            if (result != EnRequest.valid)
                return result;

            if (!Utils.StringIsValid(whyMe))
                return EnRequest.invalidWhyMe;

            if (birthDate != DateTime.MinValue && regDate != DateTime.MinValue)
                return GeneralManager.Instance.CheckAndCreateEmail(firstName, lastName, gender, country, birthDate, regDate, work, whyMe, id);
            else
                return EnRequest.somethingWrong;
        }
        #endregion
    }
}