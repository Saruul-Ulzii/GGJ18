﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class WSClientBehaviour : MonoBehaviour {

    private WebSocket webSocket;
    private Queue<Command> commandQueue;
    bool webServerReady = false;

    public WSClientBehaviour()
    {
        commandQueue = new Queue<Command>();
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        while(commandQueue.Count > 0)
        {
            var c = commandQueue.Dequeue();
            handleCommand(c);
        }
	}

    public virtual void handleCommand( Command c)
    {

    }

    public virtual void onConnectionReady(object sender, EventArgs e )
    {
        webServerReady = true; 
        Debug.Log("Captn, connection is vertial!");
    }

    public virtual void OnConnectionClose(object sender, EventArgs e)
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
        webSocket.OnClose += OnConnectionClose;
        try
        {
            webSocket.Connect();
        } catch ( Exception e)
        {
            Debug.Log("Error: " + e);
        }
        
    }

    private void handleMessage(object sender, MessageEventArgs e)
    {
        string message = e.Data;
        try
        {
            Command deserialized = Command.deserialize(message);
            commandQueue.Enqueue(deserialized);
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
