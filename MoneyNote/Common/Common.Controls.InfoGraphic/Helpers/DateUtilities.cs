using System;

namespace Common.Controls.InfoGraphic.Helpers
{
    /// <summary>
    /// Common DateTime Methods. 
    /// Author: Michael Ceranski
    /// http://www.codeproject.com/KB/cs/csdatetimelibrary.aspx
    /// 
    /// With DateTime extension from: 
    /// http://stackoverflow.com/questions/38039/how-can-i-get-the-datetime-for-the-start-of-the-week/38064#38064
    /// </summary>

    public enum Quarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public static class DateUtilities
    {
        #region Public Properties

        private static DayOfWeek _firstDayWeek = DayOfWeek.Monday;
        public static DayOfWeek FirstDayWeek
        {
            get { return _firstDayWeek; }
            set { _firstDayWeek = value; }
        }

        #endregion

        #region DateTime Extensions

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0) diff += 7;
            return dt.AddDays(-1 * diff).Date;
        }

        #endregion

        #region Quarters

        public static DateTime GetStartOfQuarter(int year, Quarter qtr)
        {
            switch (qtr)
            {
                case Quarter.First:
                    return new DateTime(year, 1, 1, 0, 0, 0, 0);
                case Quarter.Second:
                    return new DateTime(year, 4, 1, 0, 0, 0, 0);
                case Quarter.Third:
                    return new DateTime(year, 7, 1, 0, 0, 0, 0);
                default:
                    return new DateTime(year, 10, 1, 0, 0, 0, 0);
            }
        }

        public static DateTime GetEndOfQuarter(int year, Quarter qtr)
        {
            switch (qtr)
            {
                case Quarter.First:
                    return new DateTime(year, 3, DateTime.DaysInMonth(year, 3), 23, 59, 59, 999);
                case Quarter.Second:
                    return new DateTime(year, 6, DateTime.DaysInMonth(year, 6), 23, 59, 59, 999);
                case Quarter.Third:
                    return new DateTime(year, 9, DateTime.DaysInMonth(year, 9), 23, 59, 59, 999);
                default:
                    return new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
            }
        }

        public static Quarter GetQuarter(Month month)
        {
            // 1st Quarter = January 1 to March 31
            if (month <= Month.March) return Quarter.First;

            // 2nd Quarter = April 1 to June 30
            if ((month >= Month.April) && (month <= Month.June)) return Quarter.Second;

            // 3rd Quarter = July 1 to September 30
            if ((month >= Month.July) && (month <= Month.September)) return Quarter.Third;

            return Quarter.Fourth;
        }

        public static DateTime GetEndOfLastQuarter()
        {
            return (Month)DateTime.Now.Month <= Month.March
                       ? GetEndOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth)
                       : GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetStartOfLastQuarter()
        {
            return (Month)DateTime.Now.Month <= Month.March
                       ? GetStartOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth)
                       : GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetStartOfCurrentQuarter()
        {
            return GetStartOfQuarter(DateTime.Now.Year,
                   GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetEndOfCurrentQuarter()
        {
            return GetEndOfQuarter(DateTime.Now.Year,
                   GetQuarter((Month)DateTime.Now.Month));
        }
        #endregion

        #region Weeks
        public static DateTime GetStartOfLastWeek()
        {
            //var daysToSubtract = (int)DateTime.Now.DayOfWeek;
            //var dt = DateTime.Now.Subtract(TimeSpan.FromDays(daysToSubtract)).StartOfWeek(FirstDayWeek);
            var dt = DateTime.Now.Subtract(TimeSpan.FromMinutes(10079)).StartOfWeek(FirstDayWeek);
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfLastWeek()
        {
            var dt = GetStartOfLastWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        public static DateTime GetStartOfCurrentWeek()
        {
            //var daysToSubtract = (int)DateTime.Now.DayOfWeek - 7;
            //10080 minutes in week 10079
            //var dt = DateTime.Now.Subtract(TimeSpan.FromDays(daysToSubtract)).StartOfWeek(FirstDayWeek);
            //1439 minutes = 23:59:00
            var dt = DateTime.Now.Subtract(TimeSpan.FromMinutes(1439)).StartOfWeek(FirstDayWeek);
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfCurrentWeek()
        {
            var dt = GetStartOfCurrentWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }
        #endregion

        #region Months

        public static DateTime GetStartOfMonth(Month month, int year)
        {
            return new DateTime(year, (int)month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(Month month, int year)
        {
            return new DateTime(year, (int)month,
               DateTime.DaysInMonth(year, (int)month), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastMonth()
        {
            return DateTime.Now.Month == 1
                       ? GetStartOfMonth((Month)12, DateTime.Now.Year - 1)
                       : GetStartOfMonth((Month)(DateTime.Now.Month - 1), DateTime.Now.Year);
        }

        public static DateTime GetEndOfLastMonth()
        {
            return DateTime.Now.Month == 1
                       ? GetEndOfMonth((Month)12, DateTime.Now.Year - 1)
                       : GetEndOfMonth((Month)(DateTime.Now.Month - 1), DateTime.Now.Year);
        }

        public static DateTime GetStartOfCurrentMonth()
        {
            return GetStartOfMonth((Month)DateTime.Now.Month, DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentMonth()
        {
            return GetEndOfMonth((Month)DateTime.Now.Month, DateTime.Now.Year);
        }
        #endregion

        #region Years
        public static DateTime GetStartOfYear(int year)
        {
            return new DateTime(year, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfYear(int year)
        {
            return new DateTime(year, 12,
              DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastYear()
        {
            return GetStartOfYear(DateTime.Now.Year - 1);
        }

        public static DateTime GetEndOfLastYear()
        {
            return GetEndOfYear(DateTime.Now.Year - 1);
        }

        public static DateTime GetStartOfCurrentYear()
        {
            return GetStartOfYear(DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentYear()
        {
            return GetEndOfYear(DateTime.Now.Year);
        }
        #endregion

        #region Days
        public static DateTime GetStartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
        #endregion
    }
}