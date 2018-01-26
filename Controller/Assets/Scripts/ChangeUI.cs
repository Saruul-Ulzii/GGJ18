using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        inputField.SetActive(false);
        enterTextButton.SetActive(false);
        startEngine.SetActive(true);
    }
}
