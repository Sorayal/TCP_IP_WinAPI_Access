using System.Net;
using System.Net.NetworkInformation;

namespace TCPIPLoggingWinAPI.Services
{
    public static class TransferDirection
    {
        public static string GetDirection(string tcpState, int localPort, IPAddress remoteAddress)
        {
            if (IPAddress.IsLoopback(remoteAddress))
            {
                return "Loopback Address";
            }

            IPEndPoint[] localTcpListener;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            localTcpListener = properties.GetActiveTcpListeners();

            for (int i = 0; i < localTcpListener.Length; i++)
            {
                if (localTcpListener[i].Port == localPort && tcpState == "Established")
                {
                    return "in";
                }
            }
            return "out";
        }
    }
}
