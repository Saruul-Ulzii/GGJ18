using UnityEngine;

public class GoalIndicator : MonoBehaviour
{
    [SerializeField]
    Transform _Goal;

    [SerializeField]
    Transform _Spaceship;

	// Update is called once per frame
	void Update () {
        var pos = _Spaceship.position - _Goal.position;
        pos.Normalize();

        var z = Mathf.Asin(pos.x) * Mathf.Rad2Deg;
        var euler = new Vector3(0, 0, pos.y < 0 ? z : 180-z);

        transform.localRotation = Quaternion.Euler(euler);
    }
}
