using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private GameObject FadeObj; //フェードのパネル
    public GameObject OptionObj;    //オプションのウインドウ
    public Button button;


    // Start is called before the first frame update
    void Start()
    {
        FadeObj = GameObject.Find("FadePanel");
        OptionObj.SetActive(false);
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //初めからボタンを押したとき
    public void PushStartButton()
    {
        //フェード中入力できなくする処理
        if (FadeObj.GetComponent<FadeManager>().GetFadeInFlg() == false)
        {
            Debug.Log("はじめから　データ初期化");
            //始めからを選択した場合はデータを初期化する
            PlayerPrefs.DeleteAll();
            FadeObj.GetComponent<FadeManager>().FadeScene(1);
        }
    }

    //続きからボタンを押したとき
    public void PushReStartButton()
    {
        //フェード中入力できなくする処理
        if (FadeObj.GetComponent<FadeManager>().GetFadeInFlg() == false)
        {
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
            Debug.Log("オプションを開く");
            OptionObj.SetActive(true);
        }
    }
}