using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSMove : MonoBehaviour
{
    //定数宣言
    private const int STAY = 0;
    private const int NEXT = 1;
    private const int PREV = -1;

    [Header("移動時間"), SerializeField, Range(0, 10)]
    public float MoveTime;   //移動時間

    [SerializeField]
    public Vector3 MoveDist;        //移動距離

    private float NowTime;          //経過時間
    private Vector3 StartPos;       //開始位置
    private Vector3 EndPos;         //終了位置

    private bool MoveEnd;           //移動終了フラグ
    private int MoveNP;             //+1：Next　-1：Prev

    public GameObject SSMObj;       //ステージセレクトマネージャーのオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        NowTime = 0.0f;
        MoveEnd = true;
        SSMObj = GameObject.Find("SSManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
