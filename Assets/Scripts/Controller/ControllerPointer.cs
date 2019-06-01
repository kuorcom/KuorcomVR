using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ControllerPointer : MonoBehaviour
{
    [Header("Pointer Config")]
    public Transform pointerOrigin;
    LineRenderer lineRenderer;
    public Gradient initGradient;

    [Header("Teleport")]
    public LayerMask teleportMask;
    public Gradient teleportGradient;
    public float teleportRayMaxDistance = 10.0f;
    public bool canTeleport = true, isTeleport = false;
    RaycastHit teleportHit;

    [Header("Interaction")]
    public LayerMask interactionMask;
    public Gradient interactionGradient;
    public float interactionRayMaxDistance = 10.0f;
    public bool canInteract = true, isInteract = false;
    RaycastHit interactionHit;

    // Start is called before the first frame update
    void Start()
    {
        if(!pointerOrigin)
        {
            pointerOrigin = transform;
        }
        //
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTeleport)
        {

        }
        else if(isInteract)
        {

        }
        else
        {
            ResetLine();
        }
    }

    private void FixedUpdate()
    {
        InteractionRaycast();
        TeleportRaycast();
    }

    #region Raycasts

    public void TeleportRaycast()
    {
        if (Physics.Raycast(pointerOrigin.position, pointerOrigin.forward, out teleportHit, teleportRayMaxDistance, teleportMask))
        {
            isTeleport = true;
            ColorLine(teleportGradient);
            PositionLinePoints(teleportHit.point);
        }
        else
        {
            isTeleport = false;
            teleportHit = new RaycastHit();
        }
    }
    public void InteractionRaycast()
    {
        if (Physics.Raycast(pointerOrigin.position, pointerOrigin.forward, out interactionHit, interactionRayMaxDistance, interactionMask))
        {
            isInteract = true;
            ColorLine(interactionGradient);
            PositionLinePoints(interactionHit.point);
        }
        else
        {
            isInteract = false;
            interactionHit = new RaycastHit();
        }
    }

    #endregion

    #region Line Renderer

    public void ColorLine(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void PositionLinePoints(Vector3 hitPoint)
    {
        lineRenderer.SetPosition(0, pointerOrigin.position);
        lineRenderer.SetPosition(1, hitPoint);
    }

    public void ResetLine()
    {
        PositionLinePoints(pointerOrigin.position);
        ColorLine(initGradient);
    }

    #endregion
}
