using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefabObject;
    [SerializeField, Range(1, 64)]
    private int m_quantity = 1;
    [SerializeField, Range(1, 64)]
    private int m_spawnRangeX = 1;
    [SerializeField, Range(1, 64)]
    private int m_spawnRangeZ = 1;
    [SerializeField, Range(1, 10)]
    private int m_gapMultiply = 1;

    private int m_rangeX;
    private int m_rangeZ;

    //private int m_noise = Mathf.PerlinNoise(0,1);


    void Start()
    {
        for (int i = 0; i < m_quantity; i++)
        {
            m_rangeX = Random.Range(-m_spawnRangeX, m_spawnRangeX);
            m_rangeZ = Random.Range(-m_spawnRangeZ, m_spawnRangeZ);

            Instantiate(m_prefabObject, this.transform.position + new Vector3(m_rangeX * m_gapMultiply, 0, m_rangeZ * m_gapMultiply), this.transform.rotation);

            // Instantiate(m_prefabObject, transform.position = Random.insideUnitSphere * 15, this.transform.rotation);
            
        }
    }
}
