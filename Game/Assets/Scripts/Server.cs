using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;

public class Server : WebSocketBehavior
{
    private static int _playerIds = 0;
    private static WebSocketServer _socketServer;
    private int _playerId;

    public static Queue<Command> Commands = new Queue<Command>();


	public static void Start() {
        var wssv = new WebSocketServer("ws://localhost:5001");
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
        _playerId = _playerIds++;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var data = e.Data.Split(';');
        var command = new Command()
        {
            PlayerId = _playerId,
            CommandName = data[0],
            Data = data[1]
        };

        Commands.Enqueue(command);

        var msg = e.Data == "BALUS"
                  ? "I've been balused already..."
                  : "I'm not available now.";

        Send(msg);
    }
}
