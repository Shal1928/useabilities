using System;
using System.Collections.Generic;
using System.Linq;

namespace UseAbilities.Extensions.DateTimeExt
{
    public static class DateTimeMonthExt
    {
        /// <summary>
        /// Get absolute weeks (with days from another month)
        /// </summary>
        /// <param name="dt">Date</param>
        /// <param name="startOfWeek">First day of week</param>
        /// <returns>Return collection with absolute weeks (with days from another month) with collection days</returns>
        public static List<List<DateTime>> GetWeeksAndDaysOfMonth(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
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

        /// <summary>
        /// Get dates first days of weeks of target (current) month
        /// </summary>
        /// <param name="dt">Date</param>
        /// <param name="startOfWeek">First day of week</param>
        /// <returns>Collection of dates first days of week of target month</returns>
        public static List<DateTime> GetWeeksOfMonth(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            return dt.GetWeeksAndDaysOfMonth().Select(week => week.First()).ToList();
        }

        public static int GetWeekNumberOfMonth(this DateTime dt)
        {
            var weeks = dt.GetWeeksAndDaysOfMonth();
            return weeks.Where(week => week.Contains(dt.Date)).Select(week => weeks.IndexOf(week) + 1).FirstOrDefault();
        }

        public static int GetWeekNumberOfMonth(this DateTime dt, DateTime targetDt)
        {
            var weeks = targetDt.GetWeeksAndDaysOfMonth();
            return weeks.Where(week => week.Contains(dt.Date)).Select(week => weeks.IndexOf(week) + 1).FirstOrDefault();
        }

        public static List<DateTime> GetDaysOfMonth(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var result = new List<DateTime>();

            var firstDay = dt.GetStartOfMonth();
            var lastDay = dt.GetEndOfMonth();
            //for (var day = firstDay; day <= lastDay; day.AddDays(1))
            //    result.Add(day);



            var day = dt.GetStartOfMonth();

            while (day < dt.GetEndOfMonth())
            {
                result.Add(day);
                day = day.AddDays(1);
            }

            return result;
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
