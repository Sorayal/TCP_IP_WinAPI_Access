namespace TCPIPLoggingWinAPI.Entities;

public class NetworkConnection
{
    public string Protocol { get; set; } // TCP oder UDP
    public string LocalAddress { get; set; }
    public int LocalPort { get; set; }
    public string RemoteAddress { get; set; }
    public int RemotePort { get; set; }
    public string State { get; set; }
    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
    public string ProcessPath { get; set; }
    public string Direction { get; set; } // in / out / loopback
    public DateTime Timestamp { get; set; }
}