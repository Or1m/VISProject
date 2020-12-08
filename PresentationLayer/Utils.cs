using PresentationLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public static class Utils
    {
        public static EnReleaseDate CompareDate(DateTime release)
        {
            DateTime now = DateTime.Now;

            if (now < release)
                return EnReleaseDate.notReleased;

            else if (now > release && now < release.AddDays(1))
                return EnReleaseDate.oldLessThan24;

            else
                return EnReleaseDate.released;
        }
    }
}
