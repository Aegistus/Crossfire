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

    private void Start()
    {
        mainCam = Camera.main;
    }

    RaycastHit rayHit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, selectablesLayer, QueryTriggerInteraction.Collide);
            ICommandable selected = rayHit.collider?.GetComponentInParent(typeof(ICommandable)) as ICommandable;
            if (selected != null)
            {
                SelectCommandable(selected);
            }
            else
            {
                DeselectAll();
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
