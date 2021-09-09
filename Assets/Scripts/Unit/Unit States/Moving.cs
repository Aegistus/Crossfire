using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : UnitState
{
    public Moving(GameObject gameObject, Unit unit) : base(gameObject, unit)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Moving");
    }

    public override void DuringExecution()
    {

    }
}
