
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public PlayerShipLife ShipLife;
    public Image Lifebar;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Lifebar.fillAmount = ShipLife.LifePercent;
    }
}
