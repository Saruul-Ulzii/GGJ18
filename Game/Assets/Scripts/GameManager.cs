using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int MinPlayer = 3;

    public GameStates GameState;
    public LobbyManager LobbyManager;

    public GameObject StartUiRoot;
    public GameObject LobbyUiRoot;

    public bool HostWebsocketLocal;
    public string GameScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameState = GameStates.None;
        UpdateUi();
    }

    public void StartLobby()
    {
        GameState = GameStates.Lobby;
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
        StartUiRoot.SetActive(GameState == GameStates.None);
        LobbyUiRoot.SetActive(GameState == GameStates.Lobby);
    }
}

public enum GameStates
{
    None,
    Lobby,
    Game
}
