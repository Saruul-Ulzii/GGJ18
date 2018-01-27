
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipGenerator : MonoBehaviour
{
    [SerializeField]
    Transform _Anchor;

    [SerializeField]
    GameObject _EnginePrefab;
    [SerializeField]
    GameObject _DetailLightPrefab;

    [SerializeField]
    MeshFilter _MeshFilter;

    public  PlayerInputRestrictions[] GenerateSpaceship(int playerCount, List<TriebwerkController> engineControllers) 
    {
        if (_MeshFilter == null)
        {
            Debug.LogError("No _MeshFilter!");
            return null;
        }
        if (playerCount < 3 || playerCount > 6)
        {
            Debug.LogError(string.Format("PlayerCount {0} not supported!", playerCount));
            return null;
        }

        var angleBetweenPlayerModules = 360.0f / playerCount;
        var mesh = new Mesh();
        Vector3[] verts = new Vector3[1 + playerCount];
        int[] triangles = new int[3 * playerCount];
        var playerInputs = new PlayerInputRestrictions[playerCount];

        float currentAngle = 0f;
        for (int i = 0; i < playerCount; i++)
        {
            verts[i] = new Vector3(Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0, Mathf.Cos(currentAngle* Mathf.Deg2Rad));
            triangles[i * 3 + 0] = i;
            triangles[i * 3 + 1] = (i + 1) % playerCount;
            triangles[i * 3 + 2] = playerCount;

            var engine = Instantiate(_EnginePrefab);
            var engineTr = engine.transform;
            engineTr.SetParent(transform);
            engineTr.localPosition = verts[i] + new Vector3(0,-0.15f,0);
            playerInputs[i] = new PlayerInputRestrictions(currentAngle);
            engineTr.localRotation = Quaternion.Euler(0, currentAngle, 0);

            var col = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            var detailLight = Instantiate(_DetailLightPrefab);
            var detailTr = detailLight.transform;
            detailTr.SetParent(_Anchor);
            detailTr.localPosition = 0.65f*verts[i] + new Vector3(0, 0.35f, 0);
            detailTr.localRotation = Quaternion.Euler(-90, currentAngle, 0);
            detailTr.GetComponent<MeshRenderer>().material.SetColor("_TintColor",col);

            currentAngle += angleBetweenPlayerModules;

            //Fabe und Intensität der Triebwerke setzten
            var engineController = engine.GetComponent<TriebwerkController>();
            engineController.PlayerColor = col;
            engineController.Intensity = 0f;
            engineControllers.Add(engineController);
        }

        verts[playerCount] = new Vector3(0, 0.5f, 0);

        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        _MeshFilter.mesh = mesh;

        //angleBetweenPlayerModules

        return playerInputs;
    }
}
