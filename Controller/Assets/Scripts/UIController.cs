using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Color PlayerColor;

    public string MissionText;

    public string PlayerIndicator;

	// Use this for initialization
	void Start ()
	{
	    transform.Find("MissionText").GetComponent<Text>().text = MissionText;
	    transform.Find("EngineButton").GetComponentInChildren<Text>().text = PlayerIndicator;

	    var colors = transform.Find("EngineButton").GetComponent<Button>().colors;
	    colors.pressedColor = PlayerColor;
	    transform.Find("EngineButton").GetComponent<Button>().colors = colors;

	    transform.Find("EngineButton").transform.Find("Center").GetComponent<Image>().color = PlayerColor; //TODO funktioniert noch nicht #julius
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
