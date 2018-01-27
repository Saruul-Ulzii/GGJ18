using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public int Id;
    public string Name;
    public string Description;
    public float Time;
    public int Points;
    public List<AchievementGoal> Goals;

    public bool IsAchieved()
    {
        if (Goals.All(g => g.IsAchieved())) return true;
        return false;
    }

    public Achievement Clone()
    {
        return new Achievement();
    }
}

[System.Serializable]
public class AchievementGoal
{
    public bool IsPlayerGoal;
    [HideInInspector] public int PlayerId;
    public string Id;
    public float Goal;
    
    public bool IsAchieved()
    {
        if (IsPlayerGoal)
        {
            return AchievementManager.Instance.GetPlayerData(PlayerId, Id) >= Goal;
        }

        return AchievementManager.Instance.GetData(Id) >= Goal;
    }
}
