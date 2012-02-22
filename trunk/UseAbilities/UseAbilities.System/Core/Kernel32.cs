using System.Runtime.InteropServices;
using UseAbilities.System.Core.Structs;

namespace UseAbilities.System.Core
{
    internal static class Kernel32
    {
        private const string DllName = "kernel32.dll";

        [DllImport(DllName)]
        public static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);
    }
}
