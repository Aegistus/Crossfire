using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : AgentState
{
    public Shooting(GameObject gameObject) : base(gameObject)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Print("Shooting");
    }

    public override void DuringExecution()
    {

    }
}
