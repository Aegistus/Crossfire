using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Update()
    {
        // left clicks
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, selectablesLayer, QueryTriggerInteraction.Collide);
            Squad selected = rayHit.collider?.GetComponentInParent<Squad>();
            if (selected != null)
            {
                DeselectAll();
                SelectSquad(selected);
            }
            else
            {
                DeselectAll();
            }
        }

        // right clicks
        if (Input.GetMouseButtonUp(1))
        {
            // check for enemy squad to attack
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, selectablesLayer);
            if (rayHit.collider != null)
            {
                Squad squad = rayHit.collider.GetComponentInParent<Squad>();
                if (squad != null)
                {
                    GiveAttackOrderOnTarget(squad);
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
                        GiveMoveToCoverOrder(cover);
                    }
                }
                else // if no cover, check for ground
                {
                    Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out rayHit, 100f, groundLayer);
                    if (rayHit.collider != null)
                    {
                        GiveMoveOrder(rayHit.point);
                    }
                }
            }
        }

        if (HasUnitsSelected)
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
