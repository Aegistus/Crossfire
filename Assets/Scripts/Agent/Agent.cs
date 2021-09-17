using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Agent : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; } = new StateMachine();

    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
        };
        StateMachine.SetStates(states, typeof(Idling));
    }

    private void Update()
    {
        StateMachine.ExecuteState();
        if (transform.position != navAgent.destination && Vector3.Distance(transform.position, navAgent.destination) <= .1f)
        {
            SetDestination(transform.position);
        }
    }

    public void SetDestination(Vector3 pos)
    {
        navAgent.SetDestination(pos);
    }

}
