using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartClassroomPCClient
{
    static class LocalCommand
    {
        // From http://www.cnblogs.com/sosoft/p/3459826.html


        private const int SE_PRIVILEGE_ENABLED = 0x00000002;
        private const int TOKEN_QUERY = 0x00000008;
        private const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        private const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa376868%28v=vs.85%29.aspx
        private const uint WxShutdown = 0x00000001;
        private const uint EwxRestartapps = 0x00000040;
        private const uint EwxReboot = 0x00000002;
        private const uint EwxPoweroff = 0x00000008;
        private const uint EwxLogoff = 0x00000000;
        private const uint EwxHybridShutdown = 0x00400000;
        private const uint EwxForce = 0x00000004;
        private const uint EwxForceifhung = 0x00000010;


        [Flags]
        public enum ExitWindows : uint
        {
            Shutdown = WxShutdown | EwxForceifhung | EwxPoweroff,
            Reboot = EwxReboot | EwxForceifhung,
            Logoff = EwxLogoff | EwxForceifhung,
            ShutdownWin8 = WxShutdown | EwxHybridShutdown | EwxPoweroff
        }



        [Flags]
        private enum ShutdownReason : uint
        {
            MajorApplication = 0x00040000,
            MajorHardware = 0x00010000,
            MajorLegacyApi = 0x00070000,
            MajorOperatingSystem = 0x00020000,
            MajorOther = 0x00000000,
            MajorPower = 0x00060000,
            MajorSoftware = 0x00030000,
            MajorSystem = 0x00050000,
            MinorBlueScreen = 0x0000000F,
            MinorCordUnplugged = 0x0000000b,
            MinorDisk = 0x00000007,
            MinorEnvironment = 0x0000000c,
            MinorHardwareDriver = 0x0000000d,
            MinorHotfix = 0x00000011,
            MinorHung = 0x00000005,
            MinorInstallation = 0x00000002,
            MinorMaintenance = 0x00000001,
            MinorMMC = 0x00000019,
            MinorNetworkConnectivity = 0x00000014,
            MinorNetworkCard = 0x00000009,
            MinorOther = 0x00000000,
            MinorOtherDriver = 0x0000000e,
            MinorPowerSupply = 0x0000000a,
            MinorProcessor = 0x00000008,
            MinorReconfig = 0x00000004,
            MinorSecurity = 0x00000013,
            MinorSecurityFix = 0x00000012,
            MinorSecurityFixUninstall = 0x00000018,
            MinorServicePack = 0x00000010,
            MinorServicePackUninstall = 0x00000016,
            MinorTermSrv = 0x00000020,
            MinorUnstable = 0x00000006,
            MinorUpgrade = 0x00000003,
            MinorWMI = 0x00000015,
            FlagUserDefined = 0x40000000,
            FlagPlanned = 0x80000000
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll")]
        private static extern bool ExitWindowsEx(ExitWindows uFlags, ShutdownReason dwReason);

        /// <summary>
        /// 关机、重启、注销windows
        /// </summary>
        /// <param name="flag"></param>
        public static bool DoExitWindows(ExitWindows flag)
        {
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ExitWindowsEx(flag, ShutdownReason.MajorOther);
            return true;
        }

        public static bool Shutdown()
        {
            // http://blog.csdn.net/yl2isoft/article/details/17336425
            Version currentVersion = Environment.OSVersion.Version;
            Version compareToVersion = new Version("6.2");
            if (currentVersion.CompareTo(compareToVersion) >= 0)
            {
                // win8及其以上
                return DoExitWindows(ExitWindows.ShutdownWin8);
            }
            else
            {
                return DoExitWindows(ExitWindows.Shutdown);
            }
        }
        public static bool Restart()
        {
            return DoExitWindows(ExitWindows.Reboot);
        }
        public static bool Logout()
        {
            return DoExitWindows(ExitWindows.Logoff);
        }


        public class GetOperatorVersion
        {
            // http://blog.csdn.net/yl2isoft/article/details/17336329

            //C#判断操作系统是否为Windows98
            public static bool IsWindows98 => (Environment.OSVersion.Platform == PlatformID.Win32Windows) && (Environment.OSVersion.Version.Minor == 10) && (Environment.OSVersion.Version.Revision.ToString() != "2222A");
            //C#判断操作系统是否为Windows98第二版
            public static bool IsWindows98Second => (Environment.OSVersion.Platform == PlatformID.Win32Windows) && (Environment.OSVersion.Version.Minor == 10) && (Environment.OSVersion.Version.Revision.ToString() == "2222A");
            //C#判断操作系统是否为Windows2000
            public static bool IsWindows2000 => (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor == 0);
            //C#判断操作系统是否为WindowsXP
            public static bool IsWindowsXP => (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor == 1);
            //C#判断操作系统是否为Windows2003
            public static bool IsWindows2003 => (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor == 2);
            //C#判断操作系统是否为WindowsVista
            public static bool IsWindowsVista => (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 0);
            //C#判断操作系统是否为Windows7
            public static bool IsWindows7 => (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor == 1);
            //C#判断操作系统是否为Unix
            public static bool IsUnix => Environment.OSVersion.Platform == PlatformID.Unix;
        }

    }
}
