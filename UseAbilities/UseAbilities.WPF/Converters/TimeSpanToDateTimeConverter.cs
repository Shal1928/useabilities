using System;
using System.Globalization;
using System.Windows.Data;
using UseAbilities.WPF.Converters.Base;

namespace UseAbilities.WPF.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(DateTime))]
    public class TimeSpanToDateTimeConverter : ConverterBase<TimeSpanToDateTimeConverter>
    {
        public TimeSpanToDateTimeConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime)) throw new ArgumentException("value is not DateTime");
            var dateTimeValue = (DateTime)value;
            return new TimeSpan(0, dateTimeValue.Hour, dateTimeValue.Minute, dateTimeValue.Second, dateTimeValue.Millisecond);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan)) throw new ArgumentException("value is not TimeSpan");
            var timeSpanValue = (TimeSpan)value;
            return new DateTime(1,1,1, timeSpanValue.Hours, timeSpanValue.Minutes, timeSpanValue.Seconds, timeSpanValue.Milliseconds);
        }
    }

    [ValueConversion(typeof(DateTime), typeof(TimeSpan))]
    public class DateTimeTimeSpanConverter : ConverterBase<DateTimeTimeSpanConverter>
    {
        public DateTimeTimeSpanConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan)) throw new ArgumentException("value is not TimeSpan");
            var timeSpanValue = (TimeSpan)value;
            return new DateTime(1, 1, 1, timeSpanValue.Hours, timeSpanValue.Minutes, timeSpanValue.Seconds, timeSpanValue.Milliseconds);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime)) throw new ArgumentException("value is not DateTime");
            var dateTimeValue = (DateTime)value;
            return new TimeSpan(0, dateTimeValue.Hour, dateTimeValue.Minute, dateTimeValue.Second, dateTimeValue.Millisecond);
        }
    }
}
