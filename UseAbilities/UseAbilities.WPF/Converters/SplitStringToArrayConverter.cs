using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.ObjectExt;
using UseAbilities.Extensions.StringExt;
using UseAbilities.WPF.Converters.Base;

namespace UseAbilities.WPF.Converters
{
    //[ValueConversion(typeof(string[]), typeof(string))]
    public class SplitStringToArrayConverter : ConverterBase<SplitStringToArrayConverter>
    {
        public SplitStringToArrayConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var arrValue = value as ICollection<string>;
            if (arrValue.IsNull()) return value;
            if (!(parameter is string)) return value;
            var sb = new StringBuilder();
            var i = 0;
            foreach (var val in arrValue)
            {
                i++;
                sb.Append(val);
                if (i < arrValue.Count) sb.Append(parameter);
            }
            return sb.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;
            if (strValue.IsNullOrEmptyOrSpaces()) return value;
            if (!(parameter is string)) return value;
            var splitter = ((string)parameter)[0];

            return strValue.Split(splitter).ToList();
        }
    }
}
