using System;
using System.Windows;
using System.Windows.Markup;
using UseAbilities.WPF.Extensions;

namespace UseAbilities.WPF.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(Style))]
    public class MultiStyle : MarkupExtension
    {
        private string[] _resourceKeys;
        
        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="inputResourceKeys">The constructor input should be a string consisting of one or more style names separated by spaces.</param>
        public MultiStyle(string inputResourceKeys)
        {
            if (inputResourceKeys == null)
                throw new ArgumentNullException("inputResourceKeys");
            

            _resourceKeys = inputResourceKeys.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (_resourceKeys.Length == 0)
                throw new ArgumentException("No input resource keys specified.");
        }

        /// <summary>
        /// Returns a style that merges all styles with the keys specified in the constructor.
        /// </summary>
        /// <param name="serviceProvider">The service provider for this markup extension.</param>
        /// <returns>A style that merges all styles with the keys specified in the constructor.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var resultStyle = new Style();

            foreach (var currentResourceKey in _resourceKeys)
            {
                var currentStyle = new StaticResourceExtension(currentResourceKey).ProvideValue(serviceProvider) as Style;

                if (currentStyle == null)
                    throw new InvalidOperationException("Could not find style with resource key " + currentResourceKey + ".");
                
                resultStyle.Merge(currentStyle);
            }

            return resultStyle;
        }
    }
}
