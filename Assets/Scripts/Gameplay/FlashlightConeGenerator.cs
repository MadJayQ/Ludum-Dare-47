using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FlashlightConeGenerator : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private Material flashlightConeMaterial;
    [SerializeField] private float fov = 90f;
    [SerializeField] private int rayCount = 50;
    [SerializeField] private float viewDistance = 50f;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private Vector3 VectorAngles(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private void Update()
    {
        Vector3 origin = Vector3.zero;
        float angle = 0f;
        float angleDelta = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2]; //Account for 0 based counting + also origin vertex
        Vector2[] uv = new Vector2[vertices.Length];

        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIdx = 1;
        int triIdx = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + VectorAngles(angle) * viewDistance;
            vertices[vertexIdx] = vertex;
            //RaycastHit raycastHit = Physics.Raycast(origin);
            if (i > 0)
            {
                triangles[triIdx + 0] = 0;
                triangles[triIdx + 1] = vertexIdx - 1;
                triangles[triIdx + 2] = vertexIdx;

                triIdx += 3;
            }

            vertexIdx++;
            angle -= angleDelta;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
