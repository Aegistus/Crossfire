using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idling : AgentState
{
    public Idling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), IsMoving));
        transitionsTo.Add(new Transition(typeof(InCoverIdling), Not(IsMoving), InCover));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Print("Idling");
    }

    public override void DuringExecution()
    {

    }
}
