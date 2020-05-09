using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private GameObject FadeObj; //フェードのパネル
    public Button button;


    // Start is called before the first frame update
    void Start()
    {
        FadeObj = GameObject.Find("FadePanel");
        button = GameObject.Find("Start").GetComponent<Button>();
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初めからボタンを押したとき
    public void PushStartButton()
    {
        Debug.Log("はじめから");
        FadeObj.GetComponent<FadeManager>().FadeScene(1);
    }

    //続きからボタンを押したとき
    public void PushReStartButton()
    {
        Debug.Log("つづきから");
        FadeObj.GetComponent<FadeManager>().FadeScene(1);
    }

    //オプションを押したとき
    public void PushOptionButton()
    {
        Debug.Log("オプションを開く");
    }
}
