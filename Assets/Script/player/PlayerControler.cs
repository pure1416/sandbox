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
    public Vector3 PlayerDir;   //プレイヤーの方向
    public bool ClearFlg;   //クリアフラグ
    Vector3 StartPlayerPos; //プレイヤーの初期位置
    private Rigidbody _rigidbody; //物理判定の速度変数
    bool GameOverFlg;           //ゲームオーバーフラグ、高いところから落ちたときやステージ外へ行ったときtrueとなる
    bool PlayerFloatFlg;        //プレイヤーが浮いているか
    float PlayerOldVelocity;    //プレイヤーの1フレーム前の加速度
    float PlayerGravity;        //プレイヤーの重力

    [SerializeField] bool CollisionSand;         //流砂に触れているかどうか

    [SerializeField] Vector3 SandMoveSp;  //流砂の移動力
    [SerializeField] float FallDeathPos;  //どれだけ高いところから落ちたときか

    [Header("時間")]
    [SerializeField] public float PlayerSandNomalTime;   //通常に流れるほうの砂の時間
    [SerializeField] public float PlayerSandBackTime;    //逆行して流れる砂の時間
    [SerializeField] private bool PlayerEnptyFlg;         //中砂が落ちきっているか判定
    [SerializeField] private Vector3 PlayerGameoverPos;         //ゲームオーバーの位置



    //入力変数
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;                //当たり判定

    //[Header("時間")]
    //[SerializeField]float PlayerSandNomalTime;  //通常に流れる中砂
    //[SerializeField]float PlayerSandBackTime;  //逆に流れる中砂

    // Start is called before the first frame update
    void Start()
    {
        //変数初期化
        PlayerEnptyFlg = false;
        PlayerSandBackTime = 0.0f;
        PlayerSandNomalTime = PlayerTotalTime;
        PlayerDir = new Vector3(0.0f, 0.0f, 0.0f);
        SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);
        CollisionSand = false;
        ClearFlg = false;
        _rigidbody = this.GetComponent<Rigidbody>();
        GameOverFlg = false;
        PlayerFloatFlg = false;
        PlayerOldVelocity = 0.0f;
        PlayerGravity = 0.098f;
        //初期位置設定
        StartPlayerPos = GameObject.Find("StartPlace").transform.position;
        this.transform.position = StartPlayerPos;
        this.transform.position += new Vector3(0, 1.0f, 0);
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        //入力処理
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        //デバッグ
        //Debug.Log("速度ベクトル: " + _rigidbody.velocity);
        Debug.Log(rb.velocity);
        
        //ポーズ画面処理
        if (Mathf.Approximately(Time.timeScale, 0f)) //時間が止まっていたら、Update処理をしない処理
        {
            return;
        }

        //クリアしたら移動しないようにする
        if (ClearFlg == true) 
        {
            return;
        }

        //==================================================
        //ゲームオーバー処理
        //==================================================
        //trueになったら
        if (GameOverFlg == true)
        {
            //this.transform.position = StartPlayerPos;
            //this.GetComponent<Rigidbody>().useGravity = false;
            //this.transform.position.y = PlayerGameoverPos.y;

            //this.transform.position = new Vector3(this.transform.position.x, PlayerGameoverPos.y, this.transform.position.z);

            return;
        }

        //===================================================
        //入力処理
        //===================================================
        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("上");
            //PlayerAnimation.SetBool("Run", true);

        }
        else
        {
          //  PlayerAnimation.SetBool("Run", false);

        }

        if (Input.GetButtonDown("Vertical"))
        {
          //  PlayerAnimation.SetBool("Run", true);

            Debug.Log("下");
        }
        else
        {
         //   PlayerAnimation.SetBool("Run", false);

        }
        //if (Input.GetButtonDown("Controler_Right"))
        //{
        //    Debug.Log("右");
        //}
        //if (Input.GetButtonDown("Controler_Left"))
        //{
        //    Debug.Log("左");
        //}


        //=========================================================================================
        //移動処理
        //カメラの方向に準じて移動
        //=========================================================================================
        //カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        PlayerDir = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        //流砂に触れている時
        if (CollisionSand == true)
        {
            //y軸に力がかかっていないとき
            if (SandMoveSp.y == 0.0f)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
                rb.velocity = PlayerDir * PlayerSp + new Vector3(0, rb.velocity.y, 0) + SandMoveSp;
                //this.gameObject.transform.position += PlayerDir * PlayerSp * 0.007f + SandMoveSp * 0.007f;
            }
            //y軸に力がかかっている時
            else
            {
                this.GetComponent<Rigidbody>().useGravity = false;
                //rb.velocity = PlayerDir * PlayerSp + new Vector3(0, rb.velocity.y, 0) + SandMoveSp;
                rb.velocity = new Vector3(PlayerDir.x * PlayerSp, PlayerDir.y * PlayerSp + SandMoveSp.y, PlayerDir.z * PlayerSp);
                //this.gameObject.transform.position += PlayerDir * PlayerSp * 0.007f + SandMoveSp * 0.007f;


            }
            rb.velocity = PlayerDir * PlayerSp + new Vector3(0, rb.velocity.y, 0) + SandMoveSp;
            // this.gameObject.transform.position = PlayerDir * PlayerSp + new Vector3(0, this.gameObject.transform.position.y, 0) + SandMoveSp;

        }
        //流砂に触れていない時
        else  //CollisionSand == false
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            //this.gameObject.transform.position += PlayerDir * PlayerSp * 0.007f;
            rb.velocity = PlayerDir * PlayerSp + new Vector3(0, rb.velocity.y, 0);
        }

        // キャラクターの向きを進行方向に
        if (PlayerDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(PlayerDir);
        }

        //画面外に落ちたとき
        if (this.transform.position.y <= PlayerGameoverPos.y)
        {
            GameOverFlg = true;
        }

        //=========================================================================================
        //回転処理
        //=========================================================================================
        //スペースキーまたはAボタンを押したとき
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
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

    //１F前の加速度を取得
    void LateUpdate()
    {
        PlayerOldVelocity = _rigidbody.velocity.y;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Clear")
        {
            ClearFlg = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Clear")
        {
            ClearFlg = false;
        }
    }

    //流砂の処理(板ver)とか
    private void OnTriggerStay(Collider other)
    {
        //流砂
        if (other.gameObject.tag == "QuickSand_B")
        {
            CollisionSand = true;

            Vector3 tmp = other.gameObject.GetComponent<Quicksand>().GetSandMove();
            //yが大きい時に優先する
            if (SandMoveSp.y < tmp.y)
            {
                SandMoveSp.y = tmp.y;
            }
            //空っぽの時
            else if (PlayerEnptyFlg)
            {
                SandMoveSp = tmp;
            }
            SandMoveSp = new Vector3(tmp.x, SandMoveSp.y, tmp.z);
        }
        //ずっと流れる流砂
        if (other.gameObject.tag == "Mud")
        {
            CollisionSand = true;

            Vector3 tmp = other.gameObject.GetComponent<FlowingSand>().GetFlowingSandMove();
            //yが大きい時に優先する
            if (SandMoveSp.y < tmp.y)
            {
                SandMoveSp.y = tmp.y;
            }
            SandMoveSp = new Vector3(tmp.x, SandMoveSp.y, tmp.z);
        }

    }

    //流砂から離れるときに流砂の影響を消す　　とか
    private void OnTriggerExit(Collider other)
    {
        //流砂
        if (other.gameObject.tag == "QuickSand_B")
        {
            //Debug.Log("流はなれ");
            CollisionSand = false;
            SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);
        }
        //ずっと流れる流砂
        if (other.gameObject.tag == "Mud")
        {
            CollisionSand = false;
            SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);

        }
    }

    //なにかと当たった時
    private void OnCollisionEnter(Collision collision)
    {
        //高いところから落ちたとき
        if (PlayerOldVelocity <= FallDeathPos)
        {
            GameOverFlg = true;
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

    //プレイヤーのカメラに対する向き
    public Vector3 GetPlayerDir()
    {
        return PlayerDir;
    }

    //プレイヤーのワールドでの向き
    public Vector3 GetPlayerRot()
    {
        return transform.localEulerAngles;
    }

    //プレイヤーのゲームオーバー判定
    public bool GetGameOverFlg()
    {
        return GameOverFlg;
    }


    //プレイヤーのゲームオーバー判定
    public bool GetGameClearFlg()
    {
        return ClearFlg;
    }

    //プレイヤーの時間(α版用)
    public float GetPlayerNormalTime()
    {
        return PlayerSandNomalTime;
    }
    //プレイヤーの時間(α版用)
    public float GetPlayerBackTime()
    {
        return PlayerSandBackTime;
    }

}
