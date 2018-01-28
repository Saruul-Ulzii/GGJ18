using System.Collections.Generic;
using UnityEngine;

public class BurnableMaterial : MonoBehaviour
{
    public MeshRenderer Renderer;
    public float secondsToBurn = 2.0f;

    float burningStatus = 0.0f;
    Asteroid _Asteroid;

    private void Start()
    {
        _Asteroid = GetComponent<Asteroid>();
    }

    void OnEnable()
    {
        burningStatus = 0.0f;
        Renderer.material.SetColor("_Color", new Color(1, 1, 1));

    }

    public void Burn() {
        burningStatus += Time.deltaTime;
    }

    private void Update()
    {
        if (burningStatus < float.Epsilon)
            return;

        float col = 1.0f - burningStatus / secondsToBurn;
        Renderer.material.SetColor("_Color", new Color(col, col, col));
        if (burningStatus >= secondsToBurn)
        {
            if (GameManager.Instance != null && GameManager.Instance.Achievements != null)
            {
                float burned = GameManager.Instance.Achievements.GetData("BURNEDASTEROIDS");
                burned += 1;
                GameManager.Instance.Achievements.SetData("BURNEDASTEROIDS", burned);
            }

            _Asteroid.Destroy();
        }
    }
}
