using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Code.UseAbilities.WPF.Converters.Test
{
    public class TestConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
