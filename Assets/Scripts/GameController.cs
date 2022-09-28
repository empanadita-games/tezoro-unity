using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameController : MonoBehaviour {
        [DllImport("__Internal")]
        private static extern void TrySyncWallet ();
        public void CallTrySyncWallet () {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TrySyncWallet ();
        #endif
        }

        public static GameController instance;
        
        private void Start()
        {
            if (instance==null) instance = this;
            else Destroy(instance);
        }
}
