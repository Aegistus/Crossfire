using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{
    public List<Squad> squadsOnTeam = new List<Squad>();

    private List<Squad> selectedSquads = new List<Squad>();

    public void SelectSquad(Squad toSelect)
    {
        if (squadsOnTeam.Contains(toSelect))
        {
            toSelect.Select();
            selectedSquads.Add(toSelect);
        }
    }

    public void DeselectSquad(Squad toDeselect)
    {
        toDeselect.Deselect();
        selectedSquads.Remove(toDeselect);
    }

    public void DeselectAllSquads()
    {
        for (int i = 0; i < selectedSquads.Count; i++)
        {
            selectedSquads[i].Deselect();
        }
        selectedSquads.Clear();
    }

    public void GiveMoveOrder(Vector3 position)
    {
        for (int i = 0; i < selectedSquads.Count; i++)
        {
            selectedSquads[i].MoveOrder(position);
        }
    }
}
