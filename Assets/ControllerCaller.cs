using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCaller : MonoBehaviour
{
    public void CallbackGetHat()
    {
        //GameController.instance.CallBuyHat();
        GameController.instance.SendObjkt();
    }
}
