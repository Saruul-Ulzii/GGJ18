﻿using UnityEngine;

public class PlayerShipLife : MonoBehaviour
{
    [SerializeField]
    float MaxLife = 100;

    float CurrentLife;

    public float LifePercent {
    get {
            return CurrentLife / MaxLife;
        }
    }

    private void Start()
    {
        CurrentLife = MaxLife;
    }

    // Use this for initialization
    void OnCollisionEnter () {
        CurrentLife -= 10;

    }
	
}
