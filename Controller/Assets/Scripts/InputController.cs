using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public VerticalWebSocket WebSocket;

    private ButtonState _state;

    private float _resendIntervall;
    private float _lastMessage;
    

    private InputController()
    {
        _state = ButtonState.Released;
        _lastMessage = Time.time;
    }

    public void OnButtonPressed()
    {
        _state = ButtonState.Pressed;
        WebSocket.SendButton1Event("PRESSED");
        _lastMessage = Time.time;
    }

    public void OnButtonReleased()
    {
        _state = ButtonState.Released;
        WebSocket.SendButton1Event("RELEASED");
    }

    void Update()
    {
        if(_state == ButtonState.Pressed && Time.time > _lastMessage + _resendIntervall)
        {
            WebSocket.SendButton1Event("PRESSED");
            _lastMessage = Time.time;
        }
            
    }

    private enum ButtonState
    {
        Pressed,
        Released
    }
}