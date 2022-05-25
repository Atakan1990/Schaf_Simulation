using UnityEngine;

/// <summary>
/// Das Objekt bekommt eine zufällige Rotation sowie Skallierung
/// </summary>
public class ScaleAndRotation : MonoBehaviour
{
    Vector3 m_scaleChange;

    /// <summary>
    /// Bei jeder Aktivierung oder Geneierung wird das Objekt zufällig rotiert als auch skalliert
    /// </summary>
    private void OnEnable()
    {
        float randomScale = Random.Range(0.5f, 1.5f);
        m_scaleChange = new Vector3(randomScale, randomScale, randomScale);
        transform.localScale = m_scaleChange;

        this.transform.Rotate(new Vector3(0, Random.Range(0,360), 0));
    }
}
