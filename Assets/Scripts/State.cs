using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected FireCanon FireCanon;
    protected State(FireCanon fireCanon)
    {
        FireCanon = fireCanon;
    }
    public virtual void OnStateEnter() { }
    public virtual void OnStateUpdate(FireCanon FireCanon) { }
    public virtual void OnStateExit() { }
}
