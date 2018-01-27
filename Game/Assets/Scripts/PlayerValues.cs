using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour {
    public static PlayerValues Instance;

    public List<Color> colors;
    public List<string> testNames;

    private void Awake()
    {
        Instance = this;
    }
}
