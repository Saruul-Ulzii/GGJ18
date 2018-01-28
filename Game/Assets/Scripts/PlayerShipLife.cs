using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerShipLife : MonoBehaviour
{
    float damageFactor = 5.0f;

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
    void OnCollisionEnter (Collision collision) {
        CurrentLife -= damageFactor*collision.relativeVelocity.magnitude;

        if (CurrentLife < 0)
        {
            SceneManager.LoadScene("LoosingScreen");
        }
    }
	
}
