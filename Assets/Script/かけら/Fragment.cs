using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;


public class Fragment : MonoBehaviour
{

    // プレイヤーのスクリプト
    public GameObject playercontroler;

    // 変数宣言
    //Vector3 FragmentDir;           // かけらの方向
    //float FragmentSp;              // かけらの速度
    bool P_SandEnpflg;              // プレイヤーの中砂の有無
    bool SandCol;                  // 流砂に当たっているかどうか

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

        //FragmentDir = new Vector3(0.0f,0.0f,0.0f);
        //FragmentSp = 5.0f;

        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        SandCol = false;

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


    //private void OnCollisionStay(Collision collision)
    //{
    //    // プレイヤーが押している方向に移動
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        PlayerCol = true;
    //        // プレイヤーの移動方向を入れる
    //        FragmentDir = playercontroler.GetComponent<PlayerControler>().GetPlayerDir();
    //        // とりあえずプレイヤーと同じにしておく
    //        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
    //        if (PlayerCol == true)
    //        {
    //            rb.velocity = FragmentDir * FragmentSp + new Vector3(0, rb.velocity.y, 0) + SandMoveFtSp;

    //        }
    //        //FragmentMove = FragmentDir * FragmentSp;
    //        //this.transform.Translate(FragmentMove);
    //    }

    //    // 流砂の上にいるときに流砂の移動力を受け取る
    //    //if (collision.gameObject.tag == "QuickSand")
    //    //{
    //    //    SandCol = true;
    //    //    SandMoveFtSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
    //    //    SandMoveFtSp /= 100;

    //    //    // 流砂がｙ方向に力がかかっていたら重力を切る
    //    //    if(SandMoveFtSp.y > 0.0f)
    //    //    {
    //    //        this.GetComponent<Rigidbody>().useGravity = false;
    //    //    }
    //    //    Debug.Log(SandMoveFtSp);

    //    //    // プレイヤーの中砂が落ちきっていたら埋まる処理（徐々に埋まっていく感じに変更予定）
    //    //    if (PSandEnpflg == true)
    //    //    {
    //    //        //FtCol.center = new Vector3(0.0f,0.25f,0.0f);
    //    //        //FtCol.size = new Vector3(1.0f,0.5f,1.0f);
    //    //    }
    //    //}
    //}

    ////流砂から離れるときに流砂の影響を消す
    //private void OnCollisionExit(Collision collision)
    //{
    //    //if (collision.gameObject.tag == "QuickSand")
    //    //{
    //    //    this.GetComponent<Rigidbody>().useGravity = true;
    //    //    SandCol = false;
    //    //    SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
    //    //}
    //}
    //private void OntrrigerEnter(Collider collider)
    //{
    //    if(collider.gameObject.tag == "Untagged")
    //    {
    //        this.GetComponent<Rigidbody>().useGravity = true;
    //        SinkCount = 0;
    //        SandCol = false;
    //        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
    //        B_Col_Center = new Vector3(0.0f, 0.0f, 0.0f);
    //        B_Col_Size = new Vector3(1.0f, 1.0f, 1.0f);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "Untagged")
        //{
        //    this.GetComponent<Rigidbody>().useGravity = true;
        //    SinkCount = 0;
        //    SandCol = false;
        //    SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        //    B_Col_Center = new Vector3(0.0f, 0.0f, 0.0f);
        //    B_Col_Size = new Vector3(1.0f, 1.0f, 1.0f);
        //}

        // 流砂の上にいるときに流砂の移動力を受け取る
        if (other.gameObject.tag == "QuickSand_B")
        {
            SandMoveFtSp = other.gameObject.GetComponent<Quicksand>().GetSandMove();
            SandMoveFtSp /= 10;
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

            // 大体10秒かけて半分埋まりきるイメージ？
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

    private void OnTriggerExit(Collider other)
    {
        //流砂から離れるときに流砂の影響を消す
        if (other.gameObject.tag == "QuickSand_B")
        {
            B_Col_Center = new Vector3(0.0f, 0.0f, 0.0f);
            B_Col_Size = new Vector3(1.0f, 1.0f, 1.0f);
            SinkCount = 0;
            SandCol = false;
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);

            this.GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
