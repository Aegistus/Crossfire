using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadStanding : SquadState
{

    public SquadStanding(GameObject gameObject, Squad unit) : base(gameObject, unit)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Squad Standing");
    }

    public override void DuringExecution()
    {

    }
}
