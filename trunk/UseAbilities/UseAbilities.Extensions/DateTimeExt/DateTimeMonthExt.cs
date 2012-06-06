using System;
using System.Collections.Generic;
using System.Linq;

namespace UseAbilities.Extensions.DateTimeExt
{
    public static class DateTimeMonthExt
    {
        public static List<List<DateTime>> GetWeeksAndDaysByMonth(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var weeks = new List<List<DateTime>>();
            var day = dt.GetStartOfMonth();

            while (day < dt.GetEndOfMonth().LastDayOfWeekByDate())
            {
                var week = new List<DateTime>();
                for (var i = 0; i < 7; i++)
                    week.Add(day.FirstDayOfWeekByDate(startOfWeek).AddDays(i));
                
                day = week.Last().Date.AddDays(1);
                weeks.Add(week);
            }

            return weeks;
        }

        public static List<DateTime> GetWeeksByMonth(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var weeksAndDays = dt.GetWeeksAndDaysByMonth();
            return weeksAndDays.Select(week => week.First()).ToList();
        }

        public static DateTime GetStartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59, 999);
        }
    }
}
