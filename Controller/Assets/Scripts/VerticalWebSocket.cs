using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VerticalWebSocket : WSClientBehaviour
{
    public WebsocketState State {get;private set;}    

    public Nullable<int> PlayerId {get; private set;}
    public string PlayerName {get; private set;}

	// Use this for initialization

    void Start () {
        string url = "ws://172.18.11.187:5001/Server";
        State = WebsocketState.UnInitialized;
        connect(url );
    }

    public override void handleCommand(Command c)
    {
        base.handleCommand(c);
        
        switch(c.Name.ToLower())
		{
			case "id":
				HandleIdCommand(c.Content.ToLower());
                break;
		}

    }

    private void HandleIdCommand(string arg)
    {
        if(State != WebsocketState.NameSent)
        {
            Debug.Log("Wrong context for command");
            return;
        }
        State = WebsocketState.Initialized;
        PlayerId = int.Parse(arg);
    }



    public override void onConnectionReady(object sender, EventArgs e)
    {
        base.onConnectionReady(sender, e);

    }

    public void SendName(string playerName)
    {
        Command cmd = new Command("NAME", playerName);
        PlayerName = playerName;
        sendCommand(cmd);
    }
}
