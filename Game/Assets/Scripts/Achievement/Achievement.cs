using System.Linq;
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
        var achievement = new Achievement
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
                PlayerId = playerId
            }).ToList()
        };

        foreach (var goals in achievement.Goals)
        {
            if(goals.HasOffset) goals.UpdateOffset();
        }

        return achievement;
    }
}

[System.Serializable]
public class AchievementGoal
{
    public bool IsPlayerGoal;
    public bool HasOffset;
    [HideInInspector] public int PlayerId;
    public string Id;
    private float _offset;

    public float Goal;
    
    public bool IsAchieved()
    {
        float data;
        if (IsPlayerGoal)
        {
            data = GameManager.Instance.Achievements.GetPlayerData(PlayerId, Id);
        }
        else
        {
            data = GameManager.Instance.Achievements.GetData(Id);
        }
        if (HasOffset)
        {
            return (data - _offset) >= Goal;
        }
        else
        {
            return data >= Goal;
        }
    }
    
    public void UpdateOffset()
    {
        if (IsPlayerGoal)
        {
            _offset = GameManager.Instance.Achievements.GetPlayerData(PlayerId, Id);
        }
        else
        {
            _offset = GameManager.Instance.Achievements.GetData(Id);
        }
    }
}
