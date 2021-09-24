using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEffects
{
    public GameObject pinMarker;
    public GameObject suppressionMarker;

    public AgentEffects(GameObject pinMarker, GameObject suppressionMarker)
    {
        this.pinMarker = pinMarker;
        this.suppressionMarker = suppressionMarker;
    }

    public void ShowPinMarker()
    {
        pinMarker.SetActive(true);
    }

    public void HidePinMarker()
    {
        pinMarker.SetActive(false);
    }

    public void ShowSuppressionMarker()
    {
        suppressionMarker.SetActive(true);
    }

    public void HideSuppressionMarker()
    {
        suppressionMarker.SetActive(false);
    }
}
