using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearManager : MonoBehaviour
{
    GameObject PlayerObj;
    GameObject ClearUI;
    GameObject FadeObject;

    bool GameClearFlg;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        GameClearFlg = false;
        PlayerObj = GameObject.Find("player");
        ClearUI = GameObject.Find("GameClear");
        FadeObject = GameObject.Find("FadePanel");

        button = GameObject.Find("NextStage").GetComponent<Button>();
        //ボタンが選択された状態になる
        button.Select();

    }

    // Update is called once per frame
    void Update()
    {
        GameClearFlg = PlayerObj.GetComponent<PlayerControler>().GetGameClearFlg();
        if(GameClearFlg == true)
        {
            ClearUI.SetActive(true);
        }
        else if (GameClearFlg == false)
        {
            ClearUI.SetActive(false);
        }
    }


    public void PushNextStageButton()
    {
        Debug.Log("次のステージへ");

    }
    public void PushReturnStageSelectButton()
    {
        Debug.Log("ステージ選択へ");

        FadeObject.GetComponent<FadeManager>().FadeScene(0);
    }
}
