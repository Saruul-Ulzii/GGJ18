using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {
    public static PlayerColor Instance;

    public List<Color> colors;

    private void Awake()
    {
        Instance = this;
    }
}
