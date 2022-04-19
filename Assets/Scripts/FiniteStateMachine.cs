using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    Character m_character;
    // States anlegen
    AState m_currentState;
    Dictionary<E_States, AState> m_stateDictionary = new Dictionary<E_States, AState>();
    

    public FiniteStateMachine(Character _character, E_States _initState)
    {
        m_character = _character;

        m_stateDictionary.Add(E_States.WALKSTATE, new WalkState(m_character));
        m_stateDictionary.Add(E_States.FOLLOWSTATE, new FollowState(m_character));
        m_stateDictionary.Add(E_States.FENCEZONESTATE, new FenceZoneState(m_character));
        m_stateDictionary.Add(E_States.SLEEPSTATE, new SleepState(m_character));
        m_stateDictionary.Add(E_States.HUNGRYSTATE, new HungryState(m_character));

        m_currentState = m_stateDictionary[_initState];
    }


    public void Update()
    {
        E_States state = m_currentState.UpdateState();
        
        if (state != E_States.SAME)
        {
            m_currentState.ExitState();
            m_currentState = m_stateDictionary[state];
            m_currentState.EnterState();
        }
    }

}

public enum E_States
{
    SAME,

    WALKSTATE,
    FOLLOWSTATE,
    FENCEZONESTATE,
    SLEEPSTATE,
    HUNGRYSTATE
}
