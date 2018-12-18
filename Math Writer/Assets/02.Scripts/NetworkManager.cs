
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using System;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    //socket
    public Socket socket;
    private static string url = "http://127.0.0.1:3000";
    public static string EVENT_CODE = "UNITY";
    public static string EVENT_CODE_NON_MNIST = "UNITY_NON_MNIST";

    //UI
    public Text socketState;
    
    #region SINGLETON
    private static NetworkManager instance;
    public static NetworkManager Instance { get { return instance; } }
    private void SetSingleton() {
        if (Instance != null) {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    private void Awake() {
        SetSingleton();
    }


    #region SOCKET INITIALIZATION
    // Use this for initialization
    void Start() {
        ConnectSocket();
	}

    private void ConnectSocket() {
        socket = IO.Socket(url); //CONNECT SERVER
  
        //REGISTER CALLBACK FROM SERVER
        socket.On(EVENT_CODE,(io)=>{
            RecvPrediction prediction=JsonUtility.FromJson<RecvPrediction>(io.ToString());
           
            print("Receive from server : "+prediction.prediction.ToString());

            GameMgr.Instance.onResponseAnswer(prediction.prediction);
        });

        //REGISTER CALLBACK WHEN IMAGE IS NON-MNIST
        socket.On(EVENT_CODE_NON_MNIST, () => {
            GameMgr.Instance.onResponseNonMNIST();
        });

        socket.On(Socket.EVENT_CONNECT, () => {
            UnityMainThreadDispatcher.Instance().Enqueue(SocketConnectShow());
        });

        socket.On(Socket.EVENT_DISCONNECT, () => {
            UnityMainThreadDispatcher.Instance().Enqueue(SocketDisconnectShow());
        });
    }
    private IEnumerator SocketConnectShow() {
        socketState.color = Color.blue;
        socketState.text = "Connect";
        yield return null;
    }
    private IEnumerator SocketDisconnectShow() {
        socketState.color = Color.red;
        socketState.text = "Disconnect";
        yield return null;
    }

    #endregion

    [Serializable]
    public class RecvPrediction{
        public int prediction; 
    }
}
