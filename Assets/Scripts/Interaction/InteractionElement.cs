using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionElement : MonoBehaviour
{
    [Header("Configuration")]
    public InteractionType interactionType1 = InteractionType.ChangeColor;
    public InteractionType interactionType2 = InteractionType.ChangeColor;
    Material interactionMaterial;
    MeshRenderer interactionMesh;

    [Header("Change Color")]
    public bool loopColors = true;
    public List<Color> availableColors = new List<Color>();
    int actualColor = 0;

    [Header("Change Material")]
    public bool loopMaterials = true;
    public List<Material> availableMaterials = new List<Material>();
    int actualMaterial = 0;

    [Header("Change Model")]
    public InteractionContainer interactionContainer;

    // Start is called before the first frame update
    void Awake()
    {
        interactionMesh = GetComponent<MeshRenderer>();
        interactionMaterial = interactionMesh.material;

        if(!interactionContainer)
        {
            interactionContainer = transform.parent.GetComponent<InteractionContainer>();
        }
    }

    // Update is called once per frame
    void Start()
    {
        switch (interactionType1)
        {
            case InteractionType.ChangeColor:
                ChangeColor(actualColor);
                break;
            case InteractionType.ChangeMaterial:
                ChangeMaterial(actualMaterial);
                break;
            case InteractionType.ChangeModel:
                interactionContainer.InitModel();
                break;
        }
    }

    #region Interaction

    public void Interaction1()
    {
        switch (interactionType1)
        {
            case InteractionType.ChangeColor:
                ChangeToNextColor();
                break;
            case InteractionType.ChangeMaterial:
                ChangeToNextMaterial();
                break;
            case InteractionType.ChangeModel:
                interactionContainer.ChangeToNextModel();
                break;
        }
    }

    //Colors

    void ChangeColor(int colorIndex)
    {
        interactionMaterial.SetColor("_BaseColor", availableColors[colorIndex]);
    }
    public void ChangeToNextColor()
    {
        if(actualColor + 1 < availableColors.Count)
        {
            actualColor++;
        }
        else
        {
            if(loopColors)
            {
                actualColor = 0;
            }
        }
        //
        ChangeColor(actualColor);
    }
    public void ChangeToPrevColor()
    {
        if (actualColor - 1 >= 0)
        {
            actualColor--;
        }
        else
        {
            if (loopColors)
            {
                actualColor = availableColors.Count - 1;
            }
        }
        //
        ChangeColor(actualColor);
    }

    //Materials

    void ChangeMaterial(int matIndex)
    {
        interactionMesh.material = availableMaterials[matIndex];
    }
    public void ChangeToNextMaterial()
    {
        if (actualMaterial + 1 < availableMaterials.Count)
        {
            actualMaterial++;
        }
        else
        {
            if (loopMaterials)
            {
                actualMaterial = 0;
            }
        }
        //
        ChangeMaterial(actualMaterial);
    }
    public void ChangeToPrevMaterial()
    {
        if (actualMaterial - 1 >= 0)
        {
            actualMaterial--;
        }
        else
        {
            if (loopMaterials)
            {
                actualMaterial = availableMaterials.Count - 1;
            }
        }
        //
        ChangeMaterial(actualMaterial);
    }

    #endregion
}
public enum InteractionType
{
    ChangeMaterial,
    ChangeColor,
    ChangeModel
}