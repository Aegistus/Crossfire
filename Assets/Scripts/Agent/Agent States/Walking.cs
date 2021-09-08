using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : AgentState
{
    public Walking(GameObject gameObject) : base(gameObject)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Walking");
    }

    public override void DuringExecution()
    {

    }
}
