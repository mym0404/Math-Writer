using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour {

    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance {
        get {
            return instance;
        }
    }
    private void SingletonSet() {
        if(instance != null) {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private void Awake() {
        SingletonSet();

        Screen.SetResolution(1280, 800, true);
    }
    //sfx관련 변수,함수
    public static bool isBgmMute = false;
    public static bool isEffectMute = false;
    public static float effectVolume = 0.3f;
    public static float bgmVolume = 0.3f;

    public void PlaySfx(AudioClip sfx, bool isBgm) {
        //만약 음소거 옵션이 설정되있으면 함수를 반환
        //if (isSfxMute == true)
        //return;
        if(isBgm == true && isBgmMute == true)
            return;
        if(isEffectMute == true && isBgm == false)
            return;
        //게임 오브젝트를 동적으로 생성 
        GameObject soundObj = new GameObject("Sfx");
        soundObj.tag = "SFX";
        //사운드 발생 위치 지정
        soundObj.GetComponent<Transform>().position = Vector2.zero;
        //생성한 게임오브젝트에 AudioSource 컴포넌트 추가
        AudioSource audio = soundObj.AddComponent<AudioSource>();
        //컴포넌트 속성 설정    
        audio.clip = sfx;
        //전체적인 볼륨 설정
        if(isBgm == true)
            audio.volume = bgmVolume;
        else
            audio.volume = effectVolume;

        //오디오클립 재생
        audio.Play();
        //사운드의 플레이가 종료되면 동적으로 생성한 게임오브젝트를 
        Destroy(soundObj, sfx.length);
    }



}
