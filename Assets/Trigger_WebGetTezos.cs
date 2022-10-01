using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_WebGetTezos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.StartWebGetTezos();
    }
}
