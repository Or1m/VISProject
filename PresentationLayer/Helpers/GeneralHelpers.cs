
using BusinessLayer.Controllers;
using PresentationLayer.Enums;
using System;

namespace PresentationLayer.Helpers
{
    public class GeneralHelpers
    {
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

        public Enum CheckRequestAndSendFurther(string firstName, string lastName, string gender, string country,
            string dateOfBirth, string registrationDate, string work, string whyMe)
        {
            DateTime birthDate, regDate;

            if (!Utils.StringIsValid(firstName))
                return EnRequest.invalidFName;

            if (!Utils.StringIsValid(lastName))
                return EnRequest.invalidLName;

            if (gender != "F" && gender != "M")
                return EnRequest.invalidGender;

            if (!Utils.StringIsValid(country))
                return EnRequest.invalidCountry;
            
            if (!DateTime.TryParse(dateOfBirth, out birthDate))
                return EnRequest.invalidDateOfBirth;

            if (!DateTime.TryParse(registrationDate, out regDate))
                return EnRequest.invalidRegistrationDate;

            if (!Utils.StringIsValid(work))
                return EnRequest.invalidWork;

            if (!Utils.StringIsValid(whyMe))
                return EnRequest.invalidWhyMe;

            return GeneralManager.Instance.CheckAndCreateEmail(firstName, lastName, gender, country, birthDate, regDate, work, whyMe);
        }
    }
}
