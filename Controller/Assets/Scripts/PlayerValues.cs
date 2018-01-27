using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    public static PlayerValues Instance;

    public List<Color> colors;

    private void Awake()
    {
        Instance = this;
    }

    public static Color Color
    {
        get
        {
            if (VerticalWebSocket.PlayerId == null)
            {
                return Instance.colors[0];
            }

            var colorID =  VerticalWebSocket.PlayerId.Value % Instance.colors.Count;
            return Instance.colors[colorID];
        }
    }
}
