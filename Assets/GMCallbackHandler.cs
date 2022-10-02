using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMCallbackHandler : MonoBehaviour
{
    public void CallbackEndgame()
    {
        GameManager.Instance.FinishGame();
    }
}
