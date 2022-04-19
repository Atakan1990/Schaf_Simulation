using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryState : AState
{
    // Variable HungryState
    // ---

    public HungryState(Character _character) : base(_character)
    {

    }

    public override void EnterState()
    {
        Debug.Log("HungryState is entered!");

        m_character.m_MeshRenderer.material.color = Color.yellow;
    }

    public override E_States UpdateState()
    {
        Debug.Log("HungryState in update process");

        #region --- INHALT CODE für HungryState ---

        // INHALT
        m_character.m_NavMeshAgent.SetDestination(new Vector3(-10, 0, 10));

        #endregion --- INHALT CODE für HungryState ---


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

    public override void ExitState()
    {
        Debug.Log("HungryState is left!");

        m_character.m_MeshRenderer.material.color = Color.white;
    }
}
