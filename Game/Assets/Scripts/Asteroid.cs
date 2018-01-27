﻿using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    GameObject ExplosionPrefab;

    AsteroidSpawner _Spawner;
    Transform _SpaceshipTransform;
    float _DestructionDist;

    Transform _Transform;

    internal void Init(AsteroidSpawner asteroidSpawner, Transform spawnerTransform, Transform spaceShipTr, float spawnDistance, float destructionDistance, float speed)
    {
        _Spawner = asteroidSpawner;
        _Transform = transform;
        _Transform.parent = spawnerTransform;
        _SpaceshipTransform = spaceShipTr;
        _DestructionDist = destructionDistance;

        var angle = Random.Range(0, 360);
        var x = Mathf.Sin(angle);
        var y = Mathf.Cos(angle);
        var spawnVec = new Vector3(x, y, 0);

        _Transform.position = _SpaceshipTransform.position + (spawnDistance * spawnVec);
        var rigid = GetComponent<Rigidbody>();
        rigid.velocity = speed * -spawnVec;
    }

    private void Update()
    {
        if (_SpaceshipTransform == null)
        {
            _Spawner.Return(gameObject);
        }
        var dist = Vector3.Distance(_Transform.position, _SpaceshipTransform.position);
        if (dist > _DestructionDist)
        {
            Destroy();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy();
    }

    void Destroy() {
        if (ExplosionPrefab != null)
        {
            var explosion = Instantiate(ExplosionPrefab);
            explosion.transform.position = _Transform.position;
        }

        if (gameObject.activeSelf)
            _Spawner.Return(gameObject);
    }
}
