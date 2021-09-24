using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelection
{
    private GameObject[] selectionMarkers;

    public AgentSelection(GameObject[] selectionMarkers)
    {
        this.selectionMarkers = selectionMarkers;
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
}
