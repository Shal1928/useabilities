using System;
using System.Collections.Generic;

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

        public static List<DateTime> GetMonth(this DateTime dt)
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
    }
}
