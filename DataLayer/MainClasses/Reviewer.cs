using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.MainClasses
{
    public class Reviewer
    {
        public int Reviewer_id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string Work { get; set; }
        public DateTime Date_of_birth { get; set; }
        public DateTime Registration_date { get; set; }
        public int? Favorit_category_id { get; set; }
        public Category Favorit_category { get; set; }
        public int? Deleted { get; set; }

        public override string ToString()
        {
            return First_name + " " + Last_name + " " + Gender + " " + Work + " " + Country + " " + Date_of_birth.Date + " " + Registration_date.Date + " " + Favorit_category_id + " " + Deleted;
        }
        public string ToStringHeader()
        {
            return First_name + " " + Last_name + " [ " + Work + " ]" + ", " + Country;
        }

        public Reviewer() { }
        public Reviewer(string first_name, string last_name, char gender, string country, string work, DateTime date_of_birth, DateTime registration_date, int? favorit_category_id = null, Category favorit_category = null, int ? deleted = null)
        {
            First_name = first_name;
            Last_name = last_name;
            Gender = gender;
            Country = country;
            Work = work;
            Date_of_birth = date_of_birth;
            Registration_date = registration_date;
            Favorit_category_id = favorit_category_id;
            Favorit_category = favorit_category;
            Deleted = deleted;
        }
    }
}
