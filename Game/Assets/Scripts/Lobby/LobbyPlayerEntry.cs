using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerEntry : MonoBehaviour
{
    public Player Player;
    public Text PlayerNameText;
    public Image PlayerColorImage;
    
    public void AssignPlayer(Player player)
    {
        Player = player;
        PlayerNameText.text = player.Name;
        PlayerColorImage.color = player.Color;
    }

    public void UpdatePlayer(Player player)
    {
        PlayerNameText.text = player.Name;
        PlayerColorImage.color = player.Color;
    }

}
