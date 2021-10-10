using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInitiative : MonoBehaviour
{
    public Text teamText;
    public Image teamColorPanel;

    private void Awake()
    {
        Initiative.OnInitiativeChange += UpdateInitiativeUI;
    }

    private void UpdateInitiativeUI(Team team)
    {
        if (team != null)
        {
            teamText.text = team.TeamName;
            teamColorPanel.color = team.TeamMaterial.color;
        }
    }
}
