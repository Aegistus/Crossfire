using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Agent : MonoBehaviour
{
    public bool debugMode = false;
    public GameObject[] selectionMarkers;

    public StateMachine StateMachine { get; private set; } = new StateMachine();
    public AgentMovement Movement { get; private set; }
    public AgentHealth Health { get; private set; }
    public AgentSelection Selection { get; private set; }
    public AgentCover Cover { get; private set; }
    public Weapon Weapon { get; private set; }

    private void Awake()
    {
        Movement = new AgentMovement(gameObject, transform);
        Health = new AgentHealth();
        Selection = new AgentSelection(selectionMarkers);
        Cover = new AgentCover();
    }

    private void Start()
    {
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(InCoverIdling), new InCoverIdling(gameObject) },
            {typeof(Dying), new Dying(gameObject) },
            {typeof(Shooting), new Shooting(gameObject) },
        };
        StateMachine.SetStates(states, typeof(Idling));
        Movement.SetDestination(transform.position);
        Selection.Deselect();
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        StateMachine.ExecuteState();
        Movement.StopIfAtDestination();
    }

    public void Kill()
    {
        StateMachine.SwitchToNewState(typeof(Dying));
        Health.IsAlive = false;
    }

    public void Shoot()
    {
        StateMachine.SwitchToNewState(typeof(Shooting));
    }
}
