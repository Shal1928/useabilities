using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace UseAbilities.WPF.Bindings
{
    /// <summary>
    /// Binding="{SelfBinding AnyProperty}" instead of Binding="{Binding AnyProperty, RelativeSource={RelativeSource Self}}"
    /// Author: Алексей for7raid http://habrahabr.ru/post/84263/
    /// </summary>
    public class SelfBinding : MarkupExtension
    {
        public SelfBinding()
        {
            //
        }

        public SelfBinding(string path)
        {
            Path = path;
        }

        public string Path
        {
            get; 
            set;
        }

        public IValueConverter Converter
        {
            get; 
            set;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (string.IsNullOrEmpty(Path)) throw new ArgumentNullException("Path", "The Path can not be null");

                var providerValuetarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

                var targetObject = (FrameworkElement)providerValuetarget.TargetObject;
                var sourceProperty = targetObject.GetType().GetProperty(Path);
                var dependencyProperty = (DependencyProperty)providerValuetarget.TargetProperty;
                var targetProperty = targetObject.GetType().GetProperty(dependencyProperty.Name);
                var value = sourceProperty.GetValue(targetObject, null);

                return Converter != null ? 
                       Converter.Convert(value, targetProperty.PropertyType, null, Thread.CurrentThread.CurrentCulture) : 
                       value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
