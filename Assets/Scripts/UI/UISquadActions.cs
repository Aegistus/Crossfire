using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISquadActions : MonoBehaviour
{
    public List<GameObject> childElements;

    private PlayerTeam playerTeam;
    private Squad currentInspectedSquad;

    private void Start()
    {
        FindPlayerTeam();
        playerTeam.OnSelection += OnSquadSelection;
        playerTeam.OnDeselection += OnSquadDeselection;
        HideUI();
    }

    private void OnSquadSelection(Squad squad)
    {
        currentInspectedSquad = squad;
        ShowUI();
    }

    private void OnSquadDeselection(Squad squad)
    {
        currentInspectedSquad = null;
        HideUI();
    }

    public void ShowUI()
    {
        for (int i = 0; i < childElements.Count; i++)
        {
            childElements[i].SetActive(true);
        }
    }

    public void HideUI()
    {
        for (int i = 0; i < childElements.Count; i++)
        {
            childElements[i].SetActive(false);
        }
    }

    private void FindPlayerTeam()
    {
        Team[] teams = FindObjectsOfType<Team>();
        for (int i = 0; i < teams.Length; i++)
        {
            if (teams[i] is PlayerTeam)
            {
                playerTeam = (PlayerTeam)teams[i];
                break;
            }
        }
    }
}
