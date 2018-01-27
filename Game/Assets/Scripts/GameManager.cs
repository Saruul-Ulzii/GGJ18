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

    private Dictionary<Player, Achievement> _playerAchievements = new Dictionary<Player, Achievement>();

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
        UpdateUi();
        SceneManager.LoadScene(GameScene);
        Server.StartGame();

        _playerAchievements = new Dictionary<Player, Achievement>();
        foreach (var player in Server.Players)
        {
            var a = AchievementManager.Instance.GetNextAchievement(player.Id);
            _playerAchievements.Add(player, a);
            Server.SendPlayerMessage(player, new Command(player, "MISSION", a.Name + "\n" + a.Description));
        }
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
        else
        {
            foreach (var item in Server.Players)
            {
                var a = _playerAchievements[item];

                if (a.IsAchieved())
                {
                    var cmd = new Command(item, "MISSION", a.Name + "\n" + a.Description + ";SUCC");
                    Server.SendPlayerMessage(item, cmd);

                    a = AchievementManager.Instance.GetNextAchievement(item.Id);
                    _playerAchievements[item] = a;

                    cmd = new Command(item, "MISSION", a.Name + "\n" + a.Description);
                    Server.SendPlayerMessage(item, cmd);
                }
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
