using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Color PlayerColor;

    public string MissionText;

    public string PlayerIndicator;

	public Text MissionTextComponent;
	public Text PlayerIndicatorComponent;
	public Button EngineButton;
	public Image ButtonCenterImage;

	// Use this for initialization
	void Start ()
	{
	    MissionTextComponent.text = MissionText;
	    PlayerIndicatorComponent.text = PlayerIndicator;

	    var colors = EngineButton.colors;
	    colors.pressedColor = PlayerColor;
	    EngineButton.colors = colors;

	    ButtonCenterImage.color = PlayerColor; //TODO funktioniert noch nicht #julius
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
