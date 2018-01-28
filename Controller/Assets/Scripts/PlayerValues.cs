using System.Collections.Generic;
using UnityEngine;

public class PlayerValues
{
    public static readonly List<Color> Colors = new List<Color>(new Color[] {
        new Color(223/255.0f, 38/255.0f, 38/255.0f),
        new Color(64/255.0f, 74/255.0f, 143/255.0f),
        new Color(73/255.0f, 227/255.0f, 106/255.0f),
        new Color(90/255.0f, 32/255.0f, 32/255.0f),
        new Color(163/255.0f, 208/255.0f, 3/255.0f),
        new Color(190/255.0f, 26/255.0f, 206/255.0f)
    });

    public static Color Color
    {
        get
        {
            if (VerticalWebSocket.PlayerId == null)
            {
                return Colors[0];
            }

            var colorID = VerticalWebSocket.PlayerId.Value % Colors.Count;
            return Colors[colorID];
        }
    }
}
