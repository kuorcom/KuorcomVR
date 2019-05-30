﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportElement : MonoBehaviour
{
    public Transform teleportPoint;
    Transform playerTransform;
    Fader fader;

    private void Awake()
    {
        fader = FindObjectOfType<Fader>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #region Teleportation

    public void Teleport()
    {
        StartCoroutine(TeleportCoroutine());
    }

    public void TeleportViewer()
    {
        playerTransform.position = teleportPoint.position;
    }

    public IEnumerator TeleportCoroutine()
    {
        yield return StartCoroutine(fader.FaderCoroutine(false));
        TeleportViewer();
        yield return StartCoroutine(fader.FaderCoroutine(true));
    }

    #endregion
}
