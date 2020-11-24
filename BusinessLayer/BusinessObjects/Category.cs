using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessObjects
{
    public class Category
    {
        public int Category_id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Category_id + " " + Name;
        }

        public string ToStringHeader()
        {
            return Name;
        }
    }
}
