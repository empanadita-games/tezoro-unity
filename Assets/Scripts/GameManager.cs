using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private int maxFPS = 60;
    public int tezosCollected;

    public UIController UIController;

    void Awake()
    {
        Application.targetFrameRate = maxFPS;
    }

    private void Start()
    {
        if (instance==null) instance = this;
        else Destroy(instance);
    }

    public void AddTezos(int n)
    {
        tezosCollected += n;
        UIController.instance.tezos.text = tezosCollected.ToString();
    }

    public void StartGetTezos()
    {
        GameController.instance.CallGetTezos(tezosCollected, "tz2W9y2NMFYX8awk6W27q49yaoJoD8uMCBHi");
    }
    
    public void StartWebGetTezos()
    {
        GameController.instance.WebGetTezos(tezosCollected);
    }
}
