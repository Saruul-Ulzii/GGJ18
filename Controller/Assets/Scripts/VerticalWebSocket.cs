using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VerticalWebSocket : WSClientBehaviour
{
    public WebsocketState State {get;private set;}    

    public Nullable<int> PlayerId {get; private set;}
    public string PlayerName {get; private set;}
    public GameObject Button1;

    private GameObject _currentInput;

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
            case "start":
                HandleStartCommand();
                break;
            default: 
                Debug.Log("unknown command!");
                break;
		}

    }

    private void HandleIdCommand(string arg)
    {
        if(State != WebsocketState.NameSent)
        {
            Debug.Log("Wrong context for ID command");
            return;
        }
        Debug.Log("Received ID");
        State = WebsocketState.Initialized;
        PlayerId = int.Parse(arg);       
    }

    private void HandleStartCommand()
    {
        if(State != WebsocketState.Initialized)
        {
            Debug.Log("Wrong context for START command");
            return;
        }
        Debug.Log("Received START");
        State = WebsocketState.GameStarted;
        _currentInput = Button1;
        _currentInput.SetActive(true);
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
        State = WebsocketState.NameSent;
        Debug.Log("name sent");
    }

    public void SendButton1Event(string state)
    {
        var cmd = new Command("BUTTON1", state);
        sendCommand(cmd);
        Debug.Log("button1," + state + " sent");
    }
}
