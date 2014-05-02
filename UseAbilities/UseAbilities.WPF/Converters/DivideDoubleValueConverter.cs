using System;
using System.Globalization;
using System.Windows.Data;
using UseAbilities.WPF.Converters.Base;

namespace UseAbilities.WPF.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class DivideDoubleValueConverter : ConverterBase<DivideDoubleValueConverter>
    {
        public DivideDoubleValueConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double)) throw new ArgumentException("Value is not double!");
            var doubleValue = (double)value;
            return doubleValue / 2;
        }
    }
}
