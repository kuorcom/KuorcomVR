using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    Material faderMaterial;
    public float fadeTime = 1.0f;
    public Color fadeOutColor, fadeInColor;

    // Start is called before the first frame update
    void Start()
    {
        faderMaterial = GetComponent<MeshRenderer>().material;
    }

    public void FadeOut()
    {
        StartCoroutine(FaderCoroutine(true));
    }
    public void FadeIn()
    {
        StartCoroutine(FaderCoroutine(false));
    }

    public IEnumerator FaderCoroutine(bool fadeOut)
    {
        float timeHelper = 0.0f;
        float timeAdder = fadeTime / (fadeTime * 60);
        Color initColor = faderMaterial.GetColor("_BaseColor");
        if (fadeOut)
        {
            while (timeHelper < 1)
            {
                faderMaterial.SetColor("_BaseColor", Color.Lerp(initColor, fadeOutColor, timeHelper));
                timeHelper += timeAdder;
                Debug.Log(timeHelper);
                yield return new WaitForSeconds(timeAdder);
            }
        }
        else
        {
            while (timeHelper < 1)
            {
                faderMaterial.SetColor("_BaseColor", Color.Lerp(initColor, fadeInColor, timeHelper));
                timeHelper += timeAdder;
                Debug.Log(timeHelper);
                yield return new WaitForSeconds(timeAdder);
            }
        }
    }
}
