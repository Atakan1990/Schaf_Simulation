using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{
    protected Character m_character;

    public AState(Character _character)
    {
        m_character = _character;
    }

    public virtual void EnterState()
    {

    }

    public abstract E_States UpdateState();

    public virtual void ExitState()
    {

    }
}
