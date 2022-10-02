using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeSkipLevel : MonoBehaviour
{
    public void SkipLevel()
    {
        GameController.instance.SetWallet("tz1a33jZp79RSoJz3aswEUMJ3dQuQcpwCtsF");
        Debug.Log("Using cheat code and setting hardcoded wallet");
        GameManager.Instance.loadSceneController.LoadNextScene();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.I))
        {
         SkipLevel();   
        }
    }
}
