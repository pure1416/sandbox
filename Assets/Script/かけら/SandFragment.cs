using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;


public class SandFragment : MonoBehaviour
{

    // プレイヤーのスクリプト
    public GameObject playercontroler;

    // 変数宣言
    public Vector3 FtStartPos;      // かけらの初期位置
    Vector3 SandDir;                // 流砂の向きを保存しておく変数
    bool P_SandEnpflg;              // プレイヤーの中砂の有無
    bool P_ColFrag;                 // プレイヤーが当たっているかどうか

    // 当たり判定
    Rigidbody rb;

    [SerializeField] Vector3 SandMoveFtSp;  // 流砂の移動力

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        playercontroler = GameObject.FindGameObjectWithTag("Player");

        FtStartPos = this.transform.position;
        SandDir = new Vector3(0.0f, 0.0f, 0.0f);
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);

        P_SandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty();
        P_ColFrag = false;

        rb = this.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionX |
                    RigidbodyConstraints.FreezePositionZ |
                    RigidbodyConstraints.FreezeRotation;

    }
    // Update is called once per frame
    void Update()
    {
        // プレイヤーの中砂の有無を常にもってくる
        P_SandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty();
        Debug.Log("砂の速度"+SandMoveFtSp);
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

            // プレイヤーの中砂がないときの処理
            if (P_SandEnpflg == true)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;

                //if (SandDir.y != 0.0f)
                //// 流砂がｙ方向に力が入っていたら位置を固定する
                //{
                //    rb.constraints = RigidbodyConstraints.FreezeAll;
                //}
            }
            // 中砂があるときの処理
            else
            {
                // 流砂が動いてるときだけ流砂の向きを保存しておく
                SandDir = SandMoveFtSp;

                // 位置固定を外して回転固定のみにする
               // rb.constraints = RigidbodyConstraints.FreezeRotation;

                 if (SandDir.x != 0.0f)
                {
                    rb.constraints =
                    RigidbodyConstraints.FreezePositionY |
                    RigidbodyConstraints.FreezePositionZ |
                    RigidbodyConstraints.FreezeRotation;
                }

                if (SandDir.y != 0.0f)
                {
                    rb.constraints =
                    RigidbodyConstraints.FreezePositionX |
                    RigidbodyConstraints.FreezePositionZ |
                    RigidbodyConstraints.FreezeRotation;
                }
                if (SandDir.z != 0.0f)
                {
                    rb.constraints =
                    RigidbodyConstraints.FreezePositionY|
                    RigidbodyConstraints.FreezePositionX|
                    RigidbodyConstraints.FreezeRotation ;
                }


            }
        }

        // 無視砂の処理
        if (other.gameObject.tag == "Mud")
        {
            SandMoveFtSp = other.gameObject.GetComponent<FlowingSand>().GetFlowingSandMove();
            SandMoveFtSp /= 50;
            SandDir = SandMoveFtSp;
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

            if (SandDir.x != 0.0f)
            {
                rb.constraints =
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotation;
            }

            if (SandDir.y != 0.0f)
            {
                rb.constraints =
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotation;
            }
            if (SandDir.z != 0.0f)
            {
                rb.constraints =
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // かけらが落下したときに初期に戻る
        if (other.gameObject.tag == "fallcol")
        {
            this.transform.position = FtStartPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (other.gameObject.tag == "QuickSand_B")
        {
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;

        }
        //流砂から流砂へ移動するときに一旦SandMobeFtSpを初期化する
        if (other.gameObject.tag == "Mud")
        {
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
            this.GetComponent<Rigidbody>().useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;

        }
    }
}
