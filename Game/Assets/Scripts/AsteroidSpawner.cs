using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    [SerializeField]
    GameObject _ExplosionPrefab;
    [SerializeField]
    GameObject _AsteroidPrefab;

    [SerializeField]
    Transform _SpaceShipTr;
    Rigidbody _SpaceshipRigid;

    float _InitialAsteroidSpawnsInSeconds = 4.0f;
    float _Difficulty = 0.8f;
    float _DifficultyIncreasePerSeconds = 0.025f;
    float _SpawnDistance = 30.0f;
    float _DestructionDistance = 70.0f;

    float _Speed = 1.0f;

    Transform _SpawnerTransform;
    Queue<GameObject> PooledAsteroids = new Queue<GameObject>();
    Queue<GameObject> PooledExplosions = new Queue<GameObject>();

    // Use this for initialization
    void Start () {
        _SpawnerTransform = transform;
        _SpaceshipRigid = _SpaceShipTr.GetComponent<Rigidbody>();

        StartCoroutine(SpawnAsteroids());
	}

    IEnumerator SpawnAsteroids() {
        while (true)
        {
            var sqrMagDiv1000 = _SpaceshipRigid.velocity.sqrMagnitude / 1000;
            // spawn faster if player is faster, also asteroids need to be faster to keep challenge on same level
            var speedFactor = Mathf.Lerp(1, 8, sqrMagDiv1000);
            var spawnAndDistanceFactor = Mathf.Lerp(1, 2, sqrMagDiv1000);

            yield return new WaitForSeconds(_InitialAsteroidSpawnsInSeconds / (spawnAndDistanceFactor * _Difficulty));

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
            asteroid.Init(this, _SpawnerTransform, _SpaceShipTr, spawnAndDistanceFactor * _SpawnDistance, 
                spawnAndDistanceFactor * _DestructionDistance, _Speed * speedFactor);
        }
    }

    internal void SpawnExplosion(Vector3 position, Vector3 velocity)
    {
        GameObject explosionGo = null;
        if (PooledExplosions.Count > 0)
        {
            explosionGo = PooledExplosions.Dequeue();
            explosionGo.SetActive(true);
        }
        else
        {
            explosionGo = Instantiate(_ExplosionPrefab);
            explosionGo.GetComponent<ReturnExplosion>().Spawner = this;
        }

        explosionGo.transform.position = position;

        var rigid = explosionGo.GetComponent<Rigidbody>();
        rigid.velocity = velocity;
    }

    internal void ReturnAsteroid(GameObject go)
    {
        go.SetActive(false);
        PooledAsteroids.Enqueue(go);
    }

    internal void ReturnExplosion(GameObject go)
    {
        go.SetActive(false);
        PooledExplosions.Enqueue(go);
    }

    // Update is called once per frame
    void Update () {
        _Difficulty += (Time.deltaTime * _DifficultyIncreasePerSeconds);
    }
}
