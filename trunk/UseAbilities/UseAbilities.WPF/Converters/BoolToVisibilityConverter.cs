using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using UseAbilities.WPF.Converters.Base;

namespace UseAbilities.WPF.Converters
{
    /// <summary>
    /// Convert bool to Visibility, 
    /// parameter determines the choice between the Visibility.Hidden and Visibility.Collapsed. 
    /// Use anyhow for Visibility.Collapsed.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : ConverterBase<BoolToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) throw new ArgumentException("value is not bool");
            var boolValue = (bool) value;

            return boolValue ? 
                   Visibility.Visible :
                   parameter != null ? Visibility.Collapsed : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility)) throw new ArgumentException("value is not Visibility");
            return (Visibility)value == Visibility.Visible;
        }
    }
}
