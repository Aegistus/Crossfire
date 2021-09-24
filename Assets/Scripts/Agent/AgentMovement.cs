using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement
{
    private NavMeshAgent navAgent;
    private Transform transform;

    public AgentMovement(GameObject gameObject, Transform transform)
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        this.transform = transform;
    }
    
    public void SetDestination(Vector3 position)
    {
        navAgent.SetDestination(position);
    }

    public void StopIfAtDestination()
    {
        // make agent stop moving when at destination
        if (transform.position != navAgent.destination && Vector3.Distance(transform.position, navAgent.destination) <= .1f)
        {
            SetDestination(transform.position);
        }
    }
}