﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextControler : MonoBehaviour
{
    float NakasunaNormalTime; //中砂の通常の時間
    float NakasunaBackTime;   //中砂の逆の時間
    bool PlayerTurnFlg;       //プレイヤーの反転フラグ trueなら反転 falseなら通常
    public PlayerControler PlayerControler; //ゲッター
    string strNowTime;          //string型にする変数
    int seconds; //表示する時間
    GameObject time; //ゲームオブジェクト

    void Start()
    {
        time = GameObject.Find("timer");   // 時間計測用GameObjectの取得
        transform.localScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {
        //変数取得
        NakasunaNormalTime = PlayerControler.PlayerSandNomalTime;
        NakasunaBackTime = PlayerControler.PlayerSandBackTime;
        PlayerTurnFlg = PlayerControler.PlayerTurn;

        //表示変数処理
        if (PlayerTurnFlg == false)
        {
            seconds = (int)NakasunaNormalTime;
            time.GetComponent<Renderer>().material.color = Color.red;
        }
        if (PlayerTurnFlg == true)
        {
            seconds = (int)NakasunaBackTime;
            time.GetComponent<Renderer>().material.color = Color.blue;

        }

        // TextMeshのGameObjectに代入するためにString型にする
        strNowTime = seconds.ToString();
        //表示
        time.GetComponent<TextMesh>().text = strNowTime;
        
        
        //カメラの方へ向かせる処理
        transform.LookAt(Camera.main.transform);
    }
}
