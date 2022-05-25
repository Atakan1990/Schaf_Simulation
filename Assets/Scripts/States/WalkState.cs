using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Dieser Zustand wird ausgelöst wenn keiner der anderen States ausgelöst wird
/// und der Agent in kleinen Zeitabständen immer wieder eine nue kurze Strecke spaziert
/// </summary>
public class WalkState : AState
{
    // Variable WalkState
    [SerializeField]
    private int m_addRadius = 20;
    [SerializeField]
    private float m_timer = 5f;
    [SerializeField]
    private float m_walkTime = 5f;

    public WalkState(Character _character) : base(_character)
    {

    }

    /// <summary>
    /// Führt den aktuellen State aus bis die Bedingung zum nächsten State ausgelöst wird
    /// dieser State wird dann beendet und der nächste per return anschließend ausgeführt
    /// </summary>
    /// <returns>E_States</returns>
    public override E_States UpdateState()
    {
        #region --- INHALT CODE für WALKSTATE ---

        // Zählt die Zeit hoch
        m_timer += Time.deltaTime;
        // Wurde walkTime überschritten wird eine Position bestimmt
        if (m_timer > m_walkTime)
        {
            // Steuert nächste Position an
            m_character.m_NavMeshAgent.SetDestination(RandomPosition(m_character));
            // Setze Zeit zurück
            m_timer = 0f;
            // Setze eine neue zufällige Zeit für walkTime
            m_walkTime = Random.Range(2f, 10f);
        }

        #endregion --- INHALT CODE für WALKSTATE ---


        // FollowState
        if (m_character.IsSheperdClose == true)
        {
            return E_States.FOLLOWSTATE;
        }
        // FenceZoneState
        else if (m_character.IsInFence == true)
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

        return E_States.SAME;
    }

    /// <summary>
    /// Wählt zufällig im Radius vom Agent eine Position aus
    /// die er als neues Ziel ansteuert
    /// </summary>
    /// <param name="_character"></param>
    /// <returns>Vector3 Position</returns>
    private Vector3 RandomPosition(Character _character)
    {
        // Nimmt eine zufällige Position im Radius von 1 Meter
        // welches mit * m_addRadius erweitert wird z.B auf 20m Radius
        Vector3 direction = Random.insideUnitSphere * m_addRadius;
        // Addiert zur zufälligen Position die aktuelle Position des Agents hinzu
        direction += _character.transform.position;

        NavMeshHit navHit; // Damit wir eine Position im NavMesh erfassen können??

        // sourcePosition, NavMeshHit, maxDistance, areaMask (-1 für alle)
        NavMesh.SamplePosition(direction, out navHit, m_addRadius, -1);

        // Gibt die neu errechente Position zurück
        return navHit.position;
    }
}
