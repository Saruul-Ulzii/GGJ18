using UnityEngine;

public class Commander : MonoBehaviour {
    public SpaceShipController spaceShipController;

	void Update () {
        if (Server.Commands.Count > 0)
        {
            var nextCommand = Server.Commands.Dequeue();
            spaceShipController.RunCommand(nextCommand);
        }
	}
}
