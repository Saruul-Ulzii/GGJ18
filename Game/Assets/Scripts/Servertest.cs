using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servertest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Server.Start();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Server.Commands.Count; i++)
        {
            var command = Server.Commands.Dequeue();
            Debug.Log("PlayerID: " + command.Player.Id+", PLayerName: " + command.Player.Name + ", Command: " + command.CommandName + ", Data: " + command.Data);
        }       
		
	}
}
