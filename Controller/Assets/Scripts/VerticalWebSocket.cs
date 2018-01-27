using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class VerticalWebSocket : WSClientBehaviour
{
    public WebsocketState State {get;private set;}    

    public static Nullable<int> PlayerId {get; private set;}
    public string PlayerName {get; private set;}
    
    public GameObject Button1;
    public GameObject WaitingScreen;

    private GameObject _currentInput;

    private string _serverAddress;

    private void Reconnect()
    {
        if(State != WebsocketState.Disconnected)
        {
            Debug.Log("cant reconnect - not disconnected");   
        }
        var retryCounter = 0;
        do
        {
            retryCounter ++;
            try
            {
                base.connect(_serverAddress);
                var cmd = new Command("ID", PlayerId.ToString());
                sendCommand(cmd);
                State = WebsocketState.GameStarted;
            }
            catch (System.Exception ex)
            {
                Debug.Log("Reconnect Error: " + ex + "\nretry nr " + retryCounter);
            }
            
        } while (retryCounter <= 3);
        State = WebsocketState.Failed;
    }

    void Update()
    {
        if(State == WebsocketState.Disconnected)
            Reconnect();

        while (commandQueue.Count > 0)
        {
            var c = commandQueue.Dequeue();
            Debug.Log("Dequeue: " + c.Name + " content: " + c.Content);
            handleCommand(c);
        }
    }

    public override void connect(string url)
    {
        _serverAddress = url;
        State = WebsocketState.UnInitialized;
        try
        {
            base.connect(url );
        }
        catch (System.Exception)
        {
            State = WebsocketState.Failed;
        }
    }

    public override void handleCommand(Command c)
    {
        Debug.Log("Handle command: " + c.Name);

        base.handleCommand(c);
        
        switch(c.Name.ToLower())
		{
			case "id":
				HandleIdCommand(c.Content.ToLower());
                break;
            case "start":
                HandleStartCommand();
                break;
            case "end":
                base.CloseConnection();
                SceneManager.LoadScene("qr-reader");
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
        WaitingScreen.SetActive(false);
    }

    public override void onConnectionReady(object sender, EventArgs e)
    {
        base.onConnectionReady(sender, e);
    }

    public override void OnConnectionClose()
    {
        base.OnConnectionClose();
        if(PlayerId.HasValue)
            State = WebsocketState.Disconnected;
        else
            State = WebsocketState.Failed;
    }

    public bool SendName(string playerName)
    {
        if(State != WebsocketState.UnInitialized)
        {
            return false;
        }

        Command cmd = new Command("NAME", playerName);
        PlayerName = playerName;
        sendCommand(cmd);
        State = WebsocketState.NameSent;
        Debug.Log("name sent");
        return true;
    }

    public void SendButton1Event(string state)
    {
        var cmd = new Command("BUTTON1", state);
        sendCommand(cmd);
        Debug.Log("button1," + state + " sent");
    }
}
