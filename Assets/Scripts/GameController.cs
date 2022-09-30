using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [DllImport("__Internal")] 
        // UNITY -> REACT 
        // SYNC
        private static extern void TrySyncWallet ();
        public void CallTrySyncWallet () {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TrySyncWallet ();
        #endif
        }
        //GET TEZOS
        private static extern void GetTezos (int amount);
        public void CallGetTezos (amount) {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            GetTezos (amount);
        #endif
        }
        //REACT -> UNITY
        //RECEIVE WALLET ADDRESS (ALSO CONFIRMATION OF SUCCESFUL SYNC)
        
        public string walletAddress;

        public void GetWallet (string address) {
            Debug.Log ($"Sync Succesful. Address: {address}");
            walletAddress = address;
            UIController.instance.walletAddress.text = walletAddress;
        }


        public static GameController instance;

        private void Awake()
        {
            if (instance==null) instance = this;
            else Destroy(instance);
            DontDestroyOnLoad(this);
        }
}
