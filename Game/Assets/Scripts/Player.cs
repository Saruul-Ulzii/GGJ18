using UnityEngine;

public class Player
{
    public int Id;
    public Texture2D Photo;
    public Server Server;

    private string name;

    public bool? ButtonState;
    public int CommandCount;

    public float StateUpdateTime;
        
    public Color Color
    {
        get
        {
            var colorID = Id % PlayerValues.Colors.Count;
            var color = PlayerValues.Colors[colorID];
            return color;
        }
    }

    public string Name
    {
        get
        {
            return name == "" ? "Player " + Id : name;
        }
        set
        {
            name = value;
        }
    }

    public static bool operator==(Player p1, Player p2)
    {
        if (ReferenceEquals(p1, p2)) return true;
        if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null)) return false;
        return p1.Id == p2.Id;
    }

    public static bool operator !=(Player p1, Player p2)
    {
        if (ReferenceEquals(p1, p2)) return false;
        if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null)) return true;
        return p1.Id != p2.Id;
    }

    public override bool Equals(object obj)
    {
        var player = obj as Player;
        if (player == null) return false;
        return this == player;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}
