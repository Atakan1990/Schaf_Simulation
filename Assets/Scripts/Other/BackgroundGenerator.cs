using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    private MeshRenderer m_meshRenderer; // Für das Material
    private MeshFilter m_meshFilter; // Für die gesamte Infos übers Mesh
    private Mesh m_mesh; // Für die Mesh Vert & Tri Info

    [SerializeField]
    private Material m_meshMaterial;
    [SerializeField]
    private float m_size;
    [SerializeField, Range(2, 256)]
    private int m_resolution;
    [SerializeField]
    private string m_seed;
    private Vector2 m_seedNoiseOffset;
    [SerializeField]
    private Vector2 m_noiseCenter;
    [SerializeField]
    private float m_noiseScale;
    // [SerializeField]
    // private float m_noiseStrength; // Version 1
    [SerializeField]
    private AnimationCurve m_animNoiseStrength; // Version 2

    private void Awake()
    {
        m_meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        m_meshFilter = this.gameObject.AddComponent<MeshFilter>();
        m_mesh = new Mesh();
        m_mesh.name = "Customlane";

        m_meshRenderer.material = m_meshMaterial;
        m_meshFilter.sharedMesh = m_mesh;

        int seed = (int)System.DateTime.Now.Ticks;
        if (!string.IsNullOrEmpty(m_seed) && !string.IsNullOrWhiteSpace(m_seed))
        {
            seed = m_seed.GetHashCode();
        }

        seed %= 256;

        m_seedNoiseOffset = new Vector2(seed, seed);
    }

    private void Update()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Vector3 meshStartPos = (new Vector3(m_size, 0, m_size) / 2f) * -1; // Startposition

        Vector3[] verts = new Vector3[m_resolution * m_resolution];
        int[] tris = new int[(m_resolution - 1) * (m_resolution - 1) * 2 * 3];

        int TriIdx = 0;
        for (int y = 0, i = 0; y < m_resolution; y++)
        {
            for (int x = 0; x < m_resolution; x++, i++)
            {
                Vector2 percent = new Vector2(x, y) / (m_resolution - 1);

                Vector3 planePos = meshStartPos + Vector3.right * percent.x * m_size + Vector3.forward * percent.y * m_size;

                Vector2 noiseValuePosition = m_noiseCenter + m_seedNoiseOffset + new Vector2(planePos.x, planePos.z) * m_noiseScale;

                // Vector3 noisePos = planePos + Vector3.up * Mathf.PerlinNoise(noiseValuePosition.x, noiseValuePosition.y) * m_noiseStrength; // Version 1
                Vector3 noisePos = planePos + Vector3.up * Mathf.PerlinNoise(noiseValuePosition.x, noiseValuePosition.y) * m_animNoiseStrength.Evaluate(percent.x); // Version 2 AnimationGraph

                verts[i] = noisePos;

                if (x != m_resolution - 1 && y != m_resolution - 1)
                {
                    // Berechne ein Quad
                    // Triangle 1
                    tris[TriIdx + 0] = i;
                    tris[TriIdx + 1] = i + m_resolution + 1;
                    tris[TriIdx + 2] = i + 1;

                    //Triangle 2
                    tris[TriIdx + 3] = i;
                    tris[TriIdx + 4] = i + m_resolution;
                    tris[TriIdx + 5] = i + m_resolution + 1;

                    TriIdx += 6;
                }
            }
        }

        m_mesh.Clear();
        m_mesh.vertices = verts;
        m_mesh.triangles = tris;
        m_mesh.RecalculateNormals();
    }
}
