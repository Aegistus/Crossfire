using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerTeam : Team
{
    public LayerMask selectablesLayer;
    public LayerMask groundLayer;

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    RaycastHit rayHit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, selectablesLayer, QueryTriggerInteraction.Collide);
            Squad selected = rayHit.collider?.GetComponentInParent<Squad>();
            if (selected != null)
            {
                SelectSquad(selected);
            }
            else
            {
                DeselectAllSquads();
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, groundLayer);
            if (rayHit.collider != null)
            {
                GiveMoveOrder(rayHit.point);
            }
        }
    }
}
