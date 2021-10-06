using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerTeam : Team
{
    public LayerMask selectablesLayer;
    public LayerMask groundLayer;
    public LayerMask coverLayer;

    Camera mainCam;

    private void Start()
    {
        Initiative.StartGame();
        mainCam = Camera.main;
    }

    Cover currentCoverUnderCursor;
    RaycastHit rayHit;
    protected void Update()
    {
        // return if cursor over UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        // return if command underway
        if (!AllSquadsReady)
        {
            return;
        }

        // left clicks
        if (Input.GetMouseButtonDown(0))
        {
            print("LMB");
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, selectablesLayer, QueryTriggerInteraction.Collide);
            Squad selected = rayHit.collider?.GetComponentInParent<Squad>();
            if (selected != null)
            {
                Selection.DeselectAll();
                Selection.SelectSquad(selected);
            }
            else
            {
                Selection.DeselectAll();
            }
        }

        // right clicks
        if (Input.GetMouseButtonUp(1))
        {
            print("RMB");
            // check for enemy squad to attack
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, selectablesLayer);
            if (rayHit.collider != null)
            {
                Squad squad = rayHit.collider.GetComponentInParent<Squad>();
                if (squad != null)
                {
                    Orders.AddCommandToQueue(new ShootCommand(Selection.SelectedUnits[0], this, squad));
                }
            }
            else
            {
                // check for cover
                Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, coverLayer);
                if (rayHit.collider != null)
                {
                    Cover cover = rayHit.collider.GetComponentInParent<Cover>();
                    if (cover != null)
                    {
                        Orders.AddCommandToQueue(new CoverMoveCommand(Selection.SelectedUnits[0], cover));
                    }
                }
                else // if no cover, check for ground
                {
                    Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, groundLayer);
                    if (rayHit.collider != null)
                    {
                        Orders.AddCommandToQueue(new MoveCommand(Selection.SelectedUnits[0], rayHit.point));
                    }
                }
            }
        }

        if (Selection.HasUnitsSelected)
        {
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, coverLayer);
            Cover cover = null;
            if (rayHit.collider != null)
            {
                cover = rayHit.collider.GetComponentInParent<Cover>();
            }
            if (rayHit.collider == null || cover == null || currentCoverUnderCursor != cover)
            {
                currentCoverUnderCursor?.HideCoverIcons();
            }
            currentCoverUnderCursor = cover;
            currentCoverUnderCursor?.ShowCoverIcons();
        }
    }
}
