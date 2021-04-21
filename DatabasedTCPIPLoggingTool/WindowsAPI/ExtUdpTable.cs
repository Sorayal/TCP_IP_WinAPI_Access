using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TCPIPLoggingWinAPI.WindowsAPI
{
    public static class ExtUdpTable
    {
        public const int AF_INET = 2;    // Address Family for IPv4
        public const int AF_INET6 = 23;  // Address Family for IPv6

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int dwOutBufLen, bool sort, int ipVersion, UDP_TABLE_CLASS tblClass, int reserved = 0);

        public static List<MIB_UDPROW_OWNER_PID> GetAllUDPConnections()
        {
            return GetUDPConnections<MIB_UDPROW_OWNER_PID, MIB_UDPTABLE_OWNER_PID>(AF_INET);
        }


        public static List<MIB_UDP6ROW_OWNER_PID> GetAllUDPv6Connections()
        {
            return GetUDPConnections<MIB_UDP6ROW_OWNER_PID, MIB_UDP6TABLE_OWNER_PID>(AF_INET6);
        }


        private static List<IPR> GetUDPConnections<IPR, IPT>(int ipVersion)//IPR = Row Type, IPT = Table Type
        {
            IPR[] tableRows;
            int buffSize = 0;

            var dwNumEntriesField = typeof(IPT).GetField("dwNumEntries");
            _ = GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, ipVersion, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID);
            IntPtr udpTablePtr = Marshal.AllocHGlobal(buffSize);

            try
            {
                uint ret = GetExtendedUdpTable(udpTablePtr, ref buffSize, true, ipVersion, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID);
                if (ret != 0)
                    return new List<IPR>();

                IPT table = (IPT)Marshal.PtrToStructure(udpTablePtr, typeof(IPT));
                int rowStructSize = Marshal.SizeOf(typeof(IPR));
                uint numEntries = (uint)dwNumEntriesField.GetValue(table);

                tableRows = new IPR[numEntries];

                IntPtr rowPtr = (IntPtr)((long)udpTablePtr + 4);
                for (int i = 0; i < numEntries; i++)
                {
                    IPR udpRow = (IPR)Marshal.PtrToStructure(rowPtr, typeof(IPR));
                    tableRows[i] = udpRow;
                    rowPtr = (IntPtr)((long)rowPtr + rowStructSize);
                }
                return tableRows.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<IPR>();
                //TODO: return a message without using Console
            }
            finally
            {
                Marshal.FreeHGlobal(udpTablePtr);
            }
        }
    }
}
