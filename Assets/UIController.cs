using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI walletAddress;
    public TextMeshProUGUI tezos;
    [SerializeField] private FadeController fadeController;

    public static UIController instance;

    public FadeController Fade => fadeController;

    private void Start()
    {
        if (instance==null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);
    }
}
