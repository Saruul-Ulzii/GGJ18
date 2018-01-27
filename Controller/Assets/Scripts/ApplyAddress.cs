using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyAddress : MonoBehaviour {
    
    public GameObject completeThing;
    public GameObject Next;
    public VerticalWebSocket ws;

    // Use this for initialization
    void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAddress()
    {
        //ws.connect(inputField.text);
        //completeThing.SetActive(false);
        //Next.SetActive(true);
    }
}
