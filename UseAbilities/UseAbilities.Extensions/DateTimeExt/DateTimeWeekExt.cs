using System;
using System.Collections.Generic;

namespace UseAbilities.Extensions.DateTimeExt
{
    public static class DateTimeWeekExt
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime FirstDayOfWeekByDate(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.StartOfWeek(startOfWeek);
        }

        public static List<DateTime> GetWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var week = new List<DateTime>();
            for (var i = 0; i < 7; i++)
                week.Add(dt.FirstDayOfWeekByDate(startOfWeek).AddDays(i));

            return week;
        }
        

        public static DateTime FirstDayCurrentWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return DateTime.Now.StartOfWeek(startOfWeek);
        }

        public static DateTime FirstDayLastWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return DateTime.Now.AddDays(-7).StartOfWeek(startOfWeek);
        }



        public static bool ThisDateCurrentWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date >= DateTime.Now.FirstDayCurrentWeek(startOfWeek).Date;
        }

        public static bool ThisDateLastWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date >= DateTime.Now.FirstDayLastWeek(startOfWeek).Date && dt.Date < DateTime.Now.FirstDayCurrentWeek(startOfWeek).Date;
        }


        public static bool ThisDateToday(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date == DateTime.Now.Date;
        }

        public static bool ThisDateYesterday(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date == DateTime.Now.AddDays(-1).Date;
        }
    }
}
