using UnityEngine;

/// <summary>
/// Generiert ein Mesh mit Vertices und Triangles die mit einen Noise und anderen Werten Landschaften formen
/// Version 2 mit Animation Graph ist aktiv dieser beeinflusst den Noiseeffekt je nach Animation Kurve in der X Achse
/// Es wird auch ein Mesh Collider kreiert damit es nicht nur optisch da ist sondern andere Objekte per Collider damit interagieren können
/// </summary>
public class BackgroundGenerator : MonoBehaviour
{
    private MeshRenderer m_meshRenderer; // Die Optik für das Material
    private MeshFilter m_meshFilter; // Die Logik für das Mesh
    private Mesh m_mesh; // Die Informationen für das Mesh Vertices (Eckpunkte) & Triangles (Face) Info

    [SerializeField]
    private Material m_meshMaterial;
    [SerializeField]
    private float m_size; // Für die Breite und die Länge pro Quad (X,Z müssen exakt gleich lang sein)
    [SerializeField, Range(2, 256)]
    private int m_resolution; // Anzahl der Vertices (min. 2 damit 1 Quad entstehen kann - Achtung mathematischer Limit deswegen maximal 256 sonst passieren Fehler)
    [SerializeField]
    private string m_seed; // Sogesehen ein Code oder Wort das diese in ein Wert umwandelt das den Noise beeinflusst
    private Vector2 m_seedNoiseOffset;
    [SerializeField]
    private Vector2 m_noiseCenter; // Noise Startposition am Anfang in der Mitte
    [SerializeField]
    private float m_noiseScale; // Noise Skalierung den Wert am besten unter 0,2 halten
    // [SerializeField]
    // private float m_noiseStrength; // Version 1 - Multiplizierer vom Noiseeffekt für Höhen und Tiefen globale Berechnung
    [SerializeField]
    private AnimationCurve m_animNoiseStrength; // Version 2 Multiplizierer vom Noiseeffekt für Höhen und Tiefen mit AnimationGraph Position + Wert

    private MeshCollider meshCollider;

    /// <summary>
    /// Fügt die benötigten Komponenten hinzu
    /// Initialisiert Mesh und CustomName
    /// Berechnet den Seed
    /// </summary>
    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();

        m_meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        m_meshFilter = this.gameObject.AddComponent<MeshFilter>();
        m_mesh = new Mesh(); // neues mesh damit kein null referenz
        m_mesh.name = "CustomName";

        m_meshRenderer.material = m_meshMaterial;
        m_meshFilter.sharedMesh = m_mesh; // zuweisen damit eine bindung da ist

        int seed = (int)System.DateTime.Now.Ticks;
        if (!string.IsNullOrEmpty(m_seed) && !string.IsNullOrWhiteSpace(m_seed))
        {
            seed = m_seed.GetHashCode();
        }

        seed %= 256;

        m_seedNoiseOffset = new Vector2(seed, seed);

        GenerateMesh();
    }

    /// <summary>
    /// Erstellt den Mesh mit den jeweiligen Vertices und Triangles und weißt diese einen Index zu 
    /// sodass diese per Noise eine Höhe und Tiefe bekommen
    /// </summary>
    private void GenerateMesh()
    {
        Vector3 meshStartPosition = (new Vector3(m_size, 0, m_size) / 2f) * -1; // Zentrale Startposition berechnen

        Vector3[] vertices = new Vector3[m_resolution * m_resolution]; //Berechnung für die Anzahl an Vertices die erstellt/reserviert werden (1 Quad = 2 Triangles = 6 Vertices)
        int[] triangles = new int[(m_resolution - 1) * (m_resolution - 1) * 2 * 3]; // Resolution -1^2 ergibt Quad. Mal 2 * 3 ergibt die Indeces pro Quad

        int trianglesIndex = 0; // Platzhalter der Pro Quad um 6 erhöht wird damit die Indexe jeweils korrekte Werte bekommen 0,6,12 usw
        for (int y = 0, i = 0; y < m_resolution; y++)
        {
            for (int x = 0; x < m_resolution; x++, i++)
            {
                Vector2 percent = new Vector2(x, y) / (m_resolution - 1); // Prozentwert von x und y (0 bis 1) (Berechnung für UV)

                Vector3 planePosition = meshStartPosition + Vector3.right * percent.x * m_size + Vector3.forward * percent.y * m_size; // Berechnet die Plane Größe

                Vector2 noiseValuePosition = m_noiseCenter + m_seedNoiseOffset + new Vector2(planePosition.x, planePosition.z) * m_noiseScale; // Position der Noise und dessen Stärke

                // Vector3 noisePosition = planePosition + Vector3.up * Mathf.PerlinNoise(noiseValuePosition.x, noiseValuePosition.y) * m_noiseStrength; // Version 1
                Vector3 noisePosition = planePosition + Vector3.up * Mathf.PerlinNoise(noiseValuePosition.x, noiseValuePosition.y) * m_animNoiseStrength.Evaluate(percent.x); // Version 2 AnimationGraph - Bezieht sich auf X Achse

                vertices[i] = noisePosition;

                if (x != m_resolution - 1 && y != m_resolution - 1)
                {
                    // Berechne ein Quad
                    // Triangle 1
                    triangles[trianglesIndex + 0] = i;
                    triangles[trianglesIndex + 1] = i + m_resolution + 1;
                    triangles[trianglesIndex + 2] = i + 1;

                    //Triangle 2
                    triangles[trianglesIndex + 3] = i;
                    triangles[trianglesIndex + 4] = i + m_resolution;
                    triangles[trianglesIndex + 5] = i + m_resolution + 1;

                    trianglesIndex += 6; // Platzhalter um 6 erhöhen damit die nächste Iteration korrekt die Vertices den jeweiligen Indexen zuweist
                }
            }
        }

        m_mesh.Clear(); // Alle Informationen clearen
        m_mesh.vertices = vertices; // Neue Mesh Vertices zuweisen
        m_mesh.triangles = triangles; // Neue Mesh Triangles zuweisen
        m_mesh.RecalculateNormals();

        meshCollider.sharedMesh = null; // Setzt den Mesh zurück bzw. löscht die Referenz
        meshCollider.sharedMesh = m_mesh; // Weißt ein neues Mesh hinzu
    }
}