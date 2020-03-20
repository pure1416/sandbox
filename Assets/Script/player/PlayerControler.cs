using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;

public class PlayerControler : MonoBehaviour
{
    //変数宣言
    public float PlayerSp;       //プレイヤーの速度
    public bool  PlayerTurn;     //プレイヤーの反転 trueなら反転 falseなら通常
    int          PlayerTime;     //中砂の時間
    public float PlayerTotalTime;//中砂の合計の時間
    bool PlayerEnptyFlg;         //中砂が落ちきっているか判定
    Vector3 PlayerDir;   //プレイヤーの方向
    Vector3 SandMoveSp;  //流砂の移動力
    float PlayerSandNomalTime;   //通常に流れるほうの砂の時間
    float PlayerSandBackTime;    //逆行して流れる砂の時間


    //入力変数
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;                //当たり判定

    [TextAreaAttribute, SerializeField] string ReadMe; 

    // Start is called before the first frame update
    void Start()
    {
        //変数初期化
        PlayerEnptyFlg = true;
        PlayerSandNomalTime = PlayerTotalTime;
        PlayerSandBackTime = 0.0f;
        PlayerDir = new Vector3(0.0f, 0.0f, 0.0f);
        SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //入力処理
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        //デバッグ
        PlayerTime = (int)PlayerSandNomalTime;
        Debug.Log(PlayerTurn);
        Debug.Log("中砂の上が空かどうか" + PlayerEnptyFlg);
        //Debug.Log(SandMoveSp);
        Debug.Log("プレーヤーの方向" + PlayerDir);

        // Debug.Log("逆行の中砂の時間" + PlayerSandBackTime);
        // Debug.Log("通常の中砂の時間" + PlayerSandNomalTime);
        Debug.Log("中砂の時間" + PlayerTime);

        //=========================================================================================
        //移動処理
        //カメラの方向に準じて移動
        //=========================================================================================
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        PlayerDir = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = PlayerDir * PlayerSp + SandMoveSp + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (PlayerDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(PlayerDir);
        }

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

    //流砂の上にいるときに流砂の移動力を受け取る
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "QuickSand")
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            SandMoveSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();

            //上に乗っている場合はyを無視する
<<<<<<< HEAD
            //if (collision.gameObject.transform.position.y * 2.0f <= this.transform.position.y)
            //{
            //    SandMoveSp.y = 0.0f;
            //}
=======
            if (collision.gameObject.transform.position.y * 2.0f <= this.transform.position.y)
            {
                SandMoveSp.y = 0.0f;
            }
>>>>>>> 6b3656d21eab731112b0e2c838ec7b6515b20ac4
        }
    }

    //流砂から離れるときに流砂の影響を消す
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "QuickSand")
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);
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

    //プレイヤーの中砂が流れ落ちているかどうかの変数のGetter
    public Vector3 GetPlayerDir()
    {
        return PlayerDir;
    }
}
