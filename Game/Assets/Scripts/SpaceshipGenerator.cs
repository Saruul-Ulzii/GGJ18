
using UnityEngine;

public class SpaceshipGenerator : MonoBehaviour
{
    [SerializeField]
    MeshFilter _MeshFilter;
    [SerializeField]
    Rigidbody _Rigidbody;


    [SerializeField]
    int _PlayerCount;

    [SerializeField]
    GameObject _EnginePrefab;

    void Start () {
        GenerateSpaceship();
	}

    void Update()
    {
        //if (Input.GetKeyDown("1"))
        //{
        //    var tr = transform.GetChild(1);
        //    _Rigidbody.AddForce(transform.rotation* -tr.localPosition, ForceMode.Impulse);
        //}
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
            engineTr.localRotation = Quaternion.Euler(0, currentAngle, 0);

            currentAngle += angleBetweenPlayerModules;            
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
