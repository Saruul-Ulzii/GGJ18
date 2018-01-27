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

    private int _TestPlayerControls = 1;
    private List<TriebwerkController> _EngineControllers = new List<TriebwerkController>();


    void Start()
    {
        _EngineControllers = new List<TriebwerkController>();
        _PlayerInputs = _SpaceShipGenerator.GenerateSpaceship(_PlayerCount, _EngineControllers);
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
            RunEngine(_TestPlayerControls - 1);
        }
        else
        {
            _EngineControllers[_TestPlayerControls - 1].On = false;
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

    public void RunCommand(Command command)
    {
        switch (command.CommandName)
        {
            case "button1":
                RunEngine(command.Player.Id);
                break;
            case "button2":
                break;
            default:
                break;
        }
    }

    private void RunEngine(int playerID)
    {
        var tr = transform.GetChild(_TestPlayerControls);
        var direction = (tr.rotation * Vector3.back);

        var orig = tr.position - direction;
        Debug.DrawLine(orig, orig + 3 * direction, Color.red);
        _Rigidbody.AddForceAtPosition(0.1f * direction, orig, ForceMode.Impulse);

        int engineId = playerID % _EngineControllers.Count;
        _EngineControllers[engineId].On = true;
        _EngineControllers[engineId].Intensity = 0.5f;
    }
}
