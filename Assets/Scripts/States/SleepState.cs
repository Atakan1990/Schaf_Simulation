using UnityEngine;

public class SleepState : AState
{
    private float m_tiredPoints = 0f;
    private float m_tiredPerSecond = 7f;

    public SleepState(Character _character) : base(_character)
    {

    }

    public override void EnterState()
    {
        Debug.Log("SleepState is entered!");

        m_character.m_MeshRenderer.material.color = Color.blue;
    }

    public override E_States UpdateState()
    {
        Debug.Log("SleepState in update process");

        Sleep();

        // INHALT CODE für SleepState
        if (m_character.IsSleeping == false)
        {
            return E_States.WALKSTATE;
        }


        //// FollowState
        //if (m_character.IsSheperdClose == true)
        //{
        //    return E_States.FOLLOWSTATE;
        //}
        //// FenceZoneState
        //else if (m_character.IsInFence)
        //{
        //    return E_States.FENCEZONESTATE;
        //}
        //// HungryState
        //else if (m_character.IsStarving == true)
        //{
        //    return E_States.HUNGRYSTATE;
        //}
        //// WalkState
        //else if (m_character.IsSheperdClose == false &&
        //         m_character.IsInFence == false &&
        //         m_character.IsStarving == false)
        //{
        //    return E_States.WALKSTATE;
        //}

        return E_States.SAME;
    }

    public override void ExitState()
    {
        Debug.Log("SleepState is left!");

        m_character.m_MeshRenderer.material.color = Color.white;
    }

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
