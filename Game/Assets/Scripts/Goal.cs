using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SpaceShipController>() != null)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
