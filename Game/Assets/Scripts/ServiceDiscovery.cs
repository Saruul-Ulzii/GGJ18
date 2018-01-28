using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;

public class ServiceDiscovery {
    public static string GetIP()
    {
        return Network.player.ipAddress;
    }

    public static string[] GetIps()
    {
        IPHostEntry host;
        var localIPs = new List<string>();
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIPs.Add(ip.ToString());
            }
        }
        return localIPs.ToArray();
    }
}
