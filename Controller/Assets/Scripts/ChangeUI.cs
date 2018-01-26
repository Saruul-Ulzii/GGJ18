using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour {

    public GameObject inputField;
    public GameObject enterTextButton;
    public GameObject startEngine;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeTheUI()
    {
        InputField textFieldCasted = inputField.GetComponent<InputField>(); 
        if ( textFieldCasted.text == "")
        {
            Debug.Log("Please enter name!");
            return;
        }
        inputField.SetActive(false);
        enterTextButton.SetActive(false);
        startEngine.SetActive(true);
    }
}
