using UnityEngine;

public class Player
{
    public int Id;
    public string Name;
    public Texture2D Photo;
    public Server Server;

    public Color Color
    {
        get
        {
            var num = PlayerColor.Instance.colors.Count - 1;
            var color = PlayerColor.Instance.colors[Id % num];
            return color;
        }
    }
}
