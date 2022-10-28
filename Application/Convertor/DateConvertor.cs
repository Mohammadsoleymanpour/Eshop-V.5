using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Convertor
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value)+ "/" +pc.GetHour(value)+":"+pc.GetMinute(value).ToString();

        }
    }
}
