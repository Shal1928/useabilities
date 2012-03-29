using System;
using System.Globalization;
using System.Windows.Data;

namespace UseAbilities.WPF.Converters.Base
{
    public abstract class MultiConverterBase<T> : ConverterMarkupExtensionBase<T>, IMultiValueConverter where T : class, new()
    {
        #region Implementation of IMultiValueConverter

        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    //Converter={converters:DateConverter}
}
