using System;

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

        public static DateTime FirstDayLastWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.AddDays(-7).StartOfWeek(startOfWeek);
        }

        public static DateTime FirstDayCurrentWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.StartOfWeek(startOfWeek);
        }

        public static bool ThisDateLastWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date >= DateTime.Now.FirstDayLastWeek(startOfWeek).Date && dt.Date < DateTime.Now.FirstDayCurrentWeek(startOfWeek).Date;
        }

        public static bool ThisDateCurrentWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date >= DateTime.Now.FirstDayCurrentWeek(startOfWeek).Date;
        }

        public static bool ThisDateYesterday(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date == DateTime.Now.AddDays(-1).Date;
        }

        public static bool ThisDateToday(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.Date == DateTime.Now.Date;
        }
    }
}
