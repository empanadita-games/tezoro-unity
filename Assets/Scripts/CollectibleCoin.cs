using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    public GameObject coinParticles;
    
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private bool anim = true;


    private void Start()
    {
        if(anim)
         transform.parent.DOLocalMoveY(0.2f, 1f).SetLoops(-1, LoopType.Yoyo);

        Invoke("DestroyAfterTime", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.AddTezos(1);
        Instantiate(coinParticles, transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }


    private void DestroyAfterTime()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

}
