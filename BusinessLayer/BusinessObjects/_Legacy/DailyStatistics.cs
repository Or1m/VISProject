using BusinessLayer.BusinessObjects.Behaviour;
using DTO;
using System;

namespace BusinessLayer.BusinessObjects
{
    public class DailyStatistics// : Persistable<DailyStatisticsDTO>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfReviews { get; set; }
        public string Type { get; set; }

        public DailyStatisticsDTO ToDTO()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Date.Date + " " + NumberOfReviews + " " + Type;
        }
    }
}
