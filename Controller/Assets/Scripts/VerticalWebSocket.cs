using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWebSocket : WSClientBehaviour
{

	// Use this for initialization

    void Start () {
        string url = "ws://172.18.11.187:5001/Server";
        connect(url );


    }

    // Update is called once per frame
    void Update () {
		
	}
}
