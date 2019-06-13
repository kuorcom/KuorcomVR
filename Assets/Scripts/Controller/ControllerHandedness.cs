using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHandedness : MonoBehaviour
{
    public GameObject leftHandContainer, rightHandContainer;

#if (!UNITY_EDITOR)

    void Start()
    {
        DetectHandedness();
    }

    private void Update()
    {
        DetectHandedness();
    }

    public void DetectHandedness()
    {
        if(IsRightie())
        {
            if(!rightHandContainer.activeInHierarchy)
                rightHandContainer.SetActive(true);
        }
        else
        {
            if (rightHandContainer.activeInHierarchy)
                rightHandContainer.SetActive(false);
        }

        if(IsLeftie())
        {
            if (!leftHandContainer.activeInHierarchy)
                leftHandContainer.SetActive(true);
        }
        else
        {
            if (leftHandContainer.activeInHierarchy)
                leftHandContainer.SetActive(false);
        }
    }

    public bool IsRightie()
    {
        OVRPlugin.Handedness handedness = OVRPlugin.GetDominantHand();
        if (handedness == OVRPlugin.Handedness.RightHanded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsLeftie()
    {
        OVRPlugin.Handedness handedness = OVRPlugin.GetDominantHand();
        if (handedness == OVRPlugin.Handedness.LeftHanded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

#endif

}
