using System.Runtime.InteropServices;
using UseAbilities.System.Core.Enums;

namespace UseAbilities.System.Core
{
    internal static class User32
    {
        private const string DllName = "user32.dll";

        [DllImport(DllName, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetSystemMetrics(int nIndex);

        public static int GetSystemMetrics(SystemMetrics systemMetrics)
        {
            return GetSystemMetrics((int) systemMetrics);
        }
    }
}
