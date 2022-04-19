using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter(Collider _other)
    {
        Character sheep = _other.GetComponent<Character>();

        if (_other.CompareTag("Sheep") && isEdible && sheep.IsStarving)
        {
            sheep.StomachPoints += m_grassPoints;
            
            m_grassPoints = 0;
        }
    }

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
