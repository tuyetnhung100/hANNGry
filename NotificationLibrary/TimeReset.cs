using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary
{
    public static class TimeReset
    {
       
            public static DateTime ResetTimeToStartOfDay(this DateTime dateTime)
            {
                return new DateTime(
                   dateTime.Year,
                   dateTime.Month,
                   dateTime.Day,
                   0, 0, 0, 0);
            }

            public static DateTime ResetTimeToEndOfDay(this DateTime dateTime)
            {
                return new DateTime(
                   dateTime.Year,
                   dateTime.Month,
                   dateTime.Day,
                   23, 59, 59, 999);
            }
        
    }
}
