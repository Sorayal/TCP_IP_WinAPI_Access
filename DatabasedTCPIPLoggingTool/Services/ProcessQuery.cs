using System.Management;

namespace TCPIPLoggingWinAPI.Services
{
    public static class ProcessQuery
    {
        public static string GetPath(string processItem)
        {
            using ManagementObjectSearcher MOS = new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process");
            foreach (ManagementObject P_Data in MOS.Get())
            {
                if (P_Data["ExecutablePath"] != null && P_Data["ProcessID"].ToString() == processItem)

                {
                    return P_Data["ExecutablePath"].ToString();
                }
            }
            return "";
        }
    }
}
