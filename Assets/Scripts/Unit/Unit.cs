using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2[] agentPositions;

    public StateMachine StateMachine { get; private set; }

    private List<Agent> agents;
    private Vector3 position;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    private void Start()
    {
        agents = GetComponentsInChildren<Agent>().ToList();
        Vector3 center = Vector3.zero;
        for (int i = 0; i < agents.Count; i++)
        {
            center += agents[i].transform.position;
        }
        center /= agents.Count;
        position = center;
        MoveAgentsIntoFormation();
    }

    private void MoveAgentsIntoFormation()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (i < agentPositions.Length)
            {
                agents[i].SetDestination(new Vector3(position.x + agentPositions[i].x, position.y, position.z + agentPositions[i].y));
            }
        }
    }
}
