using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    // UNITY -> REACT 
    // SYNC
    [DllImport("__Internal")]
    private static extern void TrySyncWallet ();
        public void CallTrySyncWallet () {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            TrySyncWallet ();
        #endif
        }
        // //GET TEZOS REACT
    [DllImport("__Internal")]
    private static extern void ReactGetTezos (int amount, string walletAddress);
        public void CallGetTezos (int callAmount, string callWalletAddress) {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            ReactGetTezos (callAmount, callWalletAddress);
        #endif
        }
        [DllImport("__Internal")]
        private static extern void BuyHat ();
        public void CallBuyHat () {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            BuyHat ();
        #endif
        }

        //REACT -> UNITY
        //RECEIVE WALLET ADDRESS (ALSO CONFIRMATION OF SUCCESFUL SYNC)
        
        public string walletAddress;
        public UnityEvent onWalletSynced;

        //THIS METHOD SHOULD BE CALLED FROM REACT
        public void SetWallet (string address) {
            Debug.Log ($"Sync Succesful. Address: {address}");
            walletAddress = address;
            UIController.Instance.walletAddress.text = walletAddress;
            onWalletSynced.Invoke();
        }
        
        public static GameController instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(instance);
            DontDestroyOnLoad(this);

            onWalletSynced.AddListener(() => { UIController.Instance.walletAddress.text = walletAddress; }
            );
        }

        //WEB REQUEST VERSION OF GETTEZOS

        public void WebGetTezos(int amount) => StartCoroutine(Coro_WebGetTezos(amount));
        
        public IEnumerator Coro_WebGetTezos(int amount)
        {
            if (String.IsNullOrEmpty(walletAddress))
            {
                Debug.Log("Error! Trying to send tezos with no destination wallet.");
                yield return null;
            }
            
            if (amount<1)
            {
                Debug.Log("Error! Trying to send <1 tezos");
                yield return null;
            }
            
            WWWForm form = new WWWForm();
            form.AddField("amount", amount.ToString());
            form.AddField("address", walletAddress);
            
            string requestURL = "https://remote-signer.herokuapp.com/sendTez";

            UnityWebRequest request = UnityWebRequest.Post(requestURL, form);
            //request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(requestURL + ":\nError: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(requestURL + ":\nHTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(requestURL + ":\nReceived: " + request.downloadHandler.text);
                    Debug.Log("tezos request successful");
                    break;
                default:
                    Debug.Log(requestURL + ":\n" + request.result);
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                #if UNITY_EDITOR == true
                SetWallet("tz2W9y2NMFYX8awk6W27q49yaoJoD8uMCBHi");
                #endif
            }
        }
}

[Serializable]
public class TezosRequestData
{
    public int amount;
    public string address;
}
