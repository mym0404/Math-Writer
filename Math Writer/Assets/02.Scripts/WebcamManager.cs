

using System;

using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour {
    #region SINGLETON
    private static WebcamManager instance;
    public static WebcamManager Instance { get { return instance; } }
    
    private void SetSingleton() {
        if (instance != null) {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Awake() {
        SetSingleton();
    }
    #endregion

    #region UI
    public RawImage renderer;
    private WebCamTexture webcam;
    
    #endregion
    // Use this for initialization
    void Start() {
        WebCamDevice[ ] devices = WebCamTexture.devices;
        if(devices.Length == 0) {
            GameMgr.Instance.ShowError("Webcam Not Founded");
        } else {
            webcam = new WebCamTexture(devices[0].name);
            webcam.Play();
            renderer.material.mainTexture = webcam;
        }
    }

    public void SendImageToServer() {
        //WEBCAM TEXTURE => GENERAL TEXTURE 2D
        Texture2D texture = new Texture2D(webcam.width , webcam.height);
        texture.SetPixels(webcam.GetPixels());
        texture.Apply();
        //BYTE ENCODING
        byte[ ] bytes;

        bytes = texture.EncodeToPNG();
        //BASE64 ENCODING
        string enc = System.Convert.ToBase64String(bytes);
        var obj=JsonUtility.ToJson(new SendImg(enc));
        //EMIT IMG TO SERVER
        NetworkManager.Instance.socket.Emit(NetworkManager.EVENT_CODE,obj);
    }

    [Serializable]
    public class SendImg{
        public string img;
        public SendImg(string img) {
            this.img = img;
        }
    }
}
