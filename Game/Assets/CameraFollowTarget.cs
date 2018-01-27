using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform _Target;

    Transform _Transform;

	// Use this for initialization
	void Start () {
        _Transform = transform;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        _Transform.position = Vector3.Lerp(_Transform.position, new Vector3(_Target.position.x, _Target.position.y, _Transform.position.z), 20.0f*Time.deltaTime);
    }
}
