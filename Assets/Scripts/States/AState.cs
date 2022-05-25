using UnityEngine;

/// <summary>
/// Die Basis Klasse wovon alle anderen States erben um verschieden Zustände in der KI auszulösen
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
