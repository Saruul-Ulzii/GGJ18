using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class WSClientBehaviour : MonoBehaviour {

    private WebSocket webSocket;
    protected Queue<Command> commandQueue;
    bool webServerReady = false;

    public WSClientBehaviour()
    {
        commandQueue = new Queue<Command>();
    }
	// Use this for initialization
	void Start () {
	}
	
    public virtual void handleCommand( Command c)
    {

    }

    public virtual void onConnectionReady(object sender, EventArgs e )
    {
        webServerReady = true; 
        Debug.Log("Captn, connection is vertial!");
    }

    public virtual void OnConnectionClose()
    {
        webServerReady = false; 
        Debug.Log("oh boi. dat connection is det");
    }

    public virtual void connect( string url )
    {
        Debug.Log("Trying to connect to \"" + url + "\"");

        webSocket = new WebSocket(url);
        webSocket.OnMessage += handleMessage;
        webSocket.OnOpen += onConnectionReady;
        webSocket.OnClose += (s,e) => OnConnectionClose();
        webSocket.OnError += (s,e) => OnConnectionClose();
        
        webSocket.Connect();
    }

    private void handleMessage(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);

        string message = e.Data;
        try
        {
            Command deserialized = Command.deserialize(message);
            commandQueue.Enqueue(deserialized);
            Debug.Log(commandQueue.Count);
        } catch ( ArgumentException ex)
        {
            Debug.Log("Error: " + ex);
        }
    }

    protected void sendCommand( Command message )
    {
        if (!webServerReady)
        {
            Debug.Log("Can't send message! Connection not etablished!");
            return;
        }
        webSocket.Send(message.serialize());
    }
}
