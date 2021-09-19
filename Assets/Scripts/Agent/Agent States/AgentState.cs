using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class AgentState : State
{
    protected Agent self;
    protected NavMeshAgent navAgent;

    protected AgentState(GameObject gameObject) : base(gameObject)
    {
        self = gameObject.GetComponent<Agent>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public Func<bool> IsMoving => () => navAgent.velocity.magnitude > 0;
    public Func<bool> InCover => () => self.InCover;

    protected void Print(string message)
    {
        if (self.debugMode)
        {
            Debug.Log(self.gameObject.name + ": " + message);
        }
    }

}
