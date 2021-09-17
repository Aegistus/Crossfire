using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCoverIdling : AgentState
{
    public InCoverIdling(GameObject gameObject) : base(gameObject)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("In Cover Idling");
    }

    public override void DuringExecution()
    {

    }
}
