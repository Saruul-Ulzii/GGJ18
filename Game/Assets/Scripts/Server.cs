using WebSocketSharp;
using WebSocketSharp.Server;

public class Server : WebSocketBehavior
{
	public void Start() {
        var wssv = new WebSocketServer("ws://dragonsnest.far");
        wssv.AddWebSocketService<Server>("/Laputa");
        wssv.Start();
        wssv.Stop();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var msg = e.Data == "BALUS"
                  ? "I've been balused already..."
                  : "I'm not available now.";

        Send(msg);
    }
}
