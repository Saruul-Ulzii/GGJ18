using UnityEngine;

public class Asteroid : MonoBehaviour
{
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
            _Spawner.ReturnAsteroid(gameObject);
        }
        var dist = Vector3.Distance(_Transform.position, _SpaceshipTransform.position);
        if (dist > _DestructionDist)
        {
            Destroy();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float hits = AchievementManager.Instance.GetData("HITS");
        AchievementManager.Instance.SetData("HITS", hits+1);

        Destroy();
    }

    void Destroy()
    {
        _Spawner.SpawnExplosion(_Transform.position);

        if (gameObject.activeSelf)
            _Spawner.ReturnAsteroid(gameObject);
    }
}
