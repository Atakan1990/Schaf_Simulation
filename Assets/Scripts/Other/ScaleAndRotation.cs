using UnityEngine;

/// <summary>
/// Das Objekt bekommt eine zufällige Rotation sowie Skallierung
/// </summary>
public class ScaleAndRotation : MonoBehaviour
{
    [SerializeField]
    private float m_minScale = 0.5f;
    [SerializeField]
    private float m_maxScale = 1.5f;
    [SerializeField]
    private float m_minRotation = 0f;
    [SerializeField]
    private float m_maxRotation = 360f;
    private Vector3 m_scaleChange;

    /// <summary>
    /// Bei jeder Aktivierung oder Geneierung wird das Objekt zufällig rotiert als auch skalliert
    /// </summary>
    private void OnEnable()
    {
        float randomScale = Random.Range(m_minScale, m_maxScale);
        m_scaleChange = new Vector3(randomScale, randomScale, randomScale);
        transform.localScale = m_scaleChange;

        this.transform.Rotate(new Vector3(0, Random.Range(m_minRotation, m_maxRotation), 0));
    }
}
