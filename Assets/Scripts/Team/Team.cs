using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{
    public List<GameObject> teamUnitsGameObjects = new List<GameObject>();
    public List<ICommandable> unitsOnTeam = new List<ICommandable>();

    public bool HasUnitsSelected => selectedUnits.Count > 0;

    private List<ICommandable> selectedUnits = new List<ICommandable>();

    private void Awake()
    {
        for (int i = 0; i < teamUnitsGameObjects.Count; i++)
        {
            unitsOnTeam.Add(teamUnitsGameObjects[i].GetComponent(typeof(ICommandable)) as ICommandable);
            //ICommandable[] children = teamUnitsGameObjects[i].GetComponentsInChildren(typeof(ICommandable)) as ICommandable[];
            //if (children != null && children.Length > 0)
            //{
            //    unitsOnTeam.AddRange(children);
            //}
        }
    }

    public void SelectCommandable(ICommandable toSelect)
    {
        if (unitsOnTeam.Contains(toSelect))
        {
            toSelect.Select();
            selectedUnits.Add(toSelect);
        }
        else
        {
            print("Unit Not on Team");
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
            if (selectedUnits[i].InCover)
            {
                selectedUnits[i].MoveOutOfCover();
            }
            selectedUnits[i].Move(position);
        }
    }

    public void GiveMoveToCoverOrder(Cover cover)
    {
        if (cover.UnOccupied)
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnits[i].InCover)
                {
                    selectedUnits[i].MoveOutOfCover();
                }
                selectedUnits[i].MoveToCover(cover);
            }
        }
    }

    public void GiveAttackOrder(Squad enemySquad)
    {
        if (!unitsOnTeam.Contains(enemySquad))
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Attack(enemySquad);
            }
        }
    }
}
