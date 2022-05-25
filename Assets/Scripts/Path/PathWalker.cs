using UnityEngine;
using UnityEngine.AI;

public class PathWalker : MonoBehaviour
{
    // Die ID welcher Path vom Agent benutzt werden soll
    [SerializeField]
    private int m_pathIDToWalk = 0;
    private int m_currentNodeIndex = 0;

    // Der Path-selbst der vom Agent benutzt werden soll
    private Path m_path;

    private NavMeshAgent m_agent;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // per Singleton den Path zuweisen der angesteuret werden soll
        m_path = PathDatabase.m_instance.GetPathByID(m_pathIDToWalk);
        GoToNextNode();
    }

    private void Update()
    {
        if (m_agent.remainingDistance <= m_agent.stoppingDistance)
        {
            GoToNextNode();
        }
    }

    /// <summary>
    /// Weist den nächsten Node zu bis der höchste Wert erreicht wurde ansonsten setze den Wert auf 0 zurück
    /// sodass der Agent durchgehend eine Routine läuft
    private void GoToNextNode()
    {
        m_agent.SetDestination(m_path.GetNodePositionByIndex(m_currentNodeIndex));

        if (m_currentNodeIndex < m_path.GetThePathLength() - 1)
        {
            m_currentNodeIndex++;
        }
        else
        {
            m_currentNodeIndex = 0;
        }
    }
}