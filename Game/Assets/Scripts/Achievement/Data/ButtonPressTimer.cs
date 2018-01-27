using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressTimer : AchievementData
{
    private bool _currentState;

    public void OnButtonState(bool pressed)
    {
        _currentState = pressed;
    }

    private void Update()
    {
        if (!_currentState)
        {
            _value = 0;
        }
        else
        {
            _value += Time.deltaTime;
        }
    }
}
