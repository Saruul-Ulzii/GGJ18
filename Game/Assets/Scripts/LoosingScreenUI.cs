using UnityEngine;
using UnityEngine.SceneManagement;

public class LoosingScreenUI : MonoBehaviour {

    public void ReVerticalizeClicked () {
        SceneManager.LoadScene("websocket");
	}

    private void Start()
    {
        Server.EndGame();
    }
}
