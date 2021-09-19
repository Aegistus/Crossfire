using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : AgentState
{
    public Walking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(InCoverIdling), Not(IsMoving), InCover));
        transitionsTo.Add(new Transition(typeof(Idling), Not(IsMoving)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Print("Walking");
    }

    public override void DuringExecution()
    {

    }
}
