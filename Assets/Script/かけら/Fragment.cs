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
    public Vector3 FtStartPos;      // かけらの初期位置
    Vector3 SandDir;                // 流砂の向きを保存しておく変数
    bool P_SandEnpflg;              // プレイヤーの中砂の有無
    bool SandCol;                   // 砂に触れているかどうか
    bool Respawnflg;                // リスポーンフラグ
    bool WallCol;                   // 壁に触れているかどうか

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
        SandDir = new Vector3(0.0f, 0.0f,0.0f);
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        SandCol = false;
        Respawnflg = false;
        WallCol = false;
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

        //if(Respawnflg)
        //{
        //    this.gameObject.SetActive(true);
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        // 流砂の上にいるときに流砂の移動力を受け取る
        if (collision.gameObject.tag == "QuickSand_B")
        {
            Debug.Log("乗ってるお");
            SandCol = true;
            SandMoveFtSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
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

            // プレイヤーの中砂がないときの処理
            if (P_SandEnpflg == true)
            {
                // 流砂がｙ方向に力が入ってなければ沈んでいく処理
                if (SandDir.y != 0.0f)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            // 中砂があるときの処理
            else
            {
                // 流砂が動いてるときだけ流砂の向きを保存しておく
                 SandDir = SandMoveFtSp;

                // 位置固定を外して回転固定のみにする
                 rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

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

    private void OnCollisionEnter(Collision collision)
    {    
        // かけらが落下したときに初期に戻る
        if(collision.gameObject.tag == "fallcol")
        {
            this.transform.position = FtStartPos;
            //Respawnflg = true;
            //this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (collision.gameObject.tag == "QuickSand_B")
        {
            SandCol = false;
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (collision.gameObject.tag == "Mud")
        {
            SandCol = false;
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            WallCol = false;
        }
    }

    public Vector3 GetSandMoveFtSp()
    {
        return SandMoveFtSp;
    }
    public bool GetSandCol()
    {
        return SandCol;
    }
    public bool GetWallCol()
    {
        return WallCol;
    }
}
