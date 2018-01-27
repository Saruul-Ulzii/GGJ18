using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int MinPlayer = 2;

    public GameStates GameState;
    public LobbyManager LobbyManager;

    public GameObject StartUiRoot;
    public GameObject LobbyUiRoot;

    public bool HostWebsocketLocal;

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
