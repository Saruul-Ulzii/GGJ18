using System;
using UnityEngine;

public class WebSocketTest : WSClientBehaviour
{
    public static string URL = "ws://localhost:5001/Server";

    void Start()
    {
        string url = URL;
        connect(url);
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

    }

    public void sendTestCommand()
    {
        Command testCommand = new Command("SayMyName", "Fuck the mainstream");
        sendCommand(testCommand);
    }
}
