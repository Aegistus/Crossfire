using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCoverIdling : AgentState
{
    public InCoverIdling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), IsMoving));
        transitionsTo.Add(new Transition(typeof(Idling), Not(InCover)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Print("In Cover Idling");
        self.Movement.LookAt(self.Cover.Forward);
    }

    public override void DuringExecution()
    {

    }
}
