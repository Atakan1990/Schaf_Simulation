using UnityEngine;

/// <summary>
/// Bewegt MainCamera wie in einem RTS Spiel
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float m_cameraSpeed = 20f;
    [SerializeField]
    private float m_mouseScrollSpeed = 1000f;
    [SerializeField]
    private float m_minZoomY = 10f;
    [SerializeField]
    private float m_maxZoomY = 20f;
    private float m_BorderThickness = 10f; // Abstand zum Bildschirm Rand
    [SerializeField]
    private Vector2 m_mapLimit = new Vector2(20f, 25f);

    void Update()
    {
        // Kamera Position der lokalen Vector3 zuweisen zum berechnen
        Vector3 cameraPosition = this.transform.position;

        // Kamera bewegen mithilfe WASD oder Maus am Rande des Bildschirms
        // Screen.height gibt den Pixelwert für die Höhe - unser Randabstand
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - m_BorderThickness)
        {
            cameraPosition.z += m_cameraSpeed * Time.deltaTime;
        }
        // Screen.height wird hier nicht benötigt weil wir direkt bei 0 also unten anfangen
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= m_BorderThickness)
        {
            cameraPosition.z -= m_cameraSpeed * Time.deltaTime;
        }
        // Screen.width wie height nur diesmal in die Breite
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - m_BorderThickness)
        {
            cameraPosition.x += m_cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.y <= m_BorderThickness)
        {
            cameraPosition.x -= m_cameraSpeed * Time.deltaTime;
        }

        // 1. Maus Zoom 2. Geschwindigkeit 3. höhen tiefen Limit
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        cameraPosition.y += mouseScroll * m_mouseScrollSpeed * Time.deltaTime;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, m_minZoomY, m_maxZoomY);

        // Map x,z Grenze für Kamera festlegen
        // Vector2.y ist in Vector3.z
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -m_mapLimit.x, m_mapLimit.x);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, -m_mapLimit.y, m_mapLimit.y);

        // Neu berechnete lokale Vector3 der Kamera zuweisen
        this.transform.position = cameraPosition;

    }
}
