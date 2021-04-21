# TCP_IP_WinAPI_Access
Win32_IPHelper_API_Access

This project uses the Win32 IPhelper API to retrieve a snapshot about the current network traffic. It works for IPv4 and IPv6 and is divided into UDP and TCP protocols.
The classes ProcessQuery and TransferDirection were added by me to figure out what processname and path is behind a PID and if it´s an incoming or outgoing connection (works only for TCP, which is a stateful protocol). 

This project isn´t complete, but it can be used in combination with a GUI application to show visually the retrieved informations.

Language: C'
Framework: .Net Framework 4.7.2

Big thanks goes to the contributors from [Pinvoke.Net](https://www.pinvoke.net/default.aspx/iphlpapi/GetExtendedTcpTable.html). 
You will find the official documentation [here](https://docs.microsoft.com/en-us/windows/win32/api/iphlpapi/nf-iphlpapi-getextendedudptable) and [here](https://docs.microsoft.com/en-us/windows/win32/api/iphlpapi/nf-iphlpapi-getextendedtcptable) from Microsoft. 
