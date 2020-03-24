using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weight;


public class Fragment : MonoBehaviour
{

    // プレイヤーのスクリプト
    PlayerControler playercontroler = new PlayerControler();
    // ↓使い方よくわからんからコメント 【これに変えれば黄色の警告が消える】
    //PlayerControler playercontroler =GameObject.Find("オブジェクト名").GetComponent<PlayerControler>();

    // 変数宣言
    Vector3 FragmentDir;                  // かけらの方向
    public float FragmentSp;              // かけらの速度
    [SerializeField] Vector3 SandMoveFtSp;  // 流砂の移動力

    Rigidbody rb;                         // 当たり判定

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        FragmentDir = new Vector3(0.0f,0.0f,0.0f);
        FragmentSp = 5.0f;
        SandMoveFtSp = new Vector3(0.0f, 0.0f, 0.0f);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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
    }


}
