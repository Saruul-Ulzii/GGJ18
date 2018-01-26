using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;
using System;

public class Server : WebSocketBehavior
{
    private static int _playerIds = 0;
    private static WebSocketServer _socketServer;
    private Player _player;
    //private int _playerId;

    public static Queue<Command> Commands = new Queue<Command>();
    public static List<Player> Player = new List<Player>();

	public static void Start() {
        var serverIp = ServiceDiscovery.GetIP();
        var address = "ws://" + serverIp + ":5001";
        Debug.Log("Creating Websocket on " + address);
        var wssv = new WebSocketServer("ws://"+serverIp+":5001");
        wssv.AddWebSocketService<Server>("/Server");
        wssv.Start();
        _socketServer = wssv;
    }
        //wssv.Stop();

    public static void Stop()
    {
        if(_socketServer != null)
        {
            _socketServer.Stop();            
        }
    }

    public Server()
    {
        _player = new Player();
        _player.Id = _playerIds++;
        _player.Name = "Player " + (_player.Id + 1);
        Debug.Log("Player " + _player.Name);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var data = e.Data.Split(';');
        if (data.Length == 2)
        {
            Send("Not a valid command: " + e.Data);
        }

        var command = new Command()
        {
            Player = _player,
            CommandName = data[0],
            Data = data[1]
        };


        switch (command.CommandName)
        {
            case "name":
                Debug.Log("Name: " + command.Data);
                break;
            case "test":
                Debug.Log("Command: " + command.CommandName);
                break;
            default:
                Debug.Log("Unknown command: " + command.CommandName);
                break;
        }

        var msg = e.Data;
        Send(msg);
    }
}
