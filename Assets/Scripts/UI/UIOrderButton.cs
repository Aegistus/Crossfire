using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIOrderButton : MonoBehaviour
{
    protected Squad targetSquad;
    protected Team playerTeam;
    protected Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void BindUI(Squad targetSquad, Team playerTeam)
    {
        this.targetSquad = targetSquad;
        this.playerTeam = playerTeam;
    }

    public abstract void ClickButton();
    public abstract void OnShowUI();
    public abstract void OnHideUI();
}
