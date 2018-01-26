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

    public override void handleCommand(Command c)
    {
        base.handleCommand(c);
        Debug.Log(c.Name);
        Debug.Log(c.Content);
    }

    public override void onConnectionReady(object sender, EventArgs e)
    {
        base.onConnectionReady(sender, e);
        initializeConnection();

    }

    public void initializeConnection()
    {
        Command testCommand = new Command("NAME", "Fuck the mainstream");
        sendCommand(testCommand);
    }
}
