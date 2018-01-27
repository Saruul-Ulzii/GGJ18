using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    [SerializeField]
    GameObject _AsteroidPrefab;

    [SerializeField]
    Transform _SpaceShipTr;

    float _InitialAsteroidSpawnsInSeconds = 5.0f;
    float _Difficulty = 1.0f;
    float _DifficultyIncreasePerSeconds = 0.025f;
    float _SpawnDistance = 30.0f;
    float _DestructionDistance = 50.0f;

    float _Speed = 1.0f;

    Transform _SpawnerTransform;
    Queue<GameObject> PooledAsteroids = new Queue<GameObject>();

    // Use this for initialization
    void Start () {
        _SpawnerTransform = transform;

        StartCoroutine(SpawnAsteroids());
	}

    IEnumerator SpawnAsteroids() {
        while (true)
        {
            yield return new WaitForSeconds(_InitialAsteroidSpawnsInSeconds / _Difficulty);

            GameObject asteroidGo = null;
            if (PooledAsteroids.Count > 0)
            {
                asteroidGo = PooledAsteroids.Dequeue();
                asteroidGo.SetActive(true);
            }
            else {
                asteroidGo = Instantiate(_AsteroidPrefab);
            }

            var asteroid = asteroidGo.GetComponent<Asteroid>();
            asteroid.Init(this, _SpawnerTransform, _SpaceShipTr, _SpawnDistance, _DestructionDistance, _Speed);
        }
    }

    internal void Return(GameObject go)
    {
        go.SetActive(false);
        PooledAsteroids.Enqueue(go);
    }

    // Update is called once per frame
    void Update () {
        _Difficulty += (Time.deltaTime * _DifficultyIncreasePerSeconds);
    }
}
