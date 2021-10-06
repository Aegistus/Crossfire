using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveVisualizer : MonoBehaviour
{
    bool showingPath = false;
    LineRenderer line;
    NavMeshAgent navAgent;

    private void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
        navAgent = GetComponentInParent<NavMeshAgent>();
        line.startWidth = .25f;
        line.endWidth = .25f;
        HideLine();
    }

    private void Update()
    {
        if (navAgent.path.corners.Length > 0)
        {
            SetLinePath(navAgent.path.corners);
        }
        else if (showingPath)
        {
            HideLine();
        }
    }

    public void SetLinePath(Vector3[] points)
    {
        line.positionCount = points.Length;
        line.SetPositions(points);
        line.enabled = true;
        showingPath = true;
    }

    public void HideLine()
    {
        line.enabled = false;
        showingPath = false;
    }
}
