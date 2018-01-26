using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceDiscovery {
    public static string GetIP()
    {
        return Network.player.ipAddress;
    }
}
