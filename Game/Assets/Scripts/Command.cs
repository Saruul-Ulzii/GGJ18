using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public Player Player; // Maybe replace with player object reference
    public string CommandName;
    public string Data;

    public Command()
    {
    }

    public Command(Player player, string name, string data)
    {
        Player = player;
        CommandName = name;
        Data = data;
    }
}
