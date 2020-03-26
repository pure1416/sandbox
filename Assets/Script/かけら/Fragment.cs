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
    bool PSandEnpflg;                  // プレイヤーの中砂の有無

    // 当たり判定
    Collider FtCol;
    Rigidbody rb;

    [SerializeField] Vector3 SandMoveFtSp;  // 流砂の移動力

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        playercontroler = GameObject.FindGameObjectWithTag("Player");

        FragmentDir = new Vector3(0.0f,0.0f,0.0f);
        FragmentSp = 0.0f;
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);

        PSandEnpflg = playercontroler.GetComponent<PlayerControler>().GetPlayerEnpty(); ;


        FtCol = GetComponent<Collider>();
        FtCol.isTrigger = false;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

         this.transform.Translate( SandMoveFtSp);
    }

    // プレイヤーが押す方向に移動
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // プレイヤーの移動方向を入れる
            FragmentDir = playercontroler.GetComponent<PlayerControler>().GetPlayerDir();
            //FragmentDir = FragmentDir * FragmentSp;
            //this.transform.Translate(FragmentDir);
            // とりあえずプレイヤーと同じにしておく
            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
             rb.velocity = FragmentDir * FragmentSp + new Vector3(0, rb.velocity.y, 0) + SandMoveFtSp;

        }


        //流砂の上にいるときに流砂の移動力を受け取る
        if (collision.gameObject.tag == "QuickSand")
        {
            //if (PSandEnpflg == true)
            //{
            //   // FtCol.isTrigger = true;
            //   // rb.useGravity = false;
            //}
            //this.GetComponent<Rigidbody>().useGravity = false;
            // CollisionSand = true;

            SandMoveFtSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
            SandMoveFtSp /= 10;
            if(SandMoveFtSp.y > 0.0f)
            {
                this.GetComponent<Rigidbody>().useGravity = false;
            }
            Debug.Log(SandMoveFtSp); 
        }
    
}

    //流砂から離れるときに流砂の影響を消す
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "QuickSand")
        {
            this.GetComponent<Rigidbody>().useGravity = true;
           // CollisionSand = false;
            SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
