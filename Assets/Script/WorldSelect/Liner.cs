﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liner : MonoBehaviour
{
    //定数宣言
    private const int STAY = 0;
    private const int NEXT = 1;
    private const int PREV = -1;

    [Header("移動時間"), SerializeField, Range(0, 10)]
    public float MoveTime;   //移動時間

    [SerializeField]
    public Vector3 MoveDist;        //移動距離

    private float NowTime;          //経過時間
    private Vector3 StartPos;       //開始位置
    private Vector3 EndPos;         //終了位置

    private bool MoveEnd;           //移動終了フラグ
    private int MoveNP;             //+1：Next　-1：Prev

    public WSManager WSMObj;       //ワールドセレクトマネージャーのオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        NowTime = 0.0f;
        MoveEnd = true;
        WSMObj = GameObject.FindWithTag("WSManager").GetComponent<WSManager>();
        WSMObj.SetMoveEndFlg(MoveEnd);
    }

    // Update is called once per frame
    void Update()
    {
        if (!MoveEnd)
        {
            if (NowTime <= MoveTime)
            {
                NowTime = Time.deltaTime + NowTime;

                //進行度で位置を決める
                var rate = NowTime / MoveTime;
                transform.position = Vector3.Lerp(StartPos, EndPos, rate);
            }
            else if (NowTime > MoveTime)
            {
                this.transform.position = EndPos;
                //選択ワールドの更新
                WSMObj.SetNowSelWorld(MoveNP);
                MoveNP = STAY;

                MoveEnd = true;
                WSMObj.SetMoveEndFlg(MoveEnd);
            }
        }
    }

    //右に移動(Next)
    public void GoNext()
    {
        if (MoveEnd && WSMObj.GetNextUnlock())
        {
            StartPos = this.transform.position;
            //終了位置計算
            EndPos = StartPos - MoveDist;
            NowTime = 0.0f;
            MoveNP = NEXT;
            MoveEnd = false;
            WSMObj.SetMoveEndFlg(MoveEnd);
        }
    }

    //左に移動(Prev)
    public void GoPrev()
    {
        if (MoveEnd && WSMObj.GetPrevUnlock())
        {
            StartPos = this.transform.position;
            //終了位置計算
            EndPos = StartPos + MoveDist;
            NowTime = 0.0f;
            MoveNP = PREV;
            MoveEnd = false;
            WSMObj.SetMoveEndFlg(MoveEnd);
        }
    }

    //移動時間の最低値を決めとく
    private void OnValidate()
    {
        if (MoveTime <= 0)
        {
            MoveTime = 0.5f;
        }
    }
}
