using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : AgentState
{
    public Dying(GameObject gameObject) : base(gameObject)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Print("Dying");
    }

    public override void DuringExecution()
    {

    }
}
