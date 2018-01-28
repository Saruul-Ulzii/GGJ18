using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform _Target;

    Rigidbody _Rigid;
    Transform _Transform;

    float regularDistance;
    float dist;

	void Start () {
        _Transform = transform;
        regularDistance = _Transform.position.z;
        dist = regularDistance;
        _Rigid = _Target.GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        // move away if ship is faster, so players can react to asteroids
        var targetDist = Mathf.Lerp(regularDistance, regularDistance * 2, _Rigid.velocity.sqrMagnitude / 1000);
        dist = Mathf.Lerp(dist, targetDist, Time.deltaTime);
        _Transform.position = Vector3.Lerp(_Transform.position, new Vector3(_Target.position.x, _Target.position.y, dist), 20.0f*Time.deltaTime);
    }
}
