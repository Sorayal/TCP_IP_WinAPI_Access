using TCPIPLoggingWinAPI.Services;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace TCPIPLoggingWinAPI.WindowsAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPROW_OWNER_PID
    {
        public uint localAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] localPort;
        public uint owningPid;



        public uint ProcessId
        {
            get { return owningPid; }
        }

        public IPAddress LocalAddress
        {
            get { return new IPAddress(localAddr); }
        }

        public ushort LocalPort
        {
            get
            {
                return BitConverter.ToUInt16(new byte[2] { localPort[1], localPort[0] }, 0);
            }
        }

        public string Path
        {
            get { return (string)ProcessQuery.GetPath(ProcessId.ToString()); }
        }

        public string ProcessName
        {

            get
            {
                string Processname = "";
                try
                {
                    return Process.GetProcessById(Convert.ToInt32(owningPid)).ToString();
                }
                catch
                {
                    return Processname;
                };
            }
        }
    }
}
