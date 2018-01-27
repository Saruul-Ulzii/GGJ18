using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour {
    public static PlayerValues Instance;

    public List<Color> colors;

    private void Awake()
    {
        Instance = this;
    }
}
