using System;
using System.Runtime.InteropServices;
using UseAbilities.System.Core;
using UseAbilities.System.Core.Enums;
using UseAbilities.System.Core.Structs;
using UseAbilities.System.Structs;

namespace UseAbilities.System.Info
{
    public static class OSdetails
    {
        #region Common Details

        public static bool IsVistaOrHigher
        {
            get
            {
                return Version >= SystemVersion.Vista;
            }
        }

        public static bool IsXPorLower
        {
            get
            {
                return Version <= SystemVersion.XP;
            }
        }

        public static bool IsServer
        {
            get
            {
                return GetProductType() != ProductType.WorkStation;
            }
        }

        #endregion

        #region Accurate Details

        public static bool IsWin95
        {
            get
            {
                return Version == SystemVersion.Win95;
            }
        }

        public static bool IsWin98
        {
            get
            {
                return Version == SystemVersion.Win98;
            }
        }

        public static bool IsWinMe
        {
            get
            {
                return Version == SystemVersion.Me;
            }
        }

        public static bool IsWin2000
        {
            get
            {
                return Version == SystemVersion.Win2000;
            }
        }

        public static bool IsWinXP
        {
            get
            {
                return Version == SystemVersion.XP;
            }
        }

        public static bool IsWinServer2003
        {
            get
            {
                return Version == SystemVersion.Server2003 && User32.GetSystemMetrics(SystemMetrics.ServerR2) == 0;
            }
        }

        public static bool IsWinServer2003R2
        {
            get
            {
                return Version == SystemVersion.Server2003 && User32.GetSystemMetrics(SystemMetrics.ServerR2) != 0;
            }
        }

        public static bool IsWinVista
        {
            get
            {
                return Version == SystemVersion.Vista && !IsServer;
            }
        }

        public static bool IsWinServer2008
        {
            get
            {
                return Version == SystemVersion.Server2008 && IsServer;
            }
        }

        public static bool IsWinServer2008R2
        {
            get
            {
                return Version == SystemVersion.Server2008R2 && IsServer;
            }
        }

        public static bool IsWin7
        {
            get
            {
                return Version == SystemVersion.Win7 && !IsServer;
            }
        }

        public static bool IsWin8
        {
            get
            {
                return Version == SystemVersion.Win8;
            }
        }

        #endregion

        #region Fileds

        private static readonly Version Version = Environment.OSVersion.Version;

        #endregion

        #region Private Methods

        private static ProductType GetProductType()
        {
            var osVersionInfo = new OSVERSIONINFOEX
            {
                dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX))
            };

            Kernel32.GetVersionEx(ref osVersionInfo);
            return (ProductType)osVersionInfo.wProductType;
        }

        #endregion
    }

    //Operating system          Version number          dwMajorVersion          dwMinorVersion          OSVERSIONINFOEX.wProductType
    //Windows 7	                6.1	                    6	                    1	                    VER_NT_WORKSTATION
    //Windows Server 2008 R2	6.1	                    6	                    1	                    != VER_NT_WORKSTATION
    //Windows Server 2008	    6.0	                    6	                    0	                    != VER_NT_WORKSTATION
    //Windows Vista	            6.0	                    6	                    0	                    VER_NT_WORKSTATION
    //Windows Server 2003 R2	5.2	                    5	                    2	                    GetSystemMetrics(SM_SERVERR2) != 0
    //Windows Server 2003	    5.2	                    5	                    2	                    GetSystemMetrics(SM_SERVERR2) == 0
    //Windows XP	            5.1	                    5	                    1	                    Not applicable
    //Windows 2000	            5.0	                    5	                    0	                    Not applicable
}
