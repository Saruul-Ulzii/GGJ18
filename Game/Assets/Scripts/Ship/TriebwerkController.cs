﻿using System.Collections;
using System.Collections.Generic;
using MK.Glow;
using UnityEngine;

public class TriebwerkController : MonoBehaviour
{
    [SerializeField]
    GameObject BurnerTrigger;

    public bool On;
    public float Intensity = 1f;

    private GameObject _glow;
    private ParticleSystem _burn;
    private Renderer _rend;
    private Color _playerColor;
    private ParticleSystem _glowParticles;


    void Start()
    {
        _rend = GetComponent<Renderer>();
        SetColor(_playerColor);
        _glow = transform.Find("Afterburner").transform.Find("Glow").gameObject;
        _burn = GetComponentInChildren<ParticleSystem>();
        _rend = GetComponent<Renderer>();
        _glowParticles = _glow.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emmisionBurn = _burn.emission;
        var emmisionGlow = _glowParticles.emission;
        if (On)
        {
            emmisionBurn.enabled = true;
            emmisionGlow.enabled = true;

            var colGlow = _glowParticles.colorOverLifetime;
            colGlow.color = CreateGradientColor(_playerColor, Color.blue, Intensity);

            var colBurn = _burn.colorOverLifetime;
            colBurn.color = CreateGradientColor(Color.red, Color.yellow, Intensity / 2);

            BurnerTrigger.SetActive(true);
        }
        else
        {
            emmisionBurn.enabled = false;
            emmisionGlow.enabled = false;

            BurnerTrigger.SetActive(false);
        }
    }

    public void SetColor(Color color)
    {
        _playerColor = color;
        _rend.material.SetColor("_MKGlowColor", _playerColor);
        _rend.material.SetColor("_MKGlowTexColor", _playerColor);
    }

    private Gradient CreateGradientColor(Color playerColor, Color fadeColor, float alpha)
    {
        Gradient g;
        GradientColorKey[] gck;
        GradientAlphaKey[] gak;
        g = new Gradient();
        gck = new GradientColorKey[2];
        gck[0].color = playerColor;
        gck[0].time = 0.0F;
        gck[1].color = fadeColor;
        gck[1].time = 1.0F;
        gak = new GradientAlphaKey[2];
        gak[0].alpha = alpha;
        gak[0].time = 0.0F;
        gak[1].alpha = 0.0F;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);

        return g;
    }

    public void CloseHit()
    {
        GetComponentInParent<SpaceShipController>().CloseHit();
    }
}