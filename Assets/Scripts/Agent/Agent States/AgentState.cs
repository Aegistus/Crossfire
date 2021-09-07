using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentState : State
{
    protected AgentState(GameObject gameObject) : base(gameObject)
    {

    }
}
