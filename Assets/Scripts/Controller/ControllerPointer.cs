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
    [Space(5)]
    public SpriteRenderer pointerSprite;
    public static bool canTeleport = true;

    [Header("Teleport")]
    public LayerMask teleportMask;
    public Gradient teleportGradient;
    public float teleportRayMaxDistance = 10.0f;
    public bool isTeleport = false;
    TeleportElement teleportElement;
    RaycastHit teleportHit;

    [Header("Interaction")]
    public LayerMask interactionMask;
    public Gradient interactionGradient;
    public float interactionRayMaxDistance = 10.0f;
    public bool canInteract = true, isInteract = false;
    InteractionElement interactionElement;
    RaycastHit interactionHit;

    [Header("Audio")]
    AudioSource audioSource;
    public AudioClip clickClip, teleportClip;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

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
        if(isTeleport && canTeleport)
        {
            if(Input.GetMouseButtonDown(0))
            {
                teleportElement = teleportHit.transform.GetComponent<TeleportElement>();
                teleportElement.Teleport();
                audioSource.PlayOneShot(teleportClip);
            }
            //
            pointerSprite.gameObject.SetActive(true);
        }
        else if(isInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                interactionElement = interactionHit.transform.GetComponent<InteractionElement>();
                interactionElement.Interaction1();
                audioSource.PlayOneShot(clickClip);
            }
            //
            pointerSprite.gameObject.SetActive(true);
        }
        else
        {
            ResetLine();
            pointerSprite.gameObject.SetActive(false);
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
            RotatePointer(teleportHit.normal);
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
            RotatePointer(interactionHit.normal);
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
        ColorPointer(colorGradient.colorKeys[0].color);
    }

    public void PositionLinePoints(Vector3 hitPoint)
    {
        lineRenderer.SetPosition(0, pointerOrigin.position);
        lineRenderer.SetPosition(1, hitPoint);
        //
        PositionPointer(hitPoint);
    }

    public void ResetLine()
    {
        PositionLinePoints(pointerOrigin.position + pointerOrigin.forward);
        ColorLine(initGradient);
    }

    #endregion

    #region Pointer Sprite

    public void PositionPointer(Vector3 position)
    {
        pointerSprite.transform.position = position;
    }
    public void RotatePointer(Vector3 normalDir)
    {
        pointerSprite.transform.rotation = Quaternion.FromToRotation(pointerSprite.transform.up, normalDir);
    }

    public void ColorPointer(Color pointerColor)
    {
        pointerSprite.color = pointerColor;
    }

    #endregion
}
