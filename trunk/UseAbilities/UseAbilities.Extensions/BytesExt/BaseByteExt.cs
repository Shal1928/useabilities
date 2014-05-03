using System.Text;

namespace UseAbilities.Extensions.BytesExt
{
    public static class BaseByteExt
    {
        public static string GetStringUTF8(this byte[] bytes)
        {
            return bytes.GetString(Encoding.UTF8);
        }

        public static string GetString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
    }
}
