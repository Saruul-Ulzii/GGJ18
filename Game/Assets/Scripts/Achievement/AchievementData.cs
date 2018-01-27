using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData : MonoBehaviour
{
    protected float _value;

    public virtual float GetData()
    {
        return _value;
    }

    public virtual void ResetData()
    {
        _value = 0;
    }
}
