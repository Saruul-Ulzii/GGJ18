using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHitCounter : AchievementData
{

    public void HitObstacle()
    {
        _value += 1;
    }
}
