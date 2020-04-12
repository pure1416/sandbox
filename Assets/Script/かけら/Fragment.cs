﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;


public class Fragment : MonoBehaviour
{

    // プレイヤーのスクリプト
    public GameObject playercontroler;

    // 変数宣言
    bool P_SandEnpflg;              // プレイヤーの中砂の有無
    float SinkTime;                 // 沈む時間
    int SinkCount;                  // 何秒かけて沈むか？

    [SerializeField] Vector3 B_Col_Center;           // BoxColliderのCenter
    [SerializeField] Vector3 B_Col_Size;             // BoxColliderのSize

    // 当たり判定
    BoxCollider FtCol;
    Rigidbody rb;

    [SerializeField] Vector3 SandMoveFtSp;  // 流砂の移動力

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        playercontroler = GameObject.FindGameObjectWithTag("Player");

        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);

        SinkTime = 0.0f;
        SinkCount = 0;
        B_Col_Center = new Vector3(0.0f,0.0f,0.0f);
        B_Col_Size = new Vector3(1.0f, 1.0f, 1.0f);
        P_SandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty(); ;

        FtCol = GetComponent<BoxCollider>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの中砂の有無を常にもってくる
        P_SandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty();

        // プレイヤーの中砂が落ちきっていたら埋まる処理（徐々に埋まっていく感じに変更予定）
        FtCol.center = new Vector3(B_Col_Center.x, B_Col_Center.y, B_Col_Center.z);
        FtCol.size = new Vector3(B_Col_Size.x, B_Col_Size.y, B_Col_Size.z);
    }

    private void OnTriggerStay(Collider other)
    {
        // 流砂の上にいるときに流砂の移動力を受け取る
        if (other.gameObject.tag == "QuickSand_B")
        {
            SandMoveFtSp = other.gameObject.GetComponent<Quicksand>().GetSandMove();
            SandMoveFtSp /= 50;
            this.transform.Translate(SandMoveFtSp);

            // 流砂がｙ方向に力がかかっていなければ重力を付ける
            if (SandMoveFtSp.y == 0.0f)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
            }
            // 流砂がｙ方向に力がかかっていたら重力を切る
            else
            {
                this.GetComponent<Rigidbody>().useGravity = false;
            }

            // 大体17秒かけて半分埋まりきるイメージ？(仮)どのくらい埋まるのか、何秒かけて埋まるのか決まり次第で変更予定
            if (SinkCount < 170)
            {
                SinkTime += Time.deltaTime;
            }

            // プレイヤーの中砂がないとき徐々に埋まっていく(BoxColliderの位置と大きさを変えてます)
            if (P_SandEnpflg == true)
            {
                if (SinkTime >= 0.1f)
                {
                    SinkTime = 0.0f;
                    SinkCount += 1;
                    B_Col_Center.y += 0.0025f;
                    B_Col_Size.y -= 0.005f;
                }
            }
        }

        if (other.gameObject.tag == "Mud")
        {
            SandMoveFtSp = other.gameObject.GetComponent<FlowingSand>().GetFlowingSandMove();
            SandMoveFtSp /= 50;
            this.transform.Translate(SandMoveFtSp);

            // 流砂がｙ方向に力がかかっていなければ重力を付ける
            if (SandMoveFtSp.y == 0.0f)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
            }
            // 流砂がｙ方向に力がかかっていたら重力を切る
            else
            {
                this.GetComponent<Rigidbody>().useGravity = false;
            }

            // 大体17秒かけて半分埋まりきるイメージ？(仮)どのくらい埋まるのか、何秒かけて埋まるのか決まり次第で変更予定
            if (SinkCount < 170)
            {
                SinkTime += Time.deltaTime;
            }

            // プレイヤーの中砂がないとき徐々に埋まっていく(BoxColliderの位置と大きさを変えてます)
            if (P_SandEnpflg == true)
            {
                if (SinkTime >= 0.1f)
                {
                    SinkTime = 0.0f;
                    SinkCount += 1;
                    B_Col_Center.y += 0.0025f;
                    B_Col_Size.y -= 0.005f;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {    //流砂から出たら埋まっているのを解除する
        if (other.gameObject.tag == "NotSand")
        { 
            SinkCount = 0;
            B_Col_Center = new Vector3(0.0f, 0.0f, 0.0f);
            B_Col_Size = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (other.gameObject.tag == "QuickSand_B")
        {
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (other.gameObject.tag == "Mud")
        {
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }
  
}
