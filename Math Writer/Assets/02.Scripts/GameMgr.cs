using System.Collections;
using EasyUIAnimator;
using UnityEngine;
using UnityEngine.UI;

public enum Operation { ADD = 0, SUB = 1, MUL = 2 };

public class GameMgr : MonoBehaviour {
    #region SINGLETON
    private static GameMgr instance;
    public static GameMgr Instance { get { return instance; } }
    private void SetSingleton() {
        if (instance != null) {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region VARS
    private bool responsed = false;
    private int answer, num1, num2;
    private Operation op;
    #endregion

    #region UI
    public Button btn_submit;
    public Text text_num1, text_num2, text_op, text_answer;
    public Text text_err;
    public Image image_correct, image_wrong;
    #endregion

    #region SOUND
    public AudioClip Non_MNIST_error_sfx;
    #endregion


    private void Awake() {
        Screen.SetResolution(768 , 1024 , false);
        SetSingleton();
    }


    // Use this for initialization
    void Start() {
        setQuestion();

       
    }

    #region SUBMIT
    public void onClickSubmitButton() {
        responsed = false;
        StartCoroutine(Submit());
    }
    IEnumerator Submit() {
        WebcamManager.Instance.SendImageToServer();

        yield return new WaitUntil(() => responsed);
    }
    #endregion


    #region RESPONSE ANSWER
    public void onResponseAnswer(int prediction) {
        UnityMainThreadDispatcher.Instance().Enqueue(HandleAnswer1(prediction));
    }
    public IEnumerator HandleAnswer1(int prediction) {
        text_answer.text = prediction.ToString();

        if(answer == prediction) {
            image_correct.gameObject.SetActive(true);
            image_correct.GetComponent<UIParallelAnimation>().Play();
        } else {
            image_wrong.gameObject.SetActive(true);
            image_wrong.GetComponent<UIParallelAnimation>().Play();
        }
        yield return new WaitForSeconds(3.0f);

        text_answer.text = "";
        setQuestion();
    }

    public void onResponseNonMNIST() {

        UnityMainThreadDispatcher.Instance().Enqueue(HandleNonMNIST());
    }
    public IEnumerator HandleNonMNIST() {
        ShowError("This is not MNIST image!");

        yield return null;
    }

    public void ShowError(string msg) {
        text_err.gameObject.SetActive(true);
        text_err.text = msg;
        text_err.GetComponent<UIParallelAnimation>().Play();
        SoundManager.Instance.PlaySfx(Non_MNIST_error_sfx, false);
    }

    #endregion



    //WHAT IS YOUR QUESTION
    private void setQuestion() {
        op = (Operation)Random.Range(0, 4);

        switch(op) {

            case Operation.ADD:
                do {
                    num1 = Random.Range(0, 4);
                    num2 = Random.Range(0, 4);
                } while(!(0 <= num1 + num2 && num1 + num2 <= 9));
                answer = num1 + num2; text_op.text = "＋"; break;
            case Operation.SUB:
                do {
                    num1 = Random.Range(5, 10);
                    num2 = Random.Range(0, 5);
                } while(!(0 <= num1 - num2 && num1 - num2 <= 9));
                answer = num1 - num2; text_op.text = "－";break;
            case Operation.MUL:
                do {
                    num1 = Random.Range(1, 5);
                    num2 = Random.Range(1, 5);
                } while(!(0 <= num1 * num2 && num1 * num2 <= 9));
                answer = num1 * num2; text_op.text = "×";break;

        }

        text_num1.text = num1.ToString();
        text_num2.text = num2.ToString(); 


    }
}
