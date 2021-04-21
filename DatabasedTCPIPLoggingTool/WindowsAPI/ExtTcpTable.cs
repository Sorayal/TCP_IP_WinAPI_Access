using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TCPIPLoggingWinAPI.WindowsAPI
{
    public static class ExtTcpTable
    {
        private const int AF_INET = 2;    // For IP_v4
        private const int AF_INET6 = 23;  // For IP_v6 

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, uint reserved = 0);


        public static List<MIB_TCPROW_OWNER_PID> GetAllTCPConnections()
        {
            return GetTCPConnections<MIB_TCPROW_OWNER_PID, MIB_TCPTABLE_OWNER_PID>(AF_INET);
        }


        public static List<MIB_TCP6ROW_OWNER_PID> GetAllTCPv6Connections()
        {
            return GetTCPConnections<MIB_TCP6ROW_OWNER_PID, MIB_TCP6TABLE_OWNER_PID>(AF_INET6);
        }



        private static List<IPR> GetTCPConnections<IPR, IPT>(int ipVersion)//IPR = Row Type, IPT = Table Type
        {
            IPR[] tableRows;
            int buffSize = 0;

            var dwNumEntriesField = typeof(IPT).GetField("dwNumEntries");
            _ = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, ipVersion, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_All);
            IntPtr tcpTablePtr = Marshal.AllocHGlobal(buffSize);

            try
            {
                uint ret = GetExtendedTcpTable(tcpTablePtr, ref buffSize, true, ipVersion, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_All);
                if (ret != 0)
                    return new List<IPR>();

                IPT table = (IPT)Marshal.PtrToStructure(tcpTablePtr, typeof(IPT));
                int rowStructSize = Marshal.SizeOf(typeof(IPR));
                uint numEntries = (uint)dwNumEntriesField.GetValue(table);

                tableRows = new IPR[numEntries];

                IntPtr rowPtr = (IntPtr)((long)tcpTablePtr + 4);
                for (int i = 0; i < numEntries; i++)
                {
                    IPR tcpRow = (IPR)Marshal.PtrToStructure(rowPtr, typeof(IPR));
                    tableRows[i] = tcpRow;
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
                Marshal.FreeHGlobal(tcpTablePtr);
            }
        }
    }
}
