using UnityEngine;

/// <summary>
/// Die Basis Klasse wovon alle anderen States erben um verschieden Zust�nde in der KI auszul�sen
/// </summary>
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
