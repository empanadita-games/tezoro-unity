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

    public static UIController Instance;

    public FadeController Fade => fadeController;

    private void Start()
    {
        if (Instance==null) Instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);
    }
}
