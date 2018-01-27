using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressTimer : AchievementData
{
    private bool _currentState;
    private Dictionary<int, float> PlayerButtonCounts = new Dictionary<int, float>();

    public void OnButtonState(bool pressed, int playerId)
    {

        if (!pressed)
        {
            PlayerButtonCounts[playerId] += _value;
        }
    }

    private void Update()
    {
        
    }
}
