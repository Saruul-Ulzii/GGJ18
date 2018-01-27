using UnityEngine;

public class ReturnExplosion : MonoBehaviour {

    public AsteroidSpawner Spawner;

    ParticleSystem _System;

    void Start () {
        _System = GetComponent<ParticleSystem>();
    }
	
	void Update () {
        if (!_System.isEmitting) {
            Spawner.ReturnExplosion(gameObject);
        }
    }
}
