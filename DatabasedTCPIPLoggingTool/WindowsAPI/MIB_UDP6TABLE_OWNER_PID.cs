using System.Runtime.InteropServices;

namespace TCPIPLoggingWinAPI.WindowsAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDP6TABLE_OWNER_PID
    {
        public uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        public MIB_UDP6ROW_OWNER_PID[] udptable;
    }
}
