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

    public static Queue<Command> Commands = new Queue<Command>();
    public static List<Player> Players = new List<Player>();

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
                var name = command.Data;
                if (IsNameUnique(name))
                {
                    _playerIds++;
                    var player = new Player
                    {
                        Id = _playerIds,
                        Name = name
                    };
                    Debug.Log("Registered player: " + player.Name);
                    Players.Add(player);
                    Send(player.Id.ToString());
                } else
                {
                    Send("Error: Name alreay used");
                    return;
                }
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

    private bool IsNameUnique(string name)
    {
        return true;
    }
}
