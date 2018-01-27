using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerEntry : MonoBehaviour
{
    private Player _player;
    public Text PlayerNameText;
    public Image PlayerColorImage;
    
    public void AssignPlayer(Player player)
    {
        _player = player;
        PlayerNameText.text = player.Name;
        PlayerColorImage.color = player.Color;
    }

}
