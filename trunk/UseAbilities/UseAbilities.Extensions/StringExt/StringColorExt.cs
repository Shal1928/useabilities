using System;
using System.Drawing;
using DColor = System.Drawing.Color;
using MColor = System.Windows.Media.Color;

namespace UseAbilities.Extensions.StringExt
{
    public static class StringColorExt
    {
        public static DColor ToDrawingColor(this string colorName)
        {
            if (String.IsNullOrWhiteSpace(colorName)) throw new ArgumentException("colorName");
            return ColorTranslator.FromHtml(colorName);
        }

        public static MColor ToMediaColor(this string colorName)
        {
            if (String.IsNullOrWhiteSpace(colorName)) throw new ArgumentException("colorName");
            var dColor = ColorTranslator.FromHtml(colorName);
            return MColor.FromArgb(dColor.A, dColor.R, dColor.G, dColor.B);
        }
    }
}
