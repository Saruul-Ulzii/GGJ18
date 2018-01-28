using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

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

    public Text _SpeedText;

    //private float rotationDegrees = 0;
    private float rotationAngleOld;
    
    void Start()
    {
        _EngineControllers = new List<TriebwerkController>();
        var playerCount = Server.Players.Count > 2 ? Server.Players.Count : _PlayerCount;
        _PlayerInputs = _SpaceShipGenerator.GenerateSpaceship(Mathf.Max(3, playerCount), _EngineControllers);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameStates.Game)
        {
            foreach (var kv in _engineState)
            {
                RunEngine(kv.Key.Id, kv.Value);
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.GameState == GameStates.Game)
        {
            while (Server.Commands.Count > 0)
            {
                var nextCommand = Server.Commands.Dequeue();
                RunCommand(nextCommand);
            }

            foreach (var player in Server.Players)
            {
                player.StateUpdateTime -= Time.deltaTime;
                if (player.StateUpdateTime <= 0)
                {
                    player.StateUpdateTime = 0;
                    ResetState(player);
                }
            }
        }

        float speed = _Rigidbody.velocity.magnitude * 1000;
        _SpeedText.text = Convert.ToInt32(speed) + " km/h";
        if (GameManager.Instance != null && GameManager.Instance.Achievements != null)
            GameManager.Instance.Achievements.SetData("SPEED", speed);
        
        UpdateColors();

        if (Input.GetKeyDown("1"))
            _TestPlayerControls = 1;
        if (Input.GetKeyDown("2"))
            _TestPlayerControls = 2;
        if (Input.GetKeyDown("3"))
            _TestPlayerControls = 3;
        if (Input.GetKeyDown("4"))
            _TestPlayerControls = 4;
        if (Input.GetKeyDown("5"))
            _TestPlayerControls = 5;
        if (Input.GetKeyDown("6"))
            _TestPlayerControls = 6;

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

    private void UpdateColors()
    {
        foreach (var kv in _engineState)
        {
            int engineId = kv.Key.Id % _EngineControllers.Count;
            var colorID = kv.Key.Id % PlayerValues.Colors.Count;
            var color = PlayerValues.Colors[colorID];
            _EngineControllers[engineId].SetColor(color);
        }
    }

    //TODO not working #Julius
//    private void countRotations()
//    {
//        rotationDegrees += Abs(transform.eulerAngles.x - rotationAngleOld);
//                Debug.Log(rotationDegrees);
//        //            Debug.Log( transform.eulerAngles.x);
//        if (-360 > rotationDegrees || rotationDegrees > 360)
//        {
//            rotationDegrees = 0;
//            Debug.Log("+1 Rotation");
//            float rota = GameManager.Instance.Achievements.GetData("ROTATIONS");
//            GameManager.Instance.Achievements.SetData("ROTATIONS", rota +1);
//        }
//
//        rotationAngleOld = transform.eulerAngles.x;
//    }

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
            GameManager.Instance.Achievements.AddPlayerData(playerID, "PRESSTIME", Time.deltaTime);

            var tr = transform.GetChild(playerID+1);
            var direction = (tr.rotation * Vector3.back);

            var orig = tr.position - direction;
            Debug.DrawLine(orig, orig + 3 * direction, Color.red);
            _Rigidbody.AddForceAtPosition(0.1f * direction, orig, ForceMode.Impulse);
        }
        else
        {
            GameManager.Instance.Achievements.AddPlayerData(playerID, "RELEASETIME", Time.deltaTime);
        }

       
       

        int engineId = playerID % _EngineControllers.Count;
        _EngineControllers[engineId].On = pressed;
        _EngineControllers[engineId].Intensity = 0.5f;
    }

    public void CloseHit()
    {
        Debug.Log("Close Hits");        
        GameManager.Instance.Achievements.AddData("CLOSEHITS", 1);
    }
}
