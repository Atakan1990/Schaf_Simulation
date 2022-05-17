using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    private MeshRenderer meshRen; // Für das Material
    private MeshFilter meshFilter; // Für die gesamte Infos übers Mesh
    private Mesh mesh; // Für die Mesh Vert & Tri Info

    [SerializeField]
    private Material mMeshMat;
    [SerializeField]
    private float mSize;
    [SerializeField, Range(2, 256)]
    private int mResolution;
    [SerializeField]
    private string mSeed;
    private Vector2 seedNoiseOffset;
    [SerializeField]
    private Vector2 mNoiseCenter;
    [SerializeField]
    private float mNoiseScale;
    // [SerializeField]
    // private float mNoiseStrength; // Version 1
    [SerializeField]
    private AnimationCurve mAnimNoiseStrength; // Version 2

    private void Awake()
    {
        meshRen = this.gameObject.AddComponent<MeshRenderer>();
        meshFilter = this.gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        mesh.name = "Customlane";

        meshRen.material = mMeshMat;
        meshFilter.sharedMesh = mesh;

        int seed = (int)System.DateTime.Now.Ticks;
        if (!string.IsNullOrEmpty(mSeed) && !string.IsNullOrWhiteSpace(mSeed))
        {
            seed = mSeed.GetHashCode();
        }

        seed %= 256;

        seedNoiseOffset = new Vector2(seed, seed);
    }

    private void Update()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Vector3 meshStartPos = (new Vector3(mSize, 0, mSize) / 2f) * -1; // Startposition

        Vector3[] verts = new Vector3[mResolution * mResolution];
        int[] tris = new int[(mResolution - 1) * (mResolution - 1) * 2 * 3];

        int TriIdx = 0;
        for (int y = 0, i = 0; y < mResolution; y++)
        {
            for (int x = 0; x < mResolution; x++, i++)
            {
                Vector2 percent = new Vector2(x, y) / (mResolution - 1);

                Vector3 planePos = meshStartPos + Vector3.right * percent.x * mSize + Vector3.forward * percent.y * mSize;

                Vector2 noiseValuePosition = mNoiseCenter + seedNoiseOffset + new Vector2(planePos.x, planePos.z) * mNoiseScale;

                // Vector3 noisePos = planePos + Vector3.up * Mathf.PerlinNoise(noiseValuePosition.x, noiseValuePosition.y) * mNoiseStrength; // Version 1
                Vector3 noisePos = planePos + Vector3.up * Mathf.PerlinNoise(noiseValuePosition.x, noiseValuePosition.y) * mAnimNoiseStrength.Evaluate(percent.x); // Version 2 AnimationGraph

                verts[i] = noisePos;

                if (x != mResolution - 1 && y != mResolution - 1)
                {
                    // Berechne ein Quad
                    // Triangle 1
                    tris[TriIdx + 0] = i;
                    tris[TriIdx + 1] = i + mResolution + 1;
                    tris[TriIdx + 2] = i + 1;

                    //Triangle 2
                    tris[TriIdx + 3] = i;
                    tris[TriIdx + 4] = i + mResolution;
                    tris[TriIdx + 5] = i + mResolution + 1;

                    TriIdx += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
    }
}
