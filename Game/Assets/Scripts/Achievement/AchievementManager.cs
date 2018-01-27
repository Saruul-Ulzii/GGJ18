using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> Achievements;

    public Achievement GetNextAchievement()
    {
        if (Achievements.Count == 0) return null;

        var index = Random.Range(0, Achievements.Count);
        return Achievements[index];
    } 
}
