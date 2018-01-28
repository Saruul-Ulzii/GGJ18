using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour {
    public static PlayerValues Instance;

    [SerializeField]
    private List<Color> _colors;

    public List<Color> Colors
    {
        get
        {
            if (_colors == null || _colors.Count == 0)
            {
                return new List<Color>(new Color[] { Color.red});
            }
            return _colors;
        }
    }


    private void Awake()
    {
        Instance = this;
    }
}
