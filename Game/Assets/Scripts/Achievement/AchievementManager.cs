using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    //public static AchievementManager Instance;

    private Dictionary<StringPlayerId, float> _dataValues = new Dictionary<StringPlayerId, float>();

    //private void Awake()
    //{
    //    Instance = this;
    //    DontDestroyOnLoad(this.gameObject);
    //    Debug.Log("New Archivement Instance!!!");
    //}

    public void SetData(string id, float value)
    {
        var key = new StringPlayerId(null, id);
        if (!_dataValues.ContainsKey(key))
        {
            _dataValues.Add(key, value);
        }
        else
        {
            _dataValues[key] = value;
        }
    }

    public void AddData(string id, float value)
    {
        var key = new StringPlayerId(null, id);
        if (!_dataValues.ContainsKey(key))
        {
            _dataValues.Add(key, value);
        }
        else
        {
            _dataValues[key] += value;
        }
    }

    public void SetPlayerData(int playerId, string id, float value)
    {
        var key = new StringPlayerId(playerId, id);
        if (!_dataValues.ContainsKey(key))
        {
            _dataValues.Add(key, value);
        }
        else
        {
            _dataValues[key] = value;
        }
    }

    public void AddPlayerData(int playerId, string id, float value)
    {
        var key = new StringPlayerId(playerId, id);
        if (!_dataValues.ContainsKey(key))
        {
            _dataValues.Add(key, value);
        }
        else
        {
            _dataValues[key] += value;
        }
    }

    public float GetData(string id)
    {
        var key = new StringPlayerId(null, id);
        if (_dataValues.ContainsKey(key))
        {
            return _dataValues[key];
        }
        return 0;
    }

    public float GetPlayerData(int playerId, string id)
    {
        var key = new StringPlayerId(playerId, id);
        if (_dataValues.ContainsKey(key))
        {
            return _dataValues[key];
        }
        Debug.Log("Dont found Key " + id + " in Player Data for Player " + playerId);
        return 0;
    }

    public List<Achievement> Achievements;

    public Achievement GetNextAchievement(int playerId, Achievement previous = null)
    {
        if (Achievements.Count == 0)
        {
            return null;
        }
        if (previous == null)
        {
            var index = Random.Range(0, Achievements.Count);
            return Achievements[index].Clone(playerId);
        }
        else
        {
            Achievement result;
            do
            {
                var index = Random.Range(0, Achievements.Count);
                result = Achievements[index];
            } while (result.Name == previous.Name);

            return result.Clone(playerId);
        }
    }

    public struct StringPlayerId
    {
        public int? PlayerId;
        public string Id;

        public StringPlayerId(int? playerId, string id)
        {
            PlayerId = playerId;
            Id = id;
        }
    }
}


// SPEED
// PRESSTIME (Player)
// RELEASETIME (Player)
// BURNEDASTEROIDS
// HITS (Asteroids)
// CLOSEHITS 
//ROTATIONS (spaceship)
