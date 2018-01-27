﻿using System.Linq;
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

    public bool ContainsPlayer(Player player)
    {
        return LobbyPlayer.Any(p => p.Player == player);
    }

    public List<Player> GetDisconnectedPlayer(List<Player> playerList)
    {
        var result = new List<Player>();
        foreach (var p in LobbyPlayer)
        {
            if(!playerList.Any(pl => p.Player == pl))
            {
                result.Add(p.Player);
            }
        }
        return result;
    }

    public void AddPlayer(Player player)
    {
        var instance = Instantiate(PlayerEntryPrefab, PlayerEntryListTransform);
        var entry = instance.GetComponent<LobbyPlayerEntry>();
        entry.AssignPlayer(player);
        LobbyPlayer.Add(entry);
    }

    public void UpdatePlayer(Player player)
    {
        var entry = LobbyPlayer.FirstOrDefault(p => p.Player == player);
        entry.UpdatePlayer(player);
    }

    public void RemovePlayer(Player player)
    {
        var entry = LobbyPlayer.FirstOrDefault(p => p.Player == player);
        Destroy(entry.gameObject);
    }
}