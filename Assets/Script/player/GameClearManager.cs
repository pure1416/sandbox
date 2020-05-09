using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    GameObject PlayerObj;
    GameObject ClearUI;
    GameObject FadeObject;

    bool GameClearFlg;
    public Button button;
    bool ButtonSelectFlg;
    // Start is called before the first frame update
    void Start()
    {
        GameClearFlg = false;
        PlayerObj = GameObject.Find("player");
        ClearUI = GameObject.Find("GameClearUI");
        FadeObject = GameObject.Find("FadePanel");
        ButtonSelectFlg = false;
        //button = GameObject.Find("NextStage").GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {
        GameClearFlg = PlayerObj.GetComponent<PlayerControler>().GetGameClearFlg();
        if(GameClearFlg == true)
        {
            if (ButtonSelectFlg == false)
            {
                button.Select();
                ButtonSelectFlg = true;

            }

            ClearUI.SetActive(true);
        }
        else if (GameClearFlg == false)
        {
            //ボタンが選択された状態になる
            ClearUI.SetActive(false);
        }
    }


    public void PushNextStageButton()
    {
        Debug.Log("次のステージへ");
        // 現在のScene名を取得する
        //Scene loadScene = SceneManager.GetActiveScene();
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Sceneの読み直し
        SceneManager.LoadScene(NextSceneIndex);
    }
    public void PushReturnStageSelectButton()
    {
        Debug.Log("ステージ選択へ");

        FadeObject.GetComponent<FadeManager>().FadeScene(1);
    }
}
