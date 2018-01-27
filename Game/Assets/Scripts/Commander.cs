using UnityEngine;

public class Commander : MonoBehaviour
{
    public SpaceShipController spaceShipController;

	void Update ()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.GameState == GameStates.Game)
        {
            while (Server.Commands.Count > 0)
            {
                var nextCommand = Server.Commands.Dequeue();
                spaceShipController.RunCommand(nextCommand);
            }

            foreach (var player in Server.Players)
            {
                player.StateUpdateTime -= Time.deltaTime;
                if(player.StateUpdateTime <= 0)
                {
                    player.StateUpdateTime = 0;
                    spaceShipController.ResetState(player);
                }
            }
        }
	}
}
