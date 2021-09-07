using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : AgentState
{
    public Moving(GameObject gameObject) : base(gameObject)
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
