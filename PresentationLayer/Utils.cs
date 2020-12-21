using PresentationLayer.Enums;
using System;
using System.Linq;

namespace PresentationLayer
{
    public static class Utils
    {
        #region Public Static Methods
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

        public static bool StringIsValid(string str)
        {
            return !(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str) || str.All(c => char.IsDigit(c)));
        }
        #endregion
    }
}