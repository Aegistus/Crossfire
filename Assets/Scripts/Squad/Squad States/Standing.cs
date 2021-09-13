using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standing : SquadState
{
    public Standing(GameObject gameObject, Squad unit) : base(gameObject, unit)
    {
        transitionsTo.Add(new Transition(typeof(Moving), () => unit.Destination != unit.Position));
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
