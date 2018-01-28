using UnityEngine;

public class CloseHitTrigger : MonoBehaviour {

    public TriebwerkController Controller;

    void OnTriggerExit(Collider other)
    {
        Controller.CloseHit();
    }
}
