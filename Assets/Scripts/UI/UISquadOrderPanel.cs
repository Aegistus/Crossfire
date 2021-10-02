using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISquadOrderPanel : MonoBehaviour
{
    public List<GameObject> childElements;
    public List<UIOrderButton> buttons;

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
        BindDisplayToSquad();
        ShowUI();
    }

    private void OnSquadDeselection(Squad squad)
    {
        currentInspectedSquad = null;
        HideUI();
    }

    private void BindDisplayToSquad()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].BindUI(currentInspectedSquad, playerTeam);
        }
    }

    public void ShowUI()
    {
        for (int i = 0; i < childElements.Count; i++)
        {
            childElements[i].SetActive(true);
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].OnShowUI();
        }
    }

    public void HideUI()
    {
        for (int i = 0; i < childElements.Count; i++)
        {
            childElements[i].SetActive(false);
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].OnHideUI();
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
