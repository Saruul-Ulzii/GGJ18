using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;

public class Server : WebSocketBehavior
{
    private static int _playerIds = 0;
    private static WebSocketServer _socketServer;
    private Player _player;

    public static Queue<Command> Commands = new Queue<Command>();
    public static List<Player> Players = new List<Player>();

    public static string ServerUrl;

	public static void Start() {
        var serverIp = ServiceDiscovery.GetIP();
        ServerUrl = "ws://" + serverIp + ":5001";
        Debug.Log("Creating Websocket on " + ServerUrl);
        var wssv = new WebSocketServer(ServerUrl);
        wssv.AddWebSocketService<Server>("/Server");
        wssv.Start();
        _socketServer = wssv;
    }

    public Server()
    {
        _player = new Player
        {
            Id = _playerIds++,
            Server = this
        };
        Players.Add(_player);
        Debug.Log("Registerd player with ID: " + _player.Id);
    }

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
        if (data.Length != 2)
        {
            Send("Not a valid command: " + e.Data);
        }

        var command = new Command()
        {
            Player = _player,
            CommandName = data[0].ToLower(),
            Data = data[1]
        };

        switch (command.CommandName)
        {
            case "name":
                var name = command.Data;
                _player.Name = name;
                Debug.Log("Player name set: " + _player.Name);
                Send("ID;" + _player.Id.ToString());
                break;
            case "button1":
                Debug.Log(string.Format("Player {0} pressed button 1", command.Player.Id));
                Commands.Enqueue(command);
                break;
            case "button2":
                Debug.Log(string.Format("Player {0} pressed button 2", command.Player.Id));
                Commands.Enqueue(command);
                break;
            default:
                Debug.Log("Unknown command: " + command.CommandName);
                break;
        }
    }

    public static void StartGame()
    {
        foreach (var player in Players)
        {
            player.Server.Send("START;");
        }
    }

    public static void EndGame()
    {
        foreach (var player in Players)
        {
            player.Server.Send("END;");
        }
    }
}
