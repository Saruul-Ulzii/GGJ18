using UnityEngine;

public class Player
{
    public int Id;
    public string Name;
    public Texture2D Photo;

    public Color Color
    {
        get
        {
            // TODO should be based on name
            return Color.blue;
        }
    }
}
