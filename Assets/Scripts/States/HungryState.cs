using UnityEngine;

/// <summary>
/// Dieser Zustand wird ausgel�st wenn der Agent hunger hat und m_stomachPoints den Wert unter 50 erreicht hat
/// </summary>
public class HungryState : AState
{
    public HungryState(Character _character) : base(_character)
    {

    }

    /// <summary>
    /// F�hrt den aktuellen State aus bis die Bedingung zum n�chsten State ausgel�st wird
    /// dieser State wird dann beendet und der n�chste per return anschlie�end ausgef�hrt
    /// </summary>
    /// <returns>E_States</returns>
    public override E_States UpdateState()
    {
        #region --- INHALT CODE f�r HungryState ---

        // Weist den Agent zu dieser Position zu wo ein Grass Objekt platziert ist
        m_character.m_NavMeshAgent.SetDestination(new Vector3(-10, 0, 10));
        // Anmerkung: Ich hatte jede Menge Ideen wie ich das dynamisch machen k�nnte und habe mir auch Code
        // technisch viele gedanken gemacht aber da die Zeit knapp ist habe ich einfach gehalten und mich auf andere
        // Aufgaben konzentriert - Theoretisch ist eine hohe Chance das sie im WalkState/Follow State einfach irgendwas fressen

        #endregion --- INHALT CODE f�r HungryState ---


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
        else if (m_character.IsInFence == false &&
                 m_character.IsSleeping == false &&
                 m_character.IsStarving == false)
        {
            return E_States.WALKSTATE;
        }

        return E_States.SAME;
    }
}
