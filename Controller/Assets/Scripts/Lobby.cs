using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public InputField inputField;
    public VerticalWebSocket ws;
    public Text IPText;

    private void Start()
    {
        IPText.text = "IP: " +  WebSocketTest.URL;
        ws.connect(WebSocketTest.URL);
    }

    public void SendName()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            Debug.Log("Please enter name!");
            return;
        }

        inputField.gameObject.SetActive(false);
        ws.SendName(inputField.text);
    }
}
