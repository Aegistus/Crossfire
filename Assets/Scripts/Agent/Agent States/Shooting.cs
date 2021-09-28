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
        animation.OnAnimationEvent -= CheckAnimationEvent;
    }

    public override void BeforeExecution()
    {
        Print("Shooting");
        animation.OnAnimationEvent += CheckAnimationEvent;
        isFinished = false;
    }

    private void CheckAnimationEvent(AnimEvent animEvent)
    {
        if (animEvent == AnimEvent.Finish)
        {
            isFinished = true;
        }
        else if (animEvent == AnimEvent.Shoot)
        {
            self.Weapon.Shoot();
        }
    }

    public override void DuringExecution()
    {

    }
}
