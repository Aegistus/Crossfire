using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState : State
{
    protected Unit unit;

    protected UnitState(GameObject gameObject, Unit unit) : base(gameObject)
    {
        this.unit = unit;
    }

}
