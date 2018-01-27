using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour {

    public InputField inputField;
    public GameObject enterTextButton;
    public VerticalWebSocket ws;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeTheUI()
    {
        if ( string.IsNullOrEmpty(inputField.text))
        {
            Debug.Log("Please enter name!");
            return;
        }
        inputField.gameObject.SetActive(false);
        enterTextButton.SetActive(false);
        ws.SendName(inputField.text);
    }
}
