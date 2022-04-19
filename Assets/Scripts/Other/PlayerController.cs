using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Bewegt den Agent per Linke Mausklick in die jeweilige Position
/// dies wird mit ein Ray vom ScreenPointtoRay
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    private NavMeshAgent m_navMeshAgent;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Der Boden benötigt ein Collider damit ein Vector3 zurückgegeben wird
                m_navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
