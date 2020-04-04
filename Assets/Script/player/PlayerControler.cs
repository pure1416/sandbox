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
    public bool ClearFlg;   //プレイヤーの方向
    Vector3 StartPlayerPos; //プレイヤーの初期位置
    private Rigidbody _rigidbody; //物理判定の速度変数
    bool GameOverFlg;           //ゲームオーバーフラグ、高いところから落ちたときやステージ外へ行ったときtrueとなる
    　
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
        Debug.Log(ClearFlg);
        Debug.Log("速度ベクトル: " + _rigidbody.velocity);
        //Debug.Log(SandMoveSp);
        //Debug.Log("プレーヤーの方向" + PlayerDir);

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
            }
            //y軸に力がかかっている時
            else
            {
                this.GetComponent<Rigidbody>().useGravity = false;
                rb.velocity = new Vector3(0.0f, PlayerDir.y * PlayerSp + SandMoveSp.y, 0.0f);
            }

            rb.velocity = PlayerDir * PlayerSp + new Vector3(0, rb.velocity.y, 0) + SandMoveSp;
        }
        //流砂に触れていない時
        else  //CollisionSand == false
        {
            //高いところから落ちたとき
            if (_rigidbody.velocity.y <= FallDeathPos)
            {
                GameOverFlg = true;
            }
            else
            {
                GameOverFlg = false;
            }
            this.GetComponent<Rigidbody>().useGravity = true;
            rb.velocity = PlayerDir * PlayerSp + new Vector3(0, rb.velocity.y, 0);


        }

        // キャラクターの向きを進行方向に
        if (PlayerDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(PlayerDir);
        }

        //ゲームオーバーの位置
        if (this.transform.position.y <= PlayerGameoverPos.y ||
            GameOverFlg == true)
        {
            this.transform.position = StartPlayerPos;
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

    private void OnCollisionStay(Collision collision)
    {
        ////流砂の上にいるときに流砂の移動力を受け取る
        //if (collision.gameObject.tag == "QuickSand")
        //{
        //    CollisionSand = true;
        //    SandMoveSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
        //}

        if (collision.gameObject.tag == "Clear")
        {
            ClearFlg = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ////流砂から離れるときに流砂の影響を消す
        //if (collision.gameObject.tag == "QuickSand")
        //{
        //    CollisionSand = false;
        //    SandMoveSp = new Vector3(0.0f, 0.0f, 0.0f);
        //}

        if (collision.gameObject.tag == "Clear")
        {
            ClearFlg = false;
        }
    }

    //流砂の処理(板ver)
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "QuickSand_B")
        {
            CollisionSand = true;
            SandMoveSp = other.gameObject.GetComponent<Quicksand>().GetSandMove();
        }
    }

    //流砂から離れるときに流砂の影響を消す
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "QuickSand_B")
        {
            CollisionSand = false;
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
