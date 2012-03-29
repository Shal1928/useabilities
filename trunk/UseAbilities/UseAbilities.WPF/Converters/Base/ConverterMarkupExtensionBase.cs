using System;
using System.Windows.Markup;

namespace UseAbilities.WPF.Converters.Base
{
    public abstract class ConverterMarkupExtensionBase<T> : MarkupExtension where T : class, new()
    {
        #region MarkupExtension members

        private static T _converter;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }

        #endregion
    }
}
