using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : AgentState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        
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
