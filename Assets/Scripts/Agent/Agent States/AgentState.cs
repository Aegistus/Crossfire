using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class AgentState : State
{
    protected NavMeshAgent navAgent;

    protected AgentState(GameObject gameObject) : base(gameObject)
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public Func<bool> AtDestination => () => navAgent.velocity.magnitude > 0;

}
