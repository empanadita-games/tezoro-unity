using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    
    [SerializeField] private int maxFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = maxFPS;
    }

    private void Start()
    {
        if (instance==null) instance = this;
        else Destroy(instance);
    }
}
