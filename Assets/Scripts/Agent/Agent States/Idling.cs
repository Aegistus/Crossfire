using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idling : AgentState
{
    public Idling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), AtDestination));
        transitionsTo.Add(new Transition(typeof(InCoverIdling), AtDestination, InCover));
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
