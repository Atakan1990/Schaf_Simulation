using UnityEngine;

/// <summary>
/// Simuliert ein Gras das mit der Zeit wächst und eine maximal Größe erreicht
/// und setzt den Wert zurück wenn ein Schaf den Collider betritt
/// sodass die Illusion geweckt wird das dass Schaf es gegessen hat
/// </summary>
public class Grass : MonoBehaviour
{
    [SerializeField]
    private float m_grassPoints = 50f;
    [SerializeField]
    private float m_grassScale = 2f;

    private bool isEdible = true;

    private Transform m_grass;

    private void Start()
    {
        m_grass = GetComponent<Transform>();
    }

    private void Update()
    {
        GrowGrass();
    }

    /// <summary>
    /// Überprüft ob das Objekt ein Schaf ist
    /// und weist ihn die aktuellen Punkte zu und setzt seinen eigenen Wert auf 0
    /// so als hätte es den Anschein das dass Gras gegessen wurde
    /// </summary>
    /// <param name="_other"></param>
    private void OnTriggerEnter(Collider _other)
    {
        Character sheep = _other.GetComponent<Character>();
    
        if (_other.CompareTag("Sheep") && isEdible && sheep.IsStarving)
        {
            sheep.StomachPoints += m_grassPoints;
    
            m_grassPoints = 0;
        }
    }

    /// <summary>
    /// Erhöht den Wert vom Gras bis max. 100 und skalliert die Größe vom Gras entsprechend vom Wert
    /// </summary>
    private void GrowGrass()
    {
        if (m_grassPoints < 100f)
        {
            m_grassPoints += 1f * Time.deltaTime;

            if (m_grassPoints >= 100f)
            {
                m_grassPoints = 100f;
            }
        }

        if (m_grassPoints >= 50f && !isEdible)
        {
            isEdible = true;
        }
        else if (m_grassPoints < 50f && isEdible)
        {
            isEdible = false;
        }

        m_grass.transform.localScale = new Vector3(m_grassPoints, m_grassPoints, m_grassPoints) * 0.01f * m_grassScale;
    }
}
