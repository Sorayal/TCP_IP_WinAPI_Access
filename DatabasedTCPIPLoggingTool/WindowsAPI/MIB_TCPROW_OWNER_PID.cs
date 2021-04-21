using TCPIPLoggingWinAPI.Services;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace TCPIPLoggingWinAPI.WindowsAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPROW_OWNER_PID
    {
        public uint state;
        public uint localAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] localPort;
        public uint remoteAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] remotePort;
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

        public IPAddress RemoteAddress
        {
            get { return new IPAddress(remoteAddr); }
        }

        public ushort RemotePort
        {
            get
            {
                return BitConverter.ToUInt16(new byte[2] { remotePort[1], remotePort[0] }, 0);
            }
        }

        public MIB_TCP_STATE State
        {
            get { return (MIB_TCP_STATE)state; }
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

        public string TransferDirection
        {
            get
            {
                try
                {
                    return Services.TransferDirection.GetDirection(State.ToString(),
                                                                      Convert.ToInt32(LocalPort),
                                                                      RemoteAddress);
                }
                catch
                {
                    return "Error";
                }
            }
        }
    }
}
