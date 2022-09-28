using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [SerializeField] private LayerMask triggerLayers;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private bool anim = true;


    private void Start()
    {
        if(anim)
         transform.DOLocalMoveY(0.2f, 1f).SetLoops(-1, LoopType.Yoyo);

        Invoke("DestroyAfterTime", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & triggerLayers) == 0) return;

        Destroy(gameObject);
    }


    private void DestroyAfterTime()
    {
        Destroy(gameObject);
    }

}
