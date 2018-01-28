using UnityEngine;

public class Asteroid : MonoBehaviour
{
    AsteroidSpawner _Spawner;
    Transform _SpaceshipTransform;
    float _DestructionDist;

    Transform _Transform;
    Rigidbody _Rigidbody;

    internal void Init(AsteroidSpawner asteroidSpawner, Transform spawnerTransform, Transform spaceShipTr, float spawnDistance, float destructionDistance, float speed)
    {
        _Spawner = asteroidSpawner;
        _Transform = transform;
        _Transform.parent = spawnerTransform;
        _SpaceshipTransform = spaceShipTr;
        _DestructionDist = destructionDistance;

        var spaceRigid = spaceShipTr.GetComponent<Rigidbody>();

        var angle = Random.Range(0, 360);
        var x = Mathf.Sin(angle);
        var y = Mathf.Cos(angle);
        var spawnVec = new Vector3(x, y, 0);

        _Transform.position = _SpaceshipTransform.position + (spawnDistance * spawnVec);
        _Transform.localScale = Vector3.one;

        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.velocity = speed * -spawnVec + spaceRigid.velocity;
    }

    private void Update()
    {
        if (_SpaceshipTransform == null)
        {
            _Spawner.ReturnAsteroid(gameObject);
            return;
        }
        var dist = Vector3.Distance(_Transform.position, _SpaceshipTransform.position);
        if (dist > _DestructionDist)
        {
            Destroy();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance != null && GameManager.Instance.Achievements != null)
        {
            float hits = GameManager.Instance.Achievements.GetData("HITS");
            GameManager.Instance.Achievements.SetData("HITS", hits + 1);
        }

        if (_Transform.localScale.x >= 1.0f - float.Epsilon)
        {
            _Spawner.SpawnExplosion(_Transform.position, _Rigidbody.velocity);

            _Transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            var vec = _Transform.position - collision.transform.position;
            _Rigidbody.velocity += 0.25f*_Rigidbody.velocity.magnitude* vec;
            return;
        }
        else {
            Destroy();
        }
    }

    public void Destroy()
    {
        _Spawner.SpawnExplosion(_Transform.position, _Rigidbody.velocity);

        if (gameObject.activeSelf)
            _Spawner.ReturnAsteroid(gameObject);
    }
}
