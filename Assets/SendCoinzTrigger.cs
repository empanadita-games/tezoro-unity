using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCoinzTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.CollectTezos();
    }
}
