using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    //変数宣言
    public float PlayerSp;       //プレイヤーの速度
    public bool  PlayerTurn;     //プレイヤーの反転 trueなら反転 falseなら通常
    int          PlayerTime;     //中砂の時間
    public float PlayerTotalTime;//中砂の合計の時間
    bool PlayerEnptyFlg;         //中砂が落ちきっているか判定

    float PlayerSandNomalTime;   //通常に流れるほうの砂の時間
    float PlayerSandBackTime;    //逆行して流れる砂の時間

    //入力変数
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;                //当たり判定



    // Start is called before the first frame update
    void Start()
    {
        //変数初期化
        PlayerEnptyFlg = true;
        PlayerSandNomalTime = PlayerTotalTime;
        PlayerSandBackTime = 0.0f;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //変数宣言
        var PlayerDir = new Vector3(0.0f, 0.0f, 0.0f);   //プレイヤーの方向
        var SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);  //流砂の移動力(のちにセッターとなる)

        //入力処理
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        //デバッグ
        PlayerTime = (int)PlayerSandNomalTime;
        //Debug.Log(PlayerTurn);
        Debug.Log("中砂の上が空かどうか" + PlayerEnptyFlg);
        //Debug.Log("逆行の中砂の時間" + PlayerSandBackTime);
        //Debug.Log("通常の中砂の時間" + PlayerSandNomalTime);
        Debug.Log("中砂の時間" + PlayerTime);

        //=========================================================================================
        //移動処理
        //カメラの方向に準じて移動
        //=========================================================================================
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * PlayerSp + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        Debug.DrawLine(new Vector3(2.0f, 0.0f, 0.0f),
                new Vector3(2.0f, 5.0f, 5.0f),
                Color.green);

        //=========================================================================================
        //回転処理
        //=========================================================================================
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //時間逆行から通常へ変換
            if (PlayerTurn == true)
            {
                PlayerEnptyFlg = false;
                PlayerTurn = false;
                PlayerSandNomalTime = PlayerTotalTime - PlayerSandBackTime; //通常の中砂 を すべての中砂 から 逆行の中砂 を引いた分にする
            }
            //通常から時間逆行へ変換
            else
            {
                PlayerEnptyFlg = false;
                PlayerTurn = true;
                PlayerSandBackTime = PlayerTotalTime - PlayerSandNomalTime;  //逆行の中砂 を 全ての中砂 から 逆行の中砂 を引いた分にする
            }
        }

        //=========================================================================================
        //時間処理
        //=========================================================================================
        //時間逆行の向きの場合
        if (PlayerTurn == true)
        {
            //中砂が残っている場合
            if (PlayerSandBackTime > 0.0f)
            {
                PlayerSandBackTime -= Time.deltaTime;
            }
            //時間逆行の中砂が落ちきった場合
            if(PlayerSandBackTime <= 0.0f)
            {
                PlayerSandBackTime = 0.0f;
                PlayerEnptyFlg = true;
            }
        }

        //通常の向きの場合
        if (PlayerTurn == false)
        {
            //中砂が残っている場合
            if (PlayerSandNomalTime > 0.0f)
            {
                PlayerSandNomalTime -= Time.deltaTime;
            }
            //時間逆行の中砂が落ちきった場合
            if (PlayerSandNomalTime <= 0.0f)
            {
                PlayerSandNomalTime = 0.0f;
                PlayerEnptyFlg = true;
            }
        }

    }

    //プレイヤーが反転しているかどうかの変数Getter
    public bool GetPlayerTurn()
    {
        return PlayerTurn;
    }

    //プレイヤーの中砂が流れ落ちているかどうかの変数のGetter
    public bool GetPlayerEnpty()
    {
        return PlayerEnptyFlg;
    }   
}
