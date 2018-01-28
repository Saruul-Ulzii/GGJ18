using UnityEngine;

public class BurnerTrigger : MonoBehaviour
{
    GameObject _GameObject;

    private void Start()
    {
        _GameObject = gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        if (!_GameObject.activeInHierarchy)
            return;
        var parent = other.transform.parent;
        if (parent == null)
            return;
        var mat = parent.GetComponent<BurnableMaterial>();
        if (mat == null)
            return;

        mat.Burn();
    }
}
