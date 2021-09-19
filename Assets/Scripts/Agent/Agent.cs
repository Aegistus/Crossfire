using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Agent : MonoBehaviour
{
    public bool debugMode = false;
    public GameObject[] selectionMarkers;

    public bool InCover => currentCover != null;
    public StateMachine StateMachine { get; private set; } = new StateMachine();

    private NavMeshAgent navAgent;

    private Cover currentCover;
    private Transform currentCoverPosition;

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
            {typeof(InCoverIdling), new InCoverIdling(gameObject) },
        };
        StateMachine.SetStates(states, typeof(Idling));
        Move(transform.position);
        Deselect();
    }

    private void Update()
    {
        StateMachine.ExecuteState();

        // make agent stop moving when arrived
        if (transform.position != navAgent.destination && Vector3.Distance(transform.position, navAgent.destination) <= .1f)
        {
            Move(transform.position);
        }
    }

    public void Move(Vector3 position)
    {
        navAgent.SetDestination(position);
    }

    public void Select()
    {
        for (int i = 0; i < selectionMarkers.Length; i++)
        {
            selectionMarkers[i].SetActive(true);
        }
    }

    public void Deselect()
    {
        for (int i = 0; i < selectionMarkers.Length; i++)
        {
            selectionMarkers[i].SetActive(false);
        }
    }

    public void MoveToCover(Cover cover)
    {
        currentCover = cover;
        currentCoverPosition = cover.GetCoverPosition();
        Move(currentCoverPosition.position);
    }

    public void MoveOutOfCover()
    {
        if (currentCover != null)
        {
            currentCover.ReturnCoverPosition(currentCoverPosition);
            currentCover = null;
        }
    }
}
