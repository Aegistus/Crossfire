using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{
    public List<GameObject> teamUnitsGameObjects = new List<GameObject>();

    public List<ICommandable> unitsOnTeam = new List<ICommandable>();

    private List<ICommandable> selectedUnits = new List<ICommandable>();

    private void Awake()
    {
        for (int i = 0; i < teamUnitsGameObjects.Count; i++)
        {
            unitsOnTeam.Add(teamUnitsGameObjects[i].GetComponent(typeof(ICommandable)) as ICommandable);
        }
    }

    public void SelectCommandable(ICommandable toSelect)
    {
        if (unitsOnTeam.Contains(toSelect))
        {
            toSelect.Select();
            selectedUnits.Add(toSelect);
        }
    }

    public void DeselectCommandable(ICommandable toDeselect)
    {
        toDeselect.Deselect();
        selectedUnits.Remove(toDeselect);
    }

    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].Deselect();
        }
        selectedUnits.Clear();
    }

    public void GiveMoveOrder(Vector3 position)
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].Move(position);
        }
    }
}
