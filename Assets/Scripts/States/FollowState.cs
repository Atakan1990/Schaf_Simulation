using UnityEngine;

/// <summary>
/// Dieser Zustand wird ausgelöst wenn der Agent im Raum vom Shepherd ist
/// Der Agent läuft solange dem Shepherd hinterher bis dieser seinen State wechselt
/// </summary>
public class FollowState : AState
{
    // Variable FollowState
    [SerializeField]
    private float m_aggroRange = 7f;

    public FollowState(Character _character) : base(_character)
    {

    }

    /// <summary>
    /// Führt den aktuellen State aus bis die Bedingung zum nächsten State ausgelöst wird
    /// dieser State wird dann beendet und der nächste per return anschließend ausgeführt
    /// </summary>
    /// <returns>E_States</returns>
    public override E_States UpdateState()
    {
        #region --- INHALT CODE für FollowState ---

        // Wenn der Spieler NICHT tot ist UND
        // Wenn der Spieler in Aggro-Reichweite ist führe diesen Teil aus
        if (Vector3.Distance(m_character.transform.position, m_character.m_Shepherd.transform.position)
            <= m_aggroRange && !m_character.IsInFence && !m_character.IsSleeping)
        {
            // Bewege dich zur Position vom Spieler
            m_character.m_NavMeshAgent.SetDestination(m_character.m_Shepherd.transform.position);
        }

        #endregion --- INHALT CODE für FollowState ---


        // FenceZoneState
        if (m_character.IsInFence == true)
        {
            return E_States.FENCEZONESTATE;
        }
        // SleepState
        else if (m_character.IsSleeping == true)
        {
            return E_States.SLEEPSTATE;
        }
        // HungryState
        else if (m_character.IsStarving == true)
        {
            return E_States.HUNGRYSTATE;
        }
        // WalkState
        else if (m_character.IsInFence == false &&
                 m_character.IsSleeping == false &&
                 m_character.IsStarving == false)
        {
            return E_States.WALKSTATE;
        }

        return E_States.SAME;
    }
}
