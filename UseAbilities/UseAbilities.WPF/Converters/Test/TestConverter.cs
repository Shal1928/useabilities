using System;
using System.Globalization;
using UseAbilities.WPF.Converters.Base;

namespace UseAbilities.WPF.Converters.Test
{
    public class TestConverter : ConvertorBase<TestConverter>
    {
        #region Ovveride of ConvertorBase

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
