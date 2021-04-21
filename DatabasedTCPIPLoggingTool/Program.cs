using TCPIPLoggingWinAPI.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPIPLoggingWinAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var udp = ExtUdpTable.GetAllUDPConnections();
            var udp6 = ExtUdpTable.GetAllUDPv6Connections();
            var tcp6 = ExtTcpTable.GetAllTCPv6Connections();
            var tcp = ExtTcpTable.GetAllTCPConnections();

            Console.ReadLine();
            //Endpoint to attach an application to call and to process the results 
        }
    }
}
