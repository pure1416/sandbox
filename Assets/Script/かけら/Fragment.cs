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
    Vector3 FragmentDir;           // かけらの方向
    float FragmentSp;              // かけらの速度
    bool PSandEnpflg;              // プレイヤーの中砂の有無
    bool SandCol;                  // 流砂に当たっているかどうか

    // 当たり判定
    BoxCollider FtCol;
    Rigidbody rb;

    [SerializeField] Vector3 SandMoveFtSp;  // 流砂の移動力

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        playercontroler = GameObject.FindGameObjectWithTag("Player");

        FragmentDir = new Vector3(0.0f,0.0f,0.0f);
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        FragmentSp = 0.0f;
        SandCol = false;

        PSandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty(); ;

        FtCol = GetComponent<BoxCollider>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの中砂の有無を常にもってくる
        PSandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty();

        // 流砂に乗っていたら動く
        if (SandCol == true)
        {
            this.transform.Translate(SandMoveFtSp);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // プレイヤーが押している方向に移動
        if (collision.gameObject.tag == "Player")
        {
            // プレイヤーの移動方向を入れる
            FragmentDir = playercontroler.GetComponent<PlayerControler>().GetPlayerDir();
            // とりあえずプレイヤーと同じにしておく
            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
             rb.velocity = FragmentDir * FragmentSp + new Vector3(0, rb.velocity.y, 0) + SandMoveFtSp;
        }

        // 流砂の上にいるときに流砂の移動力を受け取る
        if (collision.gameObject.tag == "QuickSand")
        {
            SandCol = true;
            SandMoveFtSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
            SandMoveFtSp /= 10;
            
            // 流砂がｙ方向に力がかかっていたら重力を切る
            if(SandMoveFtSp.y > 0.0f)
            {
                this.GetComponent<Rigidbody>().useGravity = false;
            }
            //Debug.Log(SandMoveFtSp);

            // プレイヤーの中砂が落ちきっていたら埋まる処理（徐々に埋まっていく感じに変更予定）
            if (PSandEnpflg == true)
            {
                FtCol.center = new Vector3(0.0f,0.25f,0.0f);
                FtCol.size = new Vector3(1.0f,0.5f,1.0f);
            }
        }
    }

    //流砂から離れるときに流砂の影響を消す
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "QuickSand")
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            SandCol = false;
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
