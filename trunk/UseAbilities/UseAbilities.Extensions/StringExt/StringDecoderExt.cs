using System.Text;

namespace UseAbilities.Extensions.StringExt
{
    public static class StringDecoderExt
    {
        public static string ToUTF8(this string str)
        {
            var encoder = Encoding.UTF8;
            var utfBytes = Encoding.UTF8.GetBytes(str);
            return encoder.GetString(utfBytes, 0, utfBytes.Length);
        }

        public static string ToUTF32(this string str)
        {
            var encoder = Encoding.UTF32;
            var utfBytes = encoder.GetBytes(str);
            return encoder.GetString(utfBytes, 0, utfBytes.Length);
        }
    }
}
