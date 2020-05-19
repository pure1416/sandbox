using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    private GameObject FadeObj; //フェードのパネル
    public GameObject pauseObj;
    public GameObject ExplanationObj;
    public bool ReturnFlg;

    public Button ReStartButton;

    bool SelectButton;
    //Button ResetButton;
    //Button ExplanationButton;
    //Button ReturnStageSerectButton;


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        ReturnFlg = false;
        FadeObj = GameObject.Find("FadePanel");
        ExplanationObj.SetActive(false);
        SelectButton = false;
        //ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        //ExplanationButton = GameObject.Find("ExplanationButton").GetComponent<Button>();
        //ReturnStageSerectButton = GameObject.Find("ReturnStageSerectButton").GetComponent<Button>();

        // 最初に選択状態にしたいボタンの設定
    }
    void Update()
    {
 
        if (SelectButton == false)
        {
            if (pauseObj.activeSelf == true)
            {
                ReStartButton.Select();
                SelectButton = true;

            }
        }
    }

    public void PushReStartButton()
    {
        //操作説明が出ていないとき
        if (!ExplanationObj.activeSelf)
        {
            Debug.Log("ゲームに戻る");
            pauseObj.SetActive(false);
        }
    }
    public void PushReturnStageSerectButton()
    {
        //操作説明が出ていないとき
        if (!ExplanationObj.activeSelf)
        {
            Debug.Log("ステージ選択に戻る");
            pauseObj.SetActive(false);
            FadeObj.GetComponent<FadeManager>().FadeScene(1);
            //SceneManager.LoadScene("WorldSerect");
        }
    }
    public void PushExplanationButton()
    {
        //操作説明が出ていないとき
        if (!ExplanationObj.activeSelf)
        {
            Debug.Log("操作説明を開く");
            ExplanationObj.SetActive(true);
        }
    }
    public void ResetButton()
    {        //操作説明が出ていないとき
        if (!ExplanationObj.activeSelf)
        {
            Debug.Log("リセットボタン");
            pauseObj.SetActive(false);

            // 現在のScene名を取得する
            Scene loadScene = SceneManager.GetActiveScene();
            // Sceneの読み直し
            SceneManager.LoadScene(loadScene.name);
        }
    }
}
