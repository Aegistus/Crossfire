using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField]
    private Transform[] coverPositions;

    private List<Transform> occupiedCoverPositions = new List<Transform>();
    private Queue<Transform> unoccupiedCoverPositions = new Queue<Transform>();

    private void Awake()
    {
        for (int i = 0; i < coverPositions.Length; i++)
        {
            unoccupiedCoverPositions.Enqueue(coverPositions[i]);
        }
    }

    public Transform GetCoverPosition()
    {
        Transform position = unoccupiedCoverPositions.Dequeue();
        occupiedCoverPositions.Add(position);
        return position;
    }

    public void ReturnCoverPosition(Transform coverPosition)
    {
        unoccupiedCoverPositions.Enqueue(coverPosition);
        occupiedCoverPositions.Remove(coverPosition);
    }
}
