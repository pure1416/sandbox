using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class TitleManager : MonoBehaviour
{
    private GameObject FadeObj; //フェードのパネル
    public GameObject OptionObj;    //オプションのウインドウ
    public Button button;

    [SerializeField] AudioClip[] clips;//サウンド

    //SEです。
    protected AudioSource Source;


    // Start is called before the first frame update
    void Start()
    {
        FadeObj = GameObject.Find("FadePanel");
        OptionObj.SetActive(false);
        button.Select();

        //サウンド
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ////キー操作で操作できるようにする
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
        //{
           
        //        //カーソル選択音
        //        Source.PlayOneShot(clips[0]);
           
        //}
        //else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
        //{
          
        //        //カーソル選択音
        //        Source.PlayOneShot(clips[0]);
           
        //}
    }

    //初めからボタンを押したとき
    public void PushStartButton()
    {
        //フェード中入力できなくする処理
        if (FadeObj.GetComponent<FadeManager>().GetFadeInFlg() == false)
        {
            //決定の際のSE
            Source.PlayOneShot(clips[1]);

            Debug.Log("はじめから　データ初期化");
            //始めからを選択した場合はデータを初期化する
            PlayerPrefs.DeleteKey("STAGE_FLAG_1");
            PlayerPrefs.DeleteKey("STAGE_FLAG_2");
            PlayerPrefs.DeleteKey("STAGE_FLAG_3");
            PlayerPrefs.DeleteKey("STAGE_FLAG_4");
            PlayerPrefs.DeleteKey("WORLD_FLAG");
            FadeObj.GetComponent<FadeManager>().FadeScene(1);
        }
    }

    //続きからボタンを押したとき
    public void PushReStartButton()
    {
        //フェード中入力できなくする処理
        if (FadeObj.GetComponent<FadeManager>().GetFadeInFlg() == false)
        {
            //決定の際のSE
            Source.PlayOneShot(clips[1]);

            Debug.Log("つづきから");
            FadeObj.GetComponent<FadeManager>().FadeScene(1);
        }
    }

    //オプションを押したとき
    public void PushOptionButton()
    {
        //フェード中入力できなくする処理
        if (FadeObj.GetComponent<FadeManager>().GetFadeInFlg() == false)
        {
            //決定の際のSE
            Source.PlayOneShot(clips[1]);

            Debug.Log("オプションを開く");
            OptionObj.SetActive(true);
        }
    }
}