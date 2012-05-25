using System;
using System.Windows;
using System.Windows.Controls;
using Common.Controls.InfoGraphic.Enums;

namespace Common.Controls.InfoGraphic.TemplateSelectors
{
    public class InfoIoCalendarTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Seven days in column header. Read, write
        /// Example: 1 Mon   2 Tue   3 Wed   4 Thu   5 Fri   6 Sat   7 Sun
        /// </summary>
        public DataTemplate WeekViewTemplate
        {
            get; 
            set;
        }

        /// <summary>
        /// Five weeks (with date periods) in column header. Readonly
        /// Example: 29–31;1–4   5–11   12–18   19–25   26–30;1–2
        /// </summary>
        public DataTemplate MonthViewTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Twelve month in column header. Readonly
        /// Example: January February March April May June July August September October November December
        /// </summary>
        public DataTemplate YearViewTemplate
        {
            get;
            set;
        }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var temporalPeriodMode = (TemporalPeriodMode)item;

            switch (temporalPeriodMode)
            {
                case TemporalPeriodMode.Day: 
                    throw new NotImplementedException();

                case TemporalPeriodMode.Week:
                    return WeekViewTemplate;

                case TemporalPeriodMode.Month:
                    return MonthViewTemplate;

                case TemporalPeriodMode.Year:
                    return YearViewTemplate;

                case TemporalPeriodMode.Age:
                    throw new NotImplementedException();

                default:
                    return WeekViewTemplate;
            }
        }

    }//class
}//namespace
