using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.StaticTools;

namespace Application.Convertor
{
    public static class ConvertStringToDateTime
    {
        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            persianDate = persianDate.ToEnglishNumber();
            var year = Convert.ToInt32(persianDate.Substring(0, 4));
            var month = Convert.ToInt32(persianDate.Substring(5, 2));
            var day = Convert.ToInt32(persianDate.Substring(8, 2));
            return new DateTime(year, month, day, new PersianCalendar());
        }
    }
}
