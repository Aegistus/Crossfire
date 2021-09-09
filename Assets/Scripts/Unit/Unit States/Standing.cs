using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standing : UnitState
{
    public Standing(GameObject gameObject, Unit unit) : base(gameObject, unit)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Unit Standing");
        unit.MoveAgentsIntoFormation();
    }

    public override void DuringExecution()
    {

    }
}
