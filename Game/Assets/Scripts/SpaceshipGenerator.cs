
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipGenerator : MonoBehaviour
{
    class PlayerInputRestrictions
    {
        public PlayerInputRestrictions(float ang)
        {
            originalAngle = ang;
            currentAngle = ang;
        }

        public float originalAngle;
        public float currentAngle;
    }
    PlayerInputRestrictions[] _PlayerInputs;

    [SerializeField]
    MeshFilter _MeshFilter;
    [SerializeField]
    Rigidbody _Rigidbody;


    [SerializeField]
    int _PlayerCount;

    [SerializeField]
    GameObject _EnginePrefab;

    [SerializeField]
    float degreesOfFreedom = 45.0f;
    float rotationSpeed = 60.0f;

    int _TestPlayerControls;
    private List<TriebwerkController> engineControllers = new List<TriebwerkController>(); 

    void Start () {
        GenerateSpaceship();
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
            var direction = ( tr.rotation * Vector3.back);

            var orig = tr.position - direction;
            Debug.DrawLine(orig, orig + 3*direction, Color.red);
            _Rigidbody.AddForceAtPosition(0.1f* direction, orig, ForceMode.Impulse);
            engineControllers[_TestPlayerControls].Intensity = Mathf.Lerp(0, 1f, 0.5f * Time.deltaTime);
        }
        else
        {
            engineControllers[_TestPlayerControls].Intensity = 0; 
        }
        var hor = Input.GetAxis("Horizontal");
        if (hor< -float.Epsilon || hor > float.Epsilon)
        {
            var tr = transform.GetChild(_TestPlayerControls);
            var input = _PlayerInputs[_TestPlayerControls-1];
            var origAng = input.originalAngle;
            input.currentAngle = Mathf.Clamp(input.currentAngle + (hor* rotationSpeed * Time.deltaTime), origAng - degreesOfFreedom, origAng + degreesOfFreedom);
            tr.localRotation = Quaternion.Euler(0, input.currentAngle, 0);
        }
    }

    void GenerateSpaceship()
    {
        if (_MeshFilter == null)
        {
            Debug.LogError("No _MeshFilter!");
            return;
        }
        if (_PlayerCount < 3 || _PlayerCount > 6)
        {
            Debug.LogError(string.Format("PlayerCount {0} not supported!", _PlayerCount));
            return;
        }

        var angleBetweenPlayerModules = 360.0f / _PlayerCount;
        var mesh = new Mesh();
        Vector3[] verts = new Vector3[1 + _PlayerCount];
        int[] triangles = new int[3 * _PlayerCount];
        _PlayerInputs = new PlayerInputRestrictions[_PlayerCount];

        float currentAngle = 0f;
        for (int i = 0; i < _PlayerCount; i++)
        {
            verts[i] = new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0, Mathf.Cos(currentAngle* Mathf.Deg2Rad));
            triangles[i * 3 + 0] = i;
            triangles[i * 3 + 1] = (i + 1) % _PlayerCount;
            triangles[i * 3 + 2] = _PlayerCount;

            var engine = Instantiate(_EnginePrefab) as GameObject;
            var engineTr = engine.transform;
            engineTr.SetParent(transform);
            engineTr.localPosition = verts[i] + new Vector3(0,-0.1f,0);
            _PlayerInputs[i] = new PlayerInputRestrictions(currentAngle);
            engineTr.localRotation = Quaternion.Euler(0, currentAngle, 0);

            currentAngle += angleBetweenPlayerModules;

            //Fabe und Intensität der Triebwerke setzten
            var engineController = engine.GetComponent<TriebwerkController>();
            engineControllers.Add(engineController);
            engineController.PlayerColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            engineController.Intensity = 0f;

        }

        verts[_PlayerCount] = new Vector3(0, 0.5f, 0);

        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        _MeshFilter.mesh = mesh;

        //angleBetweenPlayerModules
    }


}
