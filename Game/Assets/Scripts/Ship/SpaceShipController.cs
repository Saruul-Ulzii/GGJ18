using System.Collections.Generic;
using System.Linq;
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

    private List<TriebwerkController> _EngineControllers = new List<TriebwerkController>();

    private Dictionary<Player, bool> _engineState = new Dictionary<Player, bool>();

    [SerializeField]
    int _TestPlayerControls = -1;
    
    void Start()
    {
        _EngineControllers = new List<TriebwerkController>();
        _PlayerInputs = _SpaceShipGenerator.GenerateSpaceship(Mathf.Max(3, Server.Players.Count), _EngineControllers);
    }

    void Update()
    {
        foreach (var kv in _engineState)
        {
            RunEngine(kv.Key.Id, kv.Value);
        }

        if (Input.GetKeyDown("1"))
            _TestPlayerControls = 1;
        if (Input.GetKeyDown("2"))
            _TestPlayerControls = 2;
        if (Input.GetKeyDown("3"))
            _TestPlayerControls = 3;


        if (_TestPlayerControls != -1)
        {
            RunEngine(_TestPlayerControls - 1, Input.GetButton("Jump"));

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

    public void RunCommand(Command command)
    {
        if (!_engineState.ContainsKey(command.Player)) _engineState.Add(command.Player, false);

        switch (command.CommandName)
        {
            case "button1":
                command.Player.StateUpdateTime = GameManager.Instance.ResetStateTime;
                var data = command.Data.ToLower();
                if (data == "pressed") _engineState[command.Player] = true;
                if (data == "released") _engineState[command.Player] = false;                
                break;
            case "button2":
                break;
            default:
                break;
        }
    }

    public void ResetState(Player player)
    {
        if (!_engineState.ContainsKey(player))
        {
            _engineState.Add(player, false);
        }
        else
        {
            _engineState[player] = false;
        }
    }

    public void RemoveMissingPlayers(List<Player> player)
    {
        var missing = new List<Player>();
        foreach (var kv in _engineState)
        {
            if(!player.Any(p => p == kv.Key))
            {
                missing.Add(kv.Key);
            }
        }
        foreach (var person in missing)
        {
            _engineState.Remove(person);
        }
    }

    private void RunEngine(int playerID, bool pressed)
    {
        if (pressed)
        {
            var tr = transform.GetChild(playerID+1);
            var direction = (tr.rotation * Vector3.back);

            var orig = tr.position - direction;
            Debug.DrawLine(orig, orig + 3 * direction, Color.red);
            _Rigidbody.AddForceAtPosition(0.1f * direction, orig, ForceMode.Impulse);
        }

        int engineId = playerID % _EngineControllers.Count;
        _EngineControllers[engineId].On = pressed;
        _EngineControllers[engineId].Intensity = 0.5f;
    }
}
