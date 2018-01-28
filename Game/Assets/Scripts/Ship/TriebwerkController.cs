using System.Collections;
using System.Collections.Generic;
using MK.Glow;
using UnityEngine;

public class TriebwerkController : MonoBehaviour
{
    public Color PlayerColor;
    public bool On;
    public float Intensity = 1f;

    private GameObject _glow;
    private ParticleSystem _burn;

    private Renderer _rend; 
    // Use this for initialization
    void Start()
    {
   

        _glow = transform.Find("Afterburner").transform.Find("Glow").gameObject;

        _burn = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        _rend = GetComponent<Renderer>();
        _rend.material.SetColor("_MKGlowColor", PlayerColor);
        _rend.material.SetColor("_MKGlowTexColor", PlayerColor);

        ParticleSystem glowParticles = _glow.GetComponent<ParticleSystem>();
        var emmisionBurn = _burn.emission;
        var emmisionGlow = glowParticles.emission;
        if (On)
        {
            emmisionBurn.enabled = true;
            emmisionGlow.enabled = true;

            _rend.material.SetColor("_MKGlowColor", PlayerColor);
            _rend.material.SetColor("_MKGlowTexColor", PlayerColor);


            var colGlow = glowParticles.colorOverLifetime;
            colGlow.color = CreateGradientColor(PlayerColor, Color.blue, Intensity);

            var colBurn = _burn.colorOverLifetime;
            colBurn.color = CreateGradientColor(Color.red, Color.yellow, Intensity / 2);
        }
        else
        {
            emmisionBurn.enabled = false;
            emmisionGlow.enabled = false;
        }

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

    void OnTriggerExit(Collider other)
    {
        GetComponentInParent<SpaceShipController>().CloseHit();
    }
}