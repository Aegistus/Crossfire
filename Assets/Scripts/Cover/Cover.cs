using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField]
    private CoverType type;

    [SerializeField]
    private Transform[] coverPositions;

    public CoverType Type { get => type; }
    public bool UnOccupied => occupiedCoverPositions.Count == 0;

    private List<Transform> occupiedCoverPositions = new List<Transform>();
    private Queue<Transform> unoccupiedCoverPositions = new Queue<Transform>();

    private void Awake()
    {
        for (int i = 0; i < coverPositions.Length; i++)
        {
            unoccupiedCoverPositions.Enqueue(coverPositions[i]);
            coverPositions[i].gameObject.SetActive(false);
        }
    }

    public Transform GetCoverPosition()
    {
        if (unoccupiedCoverPositions.Count > 0)
        {
            Transform position = unoccupiedCoverPositions.Dequeue();
            occupiedCoverPositions.Add(position);
            return position;
        }
        else
        {
            return transform;
        }
    }

    public void ReturnCoverPosition(Transform coverPosition)
    {
        unoccupiedCoverPositions.Enqueue(coverPosition);
        occupiedCoverPositions.Remove(coverPosition);
    }

    public void ShowCoverIcons()
    {
        if (UnOccupied)
        {
            for (int i = 0; i < coverPositions.Length; i++)
            {
                coverPositions[i].gameObject.SetActive(true);
            }
        }
    }

    public void HideCoverIcons()
    {
        for (int i = 0; i < coverPositions.Length; i++)
        {
            coverPositions[i].gameObject.SetActive(false);
        }
    }
}
