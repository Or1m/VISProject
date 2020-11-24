using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessObjects
{
    public class DailyStatistics
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int number_of_reviews { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Date.Date + " " + number_of_reviews + " " + Type;
        }
    }
}
