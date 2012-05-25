using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.InfoGraphic.Helpers
{
    internal struct TemporalPeriod
    {
        public Dictionary<int, string> DaysOfWeek;
        public string DaysOfFirstWeek;
        public string DaysOfSecondWeek;
        public string DaysOfThirdWeek;
        public string DaysOfFourthWeek;
        public string DaysOfFifthWeek;
    }

    internal static class ActualTemporalPeriod
    {
        public static TemporalPeriod GetThisWeekDays()
        {
            var d = DateTime.Today.Day;

            return new TemporalPeriod();
        }
    }
}
