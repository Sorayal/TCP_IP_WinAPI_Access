using System.Runtime.InteropServices;

namespace TCPIPLoggingWinAPI.WindowsAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCP6TABLE_OWNER_PID
    {
        public uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        public MIB_TCP6ROW_OWNER_PID[] tcpTable;
    }
}
