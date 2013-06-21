using System;
using System.Collections.Generic;
using System.Linq;

namespace UseAbilities.Extensions.DateTimeExt
{
    public static class DateTimeYearExt
    {
        public static List<DateTime> GetQuarterByMonth(this DateTime dt)
        {
            var year = dt.Year;
            var month = dt.Month;

            if (month <= 3) return new List<DateTime>
                                          {
                                              new DateTime(year, 1, 1),
                                              new DateTime(year, 2, 1),
                                              new DateTime(year, 3, 1)
                                          };

            if ((month >= 4) && (month <= 6)) return new List<DateTime>
                                          {
                                              new DateTime(year, 4, 1),
                                              new DateTime(year, 5, 1),
                                              new DateTime(year, 6, 1)
                                          };

            if ((month >= 7) && (month <= 9)) return new List<DateTime>
                                          {
                                              new DateTime(year, 7, 1),
                                              new DateTime(year, 8, 1),
                                              new DateTime(year, 9, 1)
                                          };

            if (month >= 10) return new List<DateTime>
                                          {
                                              new DateTime(year, 10, 1),
                                              new DateTime(year, 11, 1),
                                              new DateTime(year, 12, 1)
                                          };

            throw new Exception("A Month can not be greater than 12 and less than 1");
        }

        public static int GetQuarterNumberByMonth(this DateTime dt)
        {
            var month = dt.Month;

            if (month <= 3) return 1;

            if ((month >= 4) && (month <= 6)) return 2;

            if ((month >= 7) && (month <= 9)) return 3;

            if (month >= 10) return 4;

            throw new Exception("A Month can not be greater than 12 and less than 1");
        }

        public static List<DateTime> GetMonthes(this DateTime dt)
        {
            var year = dt.Year;

            return new List<DateTime>
                       {
                           new DateTime(year, 1, 1),
                           new DateTime(year, 2, 1),
                           new DateTime(year, 3, 1),
                           new DateTime(year, 4, 1),
                           new DateTime(year, 5, 1),
                           new DateTime(year, 6, 1),
                           new DateTime(year, 7, 1),
                           new DateTime(year, 8, 1),
                           new DateTime(year, 9, 1),
                           new DateTime(year, 10, 1),
                           new DateTime(year, 11, 1),
                           new DateTime(year, 12, 1)
                       };
        }

        public static List<List<DateTime>> GetQuarters(this DateTime dt)
        {
            var year = dt.Year;

            return new List<List<DateTime>>
                       {
                            new List<DateTime>
                                {
                                    new DateTime(year, 1, 1),
                                    new DateTime(year, 2, 1),
                                    new DateTime(year, 3, 1),
                                },
                            new List<DateTime>
                                {
                                    new DateTime(year, 4, 1),
                                    new DateTime(year, 5, 1),
                                    new DateTime(year, 6, 1),
                                },
                            new List<DateTime>
                               {
                                   new DateTime(year, 7, 1),
                                   new DateTime(year, 8, 1),
                                   new DateTime(year, 9, 1),
                               },
                            new List<DateTime>
                               {
                                   new DateTime(year, 10, 1),
                                   new DateTime(year, 11, 1),
                                   new DateTime(year, 12, 1),
                               },
                       };
        }

        public static int GetMonthNumberOfQuarter(this DateTime dt)
        {
            var quarters = dt.GetQuarters();
            return (from quarter in quarters from month in quarter where month.Month == dt.Month select quarter.IndexOf(month) + 1).FirstOrDefault();
        }

        public static List<DateTime> GetAllDaysInYear(this DateTime dt)
        {
            var allDays = new List<DateTime>();
            allDays.AddRange(dt.GetMonthes().SelectMany(month => month.GetDaysOfMonth()));

            return allDays;
        }

        public static List<List<DateTime>> SeparateByWeeks(this List<DateTime> dtCollection)
        {
            var weeks = new List<List<DateTime>>();

            foreach (var dt in dtCollection.Where(dt => !weeks.Any(w=> w.Contains(dt.Date))))
                weeks.Add(dt.GetWeek());

            return weeks;
        }
    }
}
