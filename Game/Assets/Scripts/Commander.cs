using UnityEngine;

public class Commander : MonoBehaviour {
    public GameObject spaceShip;

	void Update () {
        if (Server.Commands.Count > 0)
        {
            var nextCommand = Server.Commands.Dequeue();
        }
	}
}
