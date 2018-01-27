﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VerticalWebSocket : WSClientBehaviour
{
    public WebsocketState State {get;private set;}    

    public Nullable<int> PlayerId {get; private set;}
    public string PlayerName {get; private set;}
    public GameObject Button1;

	// Use this for initialization
    void Start () {
        
    }

    public override void connect(string url)
    {
        State = WebsocketState.UnInitialized;
        base.connect(url );
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
        Button1.SetActive(true);
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

    public void SendButton1Event(string state)
    {
        var cmd = new Command("BUTTON1", state);
        sendCommand(cmd);
    }
}
