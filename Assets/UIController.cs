using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshPro walletAddress;
    public TextMeshPro tezos;

    public static UIController instance;

    private void Start()
    {
        if (instance==null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);
    }
}
