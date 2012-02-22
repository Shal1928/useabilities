using System;
using System.Globalization;
using System.Windows.Data;

namespace UseAbilities.WPF.Converters.Test
{
    public class TestMultiConverter : IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[] { value };
        }

        #endregion
    }
}
