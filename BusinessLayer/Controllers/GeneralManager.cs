using BusinessLayer.BusinessObjects;
using BusinessLayer.Enums;
using DataLayer.TableDataGateways;
using DTO;
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

        public EnBusinessRequest CheckAndCreateEmail(string firstName, string lastName, string gender, string country, 
            DateTime birthDate, DateTime regDate, string work, string whyMe, int id)
        {
            if (birthDate > regDate || regDate.Year < 2014)
                return EnBusinessRequest.dateMismatch;

            User user = new User()
            {
                Id = id,
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
                EmailManager.Instance.SendEmailToAdmin(email);
            }
            catch
            {
                return EnBusinessRequest.somethingWrong;
            }

            return EnBusinessRequest.sucess;
        }

        public void ExportDaily()
        {
            DailyStatisticsGateway.Instance.Save(DailyStatistics.Instance.ToDTO(), out string errMsg);
        }

        public bool TryToFindDaily(string selectedDateString, out DailyStatisticsDTO dailyDto)
        {
            dailyDto = null;

            DailyStatisticsGateway.Instance.Load(selectedDateString, out DailyStatisticsDTO dto, out string errMsg);

            if (dto == null)
            {
                return false;
            }
            else
            {
                dailyDto = dto;
                return true;
            }
        }
    }
}