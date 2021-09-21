using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : AgentState
{
    AgentAnimation animation;
    bool isFinished = false;

    public Shooting(GameObject gameObject) : base(gameObject)
    {
        animation = gameObject.GetComponentInChildren<AgentAnimation>();
        transitionsTo.Add(new Transition(typeof(Idling), () => isFinished));
        
    }

    public override void AfterExecution()
    {
        isFinished = false;
        animation.OnAnimationEvent -= EndState;
    }

    public override void BeforeExecution()
    {
        Print("Shooting");
        animation.OnAnimationEvent += EndState;
        isFinished = false;
    }

    private void EndState(AnimEvent animEvent)
    {
        if (animEvent == AnimEvent.Finish)
        {
            isFinished = true;
        }
    }

    public override void DuringExecution()
    {

    }
}
