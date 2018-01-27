using System.Collections.Generic;
using System.Linq;
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
    private static List<Player> DisconnectedPlayers = new List<Player>();

    public static string ServerUrl;
    public static int CommandCount;

    public static bool IsInGame;

    public static void Start(bool isLocal = false) {

        var serverIp = isLocal ? "localhost" : ServiceDiscovery.GetIP();
        var address = "ws://" + serverIp + ":5001";
        var route = "/Server";
        ServerUrl = address + route;
        Debug.Log("Creating Websocket on " + ServerUrl);

        var wssv = new WebSocketServer(address);
        wssv.AddWebSocketService<Server>(route);
        wssv.Start();
        
        _socketServer = wssv;
    }

    public Server()
    {
        _player = new Player
        {
            Id = GetNextValidId(),
            Server = this,
        };
        _player.Name = "Player " + _player.Id;
        Players.Add(_player);
        Debug.Log("Registerd player with ID: " + _player.Id);
        CommandCount = 0;
    }

    ~Server()
    {
        
    }

    public static void Stop()
    {
        if(_socketServer != null)
        {
            Debug.Log("Stopping Websocket Server..");
            _socketServer.Stop();            
        }
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var data = e.Data.Split(';');
        if (data.Length < 2)
        {
            Send("Not a valid command: " + e.Data);
        }

        var command = new Command()
        {
            Player = _player,
            CommandName = data[0].ToLower(),
            Data = data[1]
        };
        CommandCount++;
        _player.CommandCount++;

        switch (command.CommandName)
        {
            case "id":
                Debug.Log("Get ID Command: " + command.Player.Id + ", " + command.Data);
                var id = Convert.ToInt32(command.Data);
                var dplayer = DisconnectedPlayers.FirstOrDefault(p => p.Id == id);
                if(dplayer != null)
                {
                    Debug.Log("Found disconnectedPlayer!");
                    DisconnectedPlayers.Remove(dplayer);
                    _player.Id = dplayer.Id;
                    _player.Name = dplayer.Name;
                    _player.CommandCount = dplayer.CommandCount;
                }
                break;
            case "name":
                var name = command.Data;
                _player.Name = name;
                Debug.Log("Player name set: " + _player.Name);
                Send("ID;" + _player.Id.ToString());
                break;
            case "button1":
                Debug.Log(string.Format("Player {0} {1} button 1", command.Player.Id, command.Data.ToLower()));
                _player.ButtonState = command.Data.ToLower() == "pressed" ? true : (command.Data.ToLower() == "released" ? (bool?)false : null);
                Commands.Enqueue(command);
                break;
            case "button2":
                Debug.Log(string.Format("Player {0} {1} button 2", command.Player.Id, command.Data.ToLower()));
                Commands.Enqueue(command);
                break;
            default:
                Debug.Log("Unknown command: " + command.CommandName);
                break;
        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Debug.Log("Player disconnected from Websocket: " + _player.Id);
        Players.Remove(_player);
        if (IsInGame)
        {
            DisconnectedPlayers.Add(_player);
        }
        else
        {
            _player = null;
        }
    }

    public static void StartGame()
    {
        IsInGame = true;
        foreach (var player in Players)
        {
            player.Server.Send("START;");
        }
    }

    public static void SendPlayerMessage(Player player, Command command)
    {
        player.Server.Send(command.CommandName + ";" + command.Data);
    }

    public static void EndGame()
    {
        IsInGame = false;
        foreach (var player in Players)
        {
            player.Server.Send("END;");
        }
    }

    private int GetNextValidId()
    {
        if (Players.Count == 0) return 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (!Players.Any(p => p.Id == i)) return i;
        }
        return Players.Count;
    }
}