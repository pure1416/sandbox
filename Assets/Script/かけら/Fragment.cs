using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;

//サウンド用
[RequireComponent(typeof(AudioSource))]

public class Fragment : MonoBehaviour
{

    // プレイヤーのスクリプト
    public GameObject playercontroler;

    // 変数宣言
    public Vector3 FtStartPos;                   // かけらの初期位置
    [SerializeField] Vector3 SandDir;            // 流砂の流れている方向を保存する変数
    [SerializeField] Vector3 SandRot;            // 流砂の角度を取って縦か横かを判断する
    [SerializeField] Vector3 FragmentMoveFt;     // かけらに乗って一緒に移動
    bool P_SandEnpflg;                           // プレイヤーの中砂の有無
    bool SandCol_X, SandCol_Y;                   // 横の流砂・縦の流砂に触れているかどうか
    bool WallCol;                                // 壁に触れているかどうか
    bool Ft_Col;                                 // かけらに触れてるかどうか

    //サウンド用
    float time_SE;
    float time_MAX = 2;
    bool SE_Lag;
    bool Player_Hit;

    // 当たり判定
    Rigidbody rb;

    [SerializeField] public Vector3 SandMoveFtSp;  // 流砂の移動力

    //サウンド用
    [SerializeField] AudioClip[] clips;
    protected AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        playercontroler = GameObject.FindGameObjectWithTag("Player");

        FtStartPos = this.transform.position;
        SandDir = new Vector3(0.0f, 0.0f, 0.0f);
        SandRot = new Vector3(0.0f, 0.0f, 0.0f);
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        SandCol_X = false;
        SandCol_Y = false;
        WallCol = false;
        Ft_Col = false;

        P_SandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty(); ;

        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Source = GetComponent<AudioSource>();

        Player_Hit = false;
        SE_Lag = false;
        time_SE = 0;

    }
    // Update is called once per frame
    void Update()
    {
        // プレイヤーの中砂の有無を常にもってくる
        P_SandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty();

        // 流砂が地面張られているときは重力をかける
        if (SandCol_X == true)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
        }

        // 流砂が壁に貼られているときは重力を切る
        if (SandCol_Y == true)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }

        //サウンド用
        //if (Player_Hit)
        //{
        //    if (!GetComponent<Rigidbody>().IsSleeping())
        //    {
        //        //欠片が押されている際のSE
        //        Source.PlayOneShot(clips[0]);
        //    }

        //}
        //サウンド用改
        if ((Player_Hit) && (!SE_Lag))
        {
            if (!GetComponent<Rigidbody>().IsSleeping())
            {
                SE_Lag = true;
                //欠片が押されている際のSE
                Source.PlayOneShot(clips[0]);
            }
        }
        if (SE_Lag)
        {
            time_SE += Time.deltaTime;
        }
        if (time_SE >= time_MAX)
        {
            time_SE = 0;
            SE_Lag = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
        private void OnCollisionStay(Collision collision)
    {
        // 流砂の上にいるときに流砂の移動力を受け取る
        if (collision.gameObject.tag == "QuickSand_B")
        {

            SandRot = collision.transform.localEulerAngles;
            SandMoveFtSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
            SandDir = SandMoveFtSp;

            // 流砂平面かどうか
            if (SandRot == new Vector3(0.0f, 0.0f, 0.0f))
            {
                SandCol_X = true;

                // 中砂がない時の処理
                if (P_SandEnpflg == true)
                {
                    // 位置固定を外して回転固定のみにする
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                }
                else
                // 中砂がある時の処理
                {
                    SandMoveFtSp /= 50;
                    this.transform.Translate(SandMoveFtSp);
                }
            }
            else
            {
                // 壁の流砂の処理
                SandCol_Y = true;

                // プレイヤーの中砂がないときの処理
                if (P_SandEnpflg == true)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                else
                // 中砂がある時の処理
                {
                    // 流砂が動いてるときだけ流砂の向きを保存しておく
                    SandMoveFtSp /= 50;

                    // 位置固定を外して回転固定のみにする
                    rb.constraints = RigidbodyConstraints.FreezeRotation;

                    this.transform.Translate(SandMoveFtSp);

                }

            }
        }

        if (collision.gameObject.tag == "Fragment")
        {
             Ft_Col= true;

            if ((SandCol_X == false) && (SandCol_Y == false))
            {
                FragmentMoveFt = collision.gameObject.GetComponent<Fragment>().GetSandMoveFtSp();

                bool Ft_SandCol_X = collision.gameObject.GetComponent<Fragment>().GetSandCol_X();
                bool Ft_SandCol_Y = collision.gameObject.GetComponent<Fragment>().GetSandCol_Y();
                bool Ft_WallCol = collision.gameObject.GetComponent<Fragment>().GetWallCol();

                if (Ft_SandCol_X)
                {
                    this.transform.Translate(SandMoveFtSp);

                    if (Ft_WallCol)
                    {
                         FragmentMoveFt = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }
                if(Ft_SandCol_Y)
                {
                    FragmentMoveFt = new Vector3(0.0f, 0.0f, 0.0f);
                }

            }
        }

        // 無視砂の処理
        if (collision.gameObject.tag == "Mud")
        {
            SandMoveFtSp = collision.gameObject.GetComponent<FlowingSand>().GetFlowingSandMove();
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
        }

        if(collision.gameObject.tag == "Wall")
        {
            WallCol = true;
        }

        //サウンド用
        if (collision.gameObject.tag == "Player")
        {
            Player_Hit = true;
        }
        else
        {
            Player_Hit = false;
        }
    }

    private void OnTrrigerEnter(Collider collider)
    {    
        // かけらが落下したときに初期に戻る
        if(collider.gameObject.tag == "fallcol")
        {
            this.transform.position = FtStartPos;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (collision.gameObject.tag == "QuickSand_B")
        {
            if (SandCol_X == true)
            {
                SandCol_X = false;
            }
            if (SandCol_Y == true)
            {
                SandCol_Y = false;
                this.GetComponent<Rigidbody>().useGravity = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            SandRot = new Vector3(0.0f, 0.0f, 0.0f);
        }
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (collision.gameObject.tag == "Mud")
        {
            SandCol_X = false;
            SandCol_Y = false;
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            WallCol = false;
        }
        if (collision.gameObject.tag == "Fragment")
        {
            Ft_Col = false;
            FragmentMoveFt = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }



    public Vector3 GetSandMoveFtSp()
    {
        return SandMoveFtSp;
    }
    public bool GetSandCol_X()
    {
        return SandCol_X;
    }
    public bool GetSandCol_Y()
    {
        return SandCol_Y;
    }

    public bool GetWallCol()
    {
        return WallCol;
    }
}
