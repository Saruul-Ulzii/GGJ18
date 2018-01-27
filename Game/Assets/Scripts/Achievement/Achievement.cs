using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public int Id;
    public string Name;
    public string Description;
    public float Goal;
    public float Time;
    public int Points;
    public List<AchievementData> Datas;

    public bool IsAchieved()
    {
        if (Datas.All(d => d.GetData() >= Goal)) return true;
        return false;
    }
}
