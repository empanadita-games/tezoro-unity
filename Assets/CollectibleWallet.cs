using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectibleWallet : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalMoveY(0.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
