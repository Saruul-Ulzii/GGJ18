using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public InputField inputField;
    public GameObject joinBtn;
    public GameObject waitingScreen;
    public VerticalWebSocket ws;
    public Text IPText;

    private void Start()
    {
        IPText.text = "IP: " +  WebSocketTest.URL;
        ws.connect(WebSocketTest.URL);

        if (ws.State == WebsocketState.Failed)
        {
            SceneManager.LoadScene("qr-reader");
            return;
        }
    }

    public void SendName()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            Debug.Log("Please enter name!");
            return;
        }

        bool sent = ws.SendName(inputField.text);
        if (sent)
        {
            inputField.gameObject.SetActive(false);
            joinBtn.SetActive(false);
            waitingScreen.SetActive(true);
        }
    }
}
