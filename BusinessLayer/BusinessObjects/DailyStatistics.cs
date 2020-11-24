using System;

namespace BusinessLayer.BusinessObjects
{
    public class DailyStatistics
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfReviews { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Date.Date + " " + NumberOfReviews + " " + Type;
        }
    }
}
