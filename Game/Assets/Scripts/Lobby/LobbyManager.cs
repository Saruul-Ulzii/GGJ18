using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public GameObject PlayerEntryPrefab;
    public Transform PlayerEntryListTransform;

    public List<LobbyPlayerEntry> LobbyPlayer;

    private void Start()
    {
        LobbyPlayer = new List<LobbyPlayerEntry>();
    }

    public void AddPlayer(Player player)
    {
        var instance = Instantiate(PlayerEntryPrefab, PlayerEntryListTransform);
        var entry = instance.GetComponent<LobbyPlayerEntry>();
        entry.AssignPlayer(player);
        LobbyPlayer.Add(entry);
    }
}
