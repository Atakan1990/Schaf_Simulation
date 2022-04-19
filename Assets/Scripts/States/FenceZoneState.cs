using UnityEngine;

public class FenceZoneState : AState
{
    // Variable FenceZoneState
    // Die ID welcher Path vom Agent benutzt werden soll
    [SerializeField]
    private int m_currentNodeIndex = 0;
    //private int m_pathIDToWalk = 0;

    public FenceZoneState(Character _character) : base(_character)
    {

    }

    public override void EnterState()
    {
        Debug.Log("FenceZoneState is entered!");
    }

    public override E_States UpdateState()
    {
        Debug.Log("FenceZoneState in update process");

        #region --- INHALT CODE für FenceZoneState ---

        if (m_character.m_NavMeshAgent.remainingDistance <= m_character.m_NavMeshAgent.stoppingDistance)
        {
            GoToNextNode(m_character);
        }

        #endregion --- INHALT CODE für FenceZoneState ---


        // SleepState
        if (m_character.IsSleeping == true)
        {
            return E_States.SLEEPSTATE;
        }
        // HungryState
        else if (m_character.IsStarving == true)
        {
            return E_States.HUNGRYSTATE;
        }


        return E_States.SAME;
    }

    public override void ExitState()
    {
        Debug.Log("FenceZoneState is left!");
    }

    private void GoToNextNode(Character _character)
    {
        _character.m_NavMeshAgent.SetDestination(_character.m_Path.GetNodePositionByIndex(m_currentNodeIndex));

        if (m_currentNodeIndex < _character.m_Path.GetThePathLength() - 1)
        {
            m_currentNodeIndex++;
        }
        else
        {
            m_currentNodeIndex = 0;
        }
    }
}
