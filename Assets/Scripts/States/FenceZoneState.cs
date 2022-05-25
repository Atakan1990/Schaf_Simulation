using UnityEngine;

/// <summary>
/// Dieser Zustand wird ausgelöst wenn der Agent im Raum von der FenceZone ist
/// Der Agent läuft solange eine Routine bis dieser seinen State wechselt
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
    /// Führt den aktuellen State aus bis die Bedingung zum nächsten State ausgelöst wird
    /// dieser State wird dann beendet und der nächste per return anschließend ausgeführt
    /// </summary>
    /// <returns>E_States</returns>
    public override E_States UpdateState()
    {
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

    /// <summary>
    /// Weist den nächsten Node zu bis der höchste Wert erreicht wurde ansonsten setze den Wert auf 0 zurück
    /// sodass der Agent durchgehend eine Routine läuft
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
