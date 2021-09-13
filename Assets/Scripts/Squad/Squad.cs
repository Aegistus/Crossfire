using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Squad : MonoBehaviour
{
    public Vector2[] agentPositions;
    public GameObject[] selectionMarkers;

    public StateMachine StateMachine { get; private set; } = new StateMachine();
    public Vector3 Position { get; private set; }
    public Vector3 Destination { get; private set; }

    private List<Agent> agents;

    private void Start()
    {
        agents = GetComponentsInChildren<Agent>().ToList();
        CentralizePosition();
        Destination = Position;
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Standing), new Standing(gameObject, this) },
            {typeof(Moving), new Moving(gameObject, this) },
        };
        StateMachine.SetStates(states, typeof(Standing));
        for (int i = 0; i < selectionMarkers.Length; i++)
        {
            selectionMarkers[i].SetActive(false);
        }
    }

    private void CentralizePosition()
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < agents.Count; i++)
        {
            center += agents[i].transform.position;
        }
        center /= agents.Count;
        Position = center;
    }

    public void MoveAgentsIntoFormation()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (i < agentPositions.Length)
            {
                agents[i].SetDestination(new Vector3(Position.x + agentPositions[i].x, Position.y, Position.z + agentPositions[i].y));
            }
        }
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

    public void MoveOrder(Vector3 destination)
    {
        Destination = destination;
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].SetDestination(destination);
        }
    }
}
