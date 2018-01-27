using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyAddress : MonoBehaviour {

    public InputField inputField;
    public GameObject completeThing;
    public GameObject Next;
    public VerticalWebSocket ws;

    // Use this for initialization
    void Start () {
		inputField.text = "ws://172.18.11.190:5001/Server";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAddress()
    {
        ws.connect(inputField.text);
        completeThing.SetActive(false);
        Next.SetActive(true);
    }
}
