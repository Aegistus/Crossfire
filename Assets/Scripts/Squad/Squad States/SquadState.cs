using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SquadState : State
{
    protected Squad unit;

    protected SquadState(GameObject gameObject, Squad unit) : base(gameObject)
    {
        this.unit = unit;
    }

}
