using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectibleWallet : MonoBehaviour
{
    public GameObject debugText;
    
    private void Start()
    {
        transform.DOMoveY(0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameController.instance.CallTrySyncWallet();
        debugText.SetActive(true);
        StartCoroutine(Coro_HideDebugText());
    }

    IEnumerator Coro_HideDebugText()
    {
        yield return new WaitForSeconds(2f);
        debugText.SetActive(false);
    }
}
