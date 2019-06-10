using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    Material faderMaterial;
    public float fadeTime = 1.0f;
    public AnimationCurve fadeInCurve, fadeOutCurve;
    public Color fadeOutColor, fadeInColor;

    // Start is called before the first frame update
    void Start()
    {
        faderMaterial = GetComponent<MeshRenderer>().material;
        FadeIn();
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
        Color initColor = faderMaterial.GetColor("_BaseColor");
        if (fadeOut)
        {
            while (timeHelper < fadeTime)
            {
                timeHelper += Time.deltaTime;
                if(timeHelper > fadeTime)
                {
                    timeHelper = fadeTime;
                }
                yield return null;
                faderMaterial.SetColor("_BaseColor", Color.Lerp(initColor, fadeOutColor, fadeOutCurve.Evaluate(timeHelper / fadeTime)));
            }
        }
        else
        {
            while (timeHelper < fadeTime)
            {
                timeHelper += Time.deltaTime;
                if (timeHelper > fadeTime)
                {
                    timeHelper = fadeTime;
                }
                yield return null;
                faderMaterial.SetColor("_BaseColor", Color.Lerp(initColor, fadeInColor, fadeInCurve.Evaluate(timeHelper / fadeTime)));
            }
        }
    }
}
