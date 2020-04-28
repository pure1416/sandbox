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
    public bool ReturnFlg;
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        ReturnFlg = false;
        FadeObj = GameObject.Find("FadePanel");
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable>();
        sel.Select();
    }

    public void PushReStartButton()
    {
        Debug.Log("ゲームに戻る");
        pauseObj.SetActive(false);
    }
    public void PushReturnStageSerectButton()
    {
        Debug.Log("ステージ選択に戻る");
        pauseObj.SetActive(false);
        FadeObj.GetComponent<FadeManager>().FadeScene(0);
        //SceneManager.LoadScene("WorldSerect");
    }
    public void PushOptionButton()
    {
        Debug.Log("オプションを開く");
    }
    public void ResetButton()
    {
        Debug.Log("リセットボタン");
        pauseObj.SetActive(false);

        // 現在のScene名を取得する
        Scene loadScene = SceneManager.GetActiveScene();
        // Sceneの読み直し
        SceneManager.LoadScene(loadScene.name);
    }
}
