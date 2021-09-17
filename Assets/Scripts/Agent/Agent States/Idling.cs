using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idling : AgentState
{
    public Idling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), AtDestination));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Idling");
    }

    public override void DuringExecution()
    {

    }
}
