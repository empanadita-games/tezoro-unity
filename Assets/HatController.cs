using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    public static HatController instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<GameObject> hats;

    public void HighlightHat(int n)
    {
        Debug.Log("Selecting random hat");
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].SetActive(false);
            if (i==n) hats[i].SetActive(true);
        }
    }
    
}
