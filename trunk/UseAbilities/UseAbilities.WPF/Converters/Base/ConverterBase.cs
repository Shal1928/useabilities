using System;
using System.Globalization;
using System.Windows.Data;

namespace UseAbilities.WPF.Converters.Base
{
    public abstract class ConverterBase<T> : ConverterMarkupExtensionBase<T>, IValueConverter where T : class, new()
    {
        /// <summary>
        /// Must be implemented in inheritor.
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Override if needed.
        /// </summary>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }       
    }

    //<Label Content="{Binding Path=Date, Converter={converters:DateConverter}}" />
}
