namespace TCPIPLoggingWinAPI.WindowsAPI
{
    public enum MIB_TCP_STATE
    {
        Closed = 1,
        Listen,
        SynSent,
        SynRcvd,
        Established,
        FinWait1,
        FinWait2,
        CloseWait,
        Closing,
        LastAck,
        TimeWait,
        DeleteTcb
    }
}
