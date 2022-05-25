using UnityEngine;

/// <summary>
/// Zerst�rt das Objekt bei Chance die vom Entwickler festgelegt wird
/// sorgt daf�r das verschiedene Objekte beim generieren mal da sind oder nicht sodass mehr vielfalt vorget�uscht wird
/// </summary>
public class DestroyThisByChance : MonoBehaviour
{
    [SerializeField]
    private float m_minChanceCondition = 5f;
    [SerializeField]
    private float m_minValue = 0f;
    [SerializeField]
    private float m_maxValue = 10f;

    private void OnEnable()
    {
        if (m_minChanceCondition < Random.Range(m_minValue, m_maxValue))
        {
            Destroy(gameObject);
        }
    }
}
