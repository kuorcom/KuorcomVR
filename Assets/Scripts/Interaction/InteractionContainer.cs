using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionContainer : MonoBehaviour
{
    public bool loopModels = true;
    public List<GameObject> interactionModels = new List<GameObject>();
    int actualModel = 0;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform t in transform)
        {
            interactionModels.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    public void InitModel()
    {
        ChangeModel(actualModel);
    }

    void ChangeModel(int modelIndex)
    {
        foreach(GameObject g in interactionModels)
        {
            g.SetActive(false);
        }
        //
        interactionModels[actualModel].SetActive(true);
    }
    public void ChangeToNextModel()
    {
        if (actualModel + 1 < interactionModels.Count)
        {
            actualModel++;
        }
        else
        {
            if (loopModels)
            {
                actualModel = 0;
            }
        }
        //
        ChangeModel(actualModel);
    }
    public void ChangeToPrevModel()
    {
        if (actualModel - 1 >= 0)
        {
            actualModel--;
        }
        else
        {
            if (loopModels)
            {
                actualModel = interactionModels.Count - 1;
            }
        }
        //
        ChangeModel(actualModel);
    }
}
