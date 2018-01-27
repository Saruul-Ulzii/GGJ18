using UnityEngine;
using UnityEngine.UI;

public class ServerPlayerDebugText : MonoBehaviour
{
    public Text TextElement;
    public int SkipFrames = 5;

    private int _frame = 0;

    private void Update()
    {
        if (_frame == 0)
        {
            var text = "ServerCommandCount: " + Server.CommandCount + "\n";
            text += "Player:\n";
            foreach (var item in Server.Players)
            {
                text += "(" + item.Id + ") " + item.Name + ": ";
                text += (item.ButtonState != null) ? ((bool)item.ButtonState ? "PRESSED" : "RELEASED") : "No command Send";
                text += " | Command Count: " + item.CommandCount;
                text += "\n";
            }

            TextElement.text = text;
            _frame = SkipFrames;
        }
        else
        {
            _frame--;
        }
    }
}
