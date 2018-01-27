using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int MinPlayer = 3;
    public float ResetStateTime = 3;

    public GameStates GameState;
    public LobbyManager LobbyManager;
    
    public GameObject LobbyUiRoot;

    public bool HostWebsocketLocal;
    public string GameScene;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartLobby();
    }

    public void StartLobby()
    {
        GameState = GameStates.Lobby;
        Server.Stop();
        Server.Start(HostWebsocketLocal);
        UpdateUi();
    }

    public void StartGame()
    {
        GameState = GameStates.Game;
        SceneManager.LoadScene(GameScene);
        Server.StartGame();
        UpdateUi();
    }

    private void Update()
    {
        if(GameState == GameStates.Lobby)
        {
            foreach (var player in Server.Players)
            {
                if (LobbyManager.ContainsPlayer(player))
                {
                    LobbyManager.UpdatePlayer(player);
                }
                else
                {
                    LobbyManager.AddPlayer(player);
                }
            }

            var removePlayer = LobbyManager.GetDisconnectedPlayer(Server.Players);
            foreach (var player in removePlayer)
            {
                LobbyManager.RemovePlayer(player);
            }
        }   
    }

    private void UpdateUi()
    {
        LobbyUiRoot.SetActive(GameState == GameStates.Lobby);
    }
}

public enum GameStates
{
    None,
    Lobby,
    Game
}
