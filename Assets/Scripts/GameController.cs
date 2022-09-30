using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    [DllImport("__Internal")] 
        // UNITY -> REACT 
        // SYNC
        private static extern void TrySyncWallet ();
        public void CallTrySyncWallet () {
        // #if UNITY_WEBGL == true && UNITY_EDITOR == false
        //     TrySyncWallet ();
        // #endif
        }
        // //GET TEZOS REACT
        // private static extern void GetTezos (int amount);
        public void CallGetTezos (int callAmount) {
        // #if UNITY_WEBGL == true && UNITY_EDITOR == false
        //     GetTezos (callAmount);
        // #endif
        }

        //REACT -> UNITY
        //RECEIVE WALLET ADDRESS (ALSO CONFIRMATION OF SUCCESFUL SYNC)
        
        public string walletAddress;
        public UnityEvent onWalletSynced;

        //THIS METHOD SHOULD BE CALLED FROM REACT
        public void GetWallet (string address) {
            Debug.Log ($"Sync Succesful. Address: {address}");
            walletAddress = address;
            UIController.instance.walletAddress.text = walletAddress;
        }
        
        public static GameController instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(instance);
            DontDestroyOnLoad(this);

            onWalletSynced.AddListener(() => { UIController.instance.walletAddress.text = walletAddress; }
            );
        }

        //WEB REQUEST VERSION OF GETTEZOS

        public void WebGetTezos(int amount) => StartCoroutine(Coro_WebGetTezos(amount));
        
        public IEnumerator Coro_WebGetTezos(int amount)
        {
            string requestURL = "https://remote-signer.herokuapp.com/sendTez";
            
            var myData = new TezosRequestData();
            //myData.address = walletAddress;
            //myData.amount = GameManager.instance.tezosCollected;
            myData.address = "tz2W9y2NMFYX8awk6W27q49yaoJoD8uMCBHi";
            myData.amount = 3;

            string jsonString = JsonUtility.ToJson(myData);
            
            UnityWebRequest request = UnityWebRequest.Put(requestURL, jsonString);
            request.SetRequestHeader("Content-Type", "application/json");
            Debug.Log("Sending web request:" + requestURL + " " + jsonString);
            yield return request.SendWebRequest();
        }
}

[Serializable]
public class TezosRequestData
{
    public int amount;
    public string address;
}
