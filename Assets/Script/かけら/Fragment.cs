using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;


public class Fragment : MonoBehaviour
{

    // プレイヤーのスクリプト
    //PlayerControler playercontroler = new PlayerControler();
    // ↓使い方よくわからんからコメント 【これに変えれば黄色の警告が消える】
    // PlayerControler playercontroler = GameObject.Find("playerModel").GetComponent<PlayerControler>();

    [SerializeField]PlayerControler playercontroler;

    // 変数宣言
    Vector3 FragmentDir;                  // かけらの方向
    public float FragmentSp;              // かけらの速度
    [SerializeField] Vector3 SandMoveFtSp;  // 流砂の移動力

    // 当たり判定
    Collider FtCol;
    Rigidbody rb;                        

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        FragmentDir = new Vector3(0.0f,0.0f,0.0f);
        FragmentSp = 5.0f;
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);

        FtCol = GetComponent<Collider>();
        FtCol.isTrigger = false;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = FragmentDir * FragmentSp + new Vector3(0, rb.velocity.y, 0) + SandMoveFtSp;

    }

    // プレイヤーが押す方向に移動
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // プレイヤーの移動方向を入れる
            FragmentDir = playercontroler.PlayerDir;

            // とりあえずプレイヤーと同じにしておく
            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rb.velocity = FragmentDir * FragmentSp + new Vector3(0, rb.velocity.y, 0) + SandMoveFtSp;

        }

        //if (playercontroler.GetPlayerEnpty() == true)
        //{

        //    //流砂の上にいるときに流砂の移動力を受け取る
        //    if (collision.gameObject.tag == "QuickSand")
        //    {
        //        FtCol.isTrigger = true;

        //        //this.GetComponent<Rigidbody>().useGravity = false;
        //        // CollisionSand = true;
        //        // SandMoveFtSp = collision.gameObject.GetComponent<Quicksand>().GetSandMove();
        //    }
        //}
    }
 
    //流砂から離れるときに流砂の影響を消す
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "QuickSand")
    //    {
    //        //this.GetComponent<Rigidbody>().useGravity = true;
    //        //CollisionSand = false;
    //        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);
    //    }
    //}
}
