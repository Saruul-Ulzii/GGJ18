using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIP : MonoBehaviour {

	void Start () {
        var ip = ServiceDiscovery.GetIP();
        print(ip);
	}
}
