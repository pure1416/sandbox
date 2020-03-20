using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weight;

public class Quicksand : MonoBehaviour
{
    [Header("流砂の流れの設定")]
    [Tooltip("x,y,z　それぞれに -1.0 ~ 1.0 の値を入れてください(超過対応済み)")]
    public Vector3           SandDir;           //流砂の正方向
    [Tooltip("ここは見る用の場所なので、値は入れないでください")]
    [SerializeField] Vector3 InvSandDir;        //流砂の逆方向
    [Tooltip("流砂が流れる速度を設定できます")]
    public float             SandSpd;           //流れる速度

    [Header("各種フラグ")]
    [SerializeField] bool    IsInverse;         //流れの正逆判定(正：false、逆：true)
    [SerializeField] bool    IsStop;            //流れの停止判定(動：false、止：true)

    [Header("その他")]
    [SerializeField] private Vector3 SandMove;  //流砂の移動力(方向 * 速度)
    public static WEIGHT  SandWeight;           //重さ

    public GameObject playerControler;

    // Start is called before the first frame update
    void Start()
    {
        //逆ベクトルを保存
        InvSandDir = SandDir * -1.0f;
        //正逆判定をfalse
        IsInverse = false;
        //停止判定をfalse
        IsStop = false;

        playerControler = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの正逆、停止と同期する
        IsInverse = playerControler.GetComponent<PlayerControler>().GetPlayerTurn();
        IsStop = playerControler.GetComponent<PlayerControler>().GetPlayerEnpty();

        //毎フレーム移動力を計算する
        SandMove = MoveCal(IsInverse, IsStop);
    }

    //移動力の計算をする関数
    private Vector3 MoveCal(bool inv,bool stop)
    {
        //止まっている状態
        if(stop)
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
        else
        {
            //正
            if(!inv)
            {
                return SandDir * SandSpd;
            }
            //逆
            else
            {
                return InvSandDir * SandSpd;
            }
        }
    }

    //移動力のGetter
    public Vector3 GetSandMove()
    {
        return SandMove;
    }

    //インスペクタで値が変更された時に発動する関数
    private void OnValidate()
    {
        //SandDirの値を設定するときの上限設定
        {
            //x
            if (SandDir.x > 1.0f)
            {
                SandDir.x = 1.0f;
            }
            else if (SandDir.x < -1.0f)
            {
                SandDir.x = -1.0f;
            }

            //y
            if (SandDir.y > 1.0f)
            {
                SandDir.y = 1.0f;
            }
            else if (SandDir.y < -1.0f)
            {
                SandDir.y = -1.0f;
            }

            //z
            if (SandDir.z > 1.0f)
            {
                SandDir.z = 1.0f;
            }
            else if (SandDir.z < -1.0f)
            {
                SandDir.z = -1.0f;
            }
        }

        //SandSpdの値が０以下にならないようにする
        if(SandSpd <= 1.0f)
        {
            SandSpd = 1.0f;
        }
    }
}
