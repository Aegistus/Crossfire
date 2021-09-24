using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{
    public List<GameObject> teamUnitsGameObjects = new List<GameObject>();
    public List<Squad> unitsOnTeam = new List<Squad>();

    public bool HasUnitsSelected => selectedUnits.Count > 0;

    private List<Squad> selectedUnits = new List<Squad>();

    private void Awake()
    {
        for (int i = 0; i < teamUnitsGameObjects.Count; i++)
        {
            unitsOnTeam.Add(teamUnitsGameObjects[i].GetComponent<Squad>());
            //ICommandable[] children = teamUnitsGameObjects[i].GetComponentsInChildren(typeof(ICommandable)) as ICommandable[];
            //if (children != null && children.Length > 0)
            //{
            //    unitsOnTeam.AddRange(children);
            //}
        }
    }

    public void SelectCommandable(Squad toSelect)
    {
        if (unitsOnTeam.Contains(toSelect))
        {
            toSelect.Selection.Select();
            selectedUnits.Add(toSelect);
        }
        else
        {
            print("Unit Not on Team");
        }
    }

    public void DeselectCommandable(Squad toDeselect)
    {
        toDeselect.Selection.Deselect();
        selectedUnits.Remove(toDeselect);
    }

    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].Selection.Deselect();
        }
        selectedUnits.Clear();
    }

    public void GiveMoveOrder(Vector3 position)
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (selectedUnits[i].Cover == CoverType.HalfCover || selectedUnits[i].Cover == CoverType.FullCover)
            {
                selectedUnits[i].Movement.MoveOutOfCover();
            }
            selectedUnits[i].Movement.Move(position);
        }
    }

    public void GiveMoveToCoverOrder(Cover cover)
    {
        if (cover.UnOccupied)
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnits[i].Cover == CoverType.HalfCover || selectedUnits[i].Cover == CoverType.FullCover)
                {
                    selectedUnits[i].Movement.MoveOutOfCover();
                }
                selectedUnits[i].Movement.MoveToCover(cover);
            }
        }
    }

    public void GiveAttackOrderOnTarget(Squad enemySquad)
    {
        if (!unitsOnTeam.Contains(enemySquad))
        {
            enemySquad.Movement.CentralizePosition();
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Combat.Attack(enemySquad);
            }
        }
    }
}
