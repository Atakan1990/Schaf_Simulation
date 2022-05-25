using UnityEngine;

/// <summary>
/// Dieser Zustand wird ausgelöst wenn der Agent erschöpft/müde ist und m_tiredPoints den Wert unter 0 erreicht hat
/// </summary>
public class SleepState : AState
{
    private float m_tiredPoints = 0f;
    private float m_tiredPerSecond = 7f;

    public SleepState(Character _character) : base(_character)
    {

    }

    /// <summary>
    /// Beim Start/Beitritt des States wird der Agent blau gefärbt um es visuell zu zeigen
    /// </summary>
    public override void EnterState()
    {
        m_character.m_MeshRenderer.material.color = Color.blue;
    }

    /// <summary>
    /// Führt den aktuellen State aus bis die Bedingung zum nächsten State ausgelöst wird
    /// dieser State wird dann beendet und der nächste per return anschließend ausgeführt
    /// </summary>
    /// <returns>E_States</returns>
    public override E_States UpdateState()
    {
        Sleep();

        // INHALT CODE für SleepState
        if (m_character.IsSleeping == false)
        {
            return E_States.WALKSTATE;
        }

        return E_States.SAME;
    }

    /// <summary>
    /// Beim verlassen des States wird der Agent weiß gefärbt um visuell feedback zu geben
    /// </summary>
    public override void ExitState()
    {
        m_character.m_MeshRenderer.material.color = Color.white;
    }

    /// <summary>
    /// Simuliert den Schlaf/Relax Phase wo sich der Agent ausruht
    /// </summary>
    public void Sleep()
    {
        m_tiredPoints += m_tiredPerSecond * Time.deltaTime;

        if (m_tiredPoints >= 100)
        {
            m_character.IsSleeping = false;
            m_character.TiredPoints = Random.Range(80f, 120f);

            m_tiredPoints = 0;
        }
    }
}
