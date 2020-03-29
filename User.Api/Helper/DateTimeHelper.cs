using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        public static string ToDateTimeString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
