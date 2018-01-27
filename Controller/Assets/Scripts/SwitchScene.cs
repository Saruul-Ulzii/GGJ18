using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    public void LoadLobby()
    {
        SceneManager.LoadScene("lobby");
    }
}
