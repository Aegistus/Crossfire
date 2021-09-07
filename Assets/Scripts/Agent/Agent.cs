using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour
{
    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine();
    }

    private void Start()
    {
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Moving), new Moving(gameObject) },
        };
        stateMachine.SetStates(states, typeof(Idling));
    }

    private void Update()
    {
        stateMachine.ExecuteState();
    }

}
