using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    GameObject FadeObj; //フェードのパネル
    public GameObject pauseObj;

    // Start is called before the first frame update
    void Start()
    {
        FadeObj = GameObject.Find("FadePanel");
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable>();
        sel.Select();
    }

    public void PushReStartButton()
    {
        Debug.Log("ゲームに戻る");
        pauseObj.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PushReturnStageSerectButton()
    {
        Debug.Log("ステージ選択に戻る");
        //FadeObj.GetComponent<FadeManager>().FadeScene(0);
        SceneManager.LoadScene("WorldSerect");

    }
    public void PushOptionButton()
    {
        Debug.Log("オプションを開く");
    }
}
