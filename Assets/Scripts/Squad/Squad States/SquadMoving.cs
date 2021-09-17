using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMoving : SquadState
{
    public SquadMoving(GameObject gameObject, Squad unit) : base(gameObject, unit)
    {
        
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Squad Moving");
    }

    public override void DuringExecution()
    {

    }
}
