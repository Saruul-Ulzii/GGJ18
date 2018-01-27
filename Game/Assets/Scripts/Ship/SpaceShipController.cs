using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour {
    
    PlayerInputRestrictions[] _PlayerInputs;

    [SerializeField]
    SpaceshipGenerator _SpaceShipGenerator;

    [SerializeField]
    int _PlayerCount;

    [SerializeField]
    float _DegreesOfFreedom = 45.0f;
    [SerializeField]
    float _RotationSpeed = 60.0f;

    [SerializeField]
    Rigidbody _Rigidbody;

    int _TestPlayerControls = 1;

    private List<TriebwerkController> engineControllers = new List<TriebwerkController>();


    private void Start()
    {
        engineControllers = new List<TriebwerkController>();
        _PlayerInputs = _SpaceShipGenerator.GenerateSpaceship(_PlayerCount, engineControllers);
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
            _TestPlayerControls = 1;

        if (Input.GetKeyDown("2"))
            _TestPlayerControls = 2;

        if (Input.GetKeyDown("3"))
            _TestPlayerControls = 3;

        if (Input.GetButton("Jump"))
        {
            var tr = transform.GetChild(_TestPlayerControls);
            var direction = (tr.rotation * Vector3.back);

            var orig = tr.position - direction;
            Debug.DrawLine(orig, orig + 3 * direction, Color.red);
            _Rigidbody.AddForceAtPosition(0.1f * direction, orig, ForceMode.Impulse);

            engineControllers[_TestPlayerControls - 1].On = true;
            engineControllers[_TestPlayerControls - 1].Intensity = 0.5f;
        }
        else
        {
            engineControllers[_TestPlayerControls - 1].On = false;
        }
        var hor = Input.GetAxis("Horizontal");
        if (hor < -float.Epsilon || hor > float.Epsilon)
        {
            var tr = transform.GetChild(_TestPlayerControls);
            var input = _PlayerInputs[_TestPlayerControls - 1];
            var origAng = input.originalAngle;
            input.currentAngle = Mathf.Clamp(input.currentAngle + (hor * _RotationSpeed * Time.deltaTime), origAng - _DegreesOfFreedom, origAng + _DegreesOfFreedom);
            tr.localRotation = Quaternion.Euler(0, input.currentAngle, 0);
        }
    }
}
