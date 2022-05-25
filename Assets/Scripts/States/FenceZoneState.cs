using UnityEngine;

/// <summary>
/// Dieser Zustand wird ausgel�st wenn der Agent im Raum von der FenceZone ist
/// Der Agent l�uft solange eine Routine bis dieser seinen State wechselt
/// </summary>
public class FenceZoneState : AState
{
    // Variable FenceZoneState
    // Die ID welcher Path vom Agent benutzt werden soll
    [SerializeField]
    private int m_currentNodeIndex = 0;

    public FenceZoneState(Character _character) : base(_character)
    {

    }

    /// <summary>
    /// F�hrt den aktuellen State aus bis die Bedingung zum n�chsten State ausgel�st wird
    /// dieser State wird dann beendet und der n�chste per return anschlie�end ausgef�hrt
    /// </summary>
    /// <returns>E_States</returns>
    public override E_States UpdateState()
    {
        #region --- INHALT CODE f�r FenceZoneState ---

        if (m_character.m_NavMeshAgent.remainingDistance <= m_character.m_NavMeshAgent.stoppingDistance)
        {
            GoToNextNode(m_character);
        }

        #endregion --- INHALT CODE f�r FenceZoneState ---


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

    /// <summary>
    /// Weist den n�chsten Node zu bis der h�chste Wert erreicht wurde ansonsten setze den Wert auf 0 zur�ck
    /// sodass der Agent durchgehend eine Routine l�uft
    /// </summary>
    /// <param name="_character"></param>
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
