﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string Name;
    public string Description;
    public float Time;
    public int Points;
    public List<AchievementGoal> Goals;

    public bool IsAchieved()
    {
        return Goals.All(g => g.IsAchieved());
    }

    public Achievement Clone(int playerId)
    {
        return new Achievement
        {
            Name = Name,
            Description = Description,
            Time = Time,
            Points = Points,
            Goals = Goals.Select(g => new AchievementGoal()
            {
                Id = g.Id,
                Goal = g.Goal,
                IsPlayerGoal = g.IsPlayerGoal,
                PlayerId = g.PlayerId
            }).ToList()
        };
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
        float data;
        if (IsPlayerGoal)
        {
            data = AchievementManager.Instance.GetPlayerData(PlayerId, Id);
        }
        else
        {
            data = AchievementManager.Instance.GetData(Id);
        }
        return data >= Goal;
    }
}
