using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public GameObject targetObj;
    Vector3 targetPos;          //ターゲットのオブジェクトの位置
    Vector3 Correct;            //プレイヤーの離れている距離
    Vector3 CameraVertical;
    float CameraMaxRot;         //カメラの最大角度
    float CameraMinRot;         //カメラの最大角度

    //見上げ式の変数
    float KeyInputTime; //キーを入力している時間
    float KeyInputCnt;  //キーを入力している時間のカウント
    float CameraRotCnt; //カメラの回転する時間
    float CameraRotTime;//カメラの回転する時間カウント
    bool CameraUpFlg;
    bool CameraDownFlg;


    void Start()
    {
        //targetObj = GameObject.Find("playerModel");

        Correct = new Vector3(0.0f, -1.5f, 0.5f);
        targetPos = targetObj.transform.position + Correct;
        CameraVertical = new Vector3(0.0f, 0.0f, 0.0f);

        CameraMaxRot = 300;
        CameraMinRot = 80;
        KeyInputTime = 1.0f;
        KeyInputCnt = 0.0f;
        CameraRotCnt = 0.0f;
        CameraRotTime = 1.0f;
        CameraUpFlg = false;
        CameraDownFlg = false;
    }

    void Update()
    {
        //targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        //Debug.Log(KeyInputCnt);
        Debug.Log(transform.localEulerAngles);

        ////右スティック（追加）
        //if (Input.GetAxisRaw("Vertical2") < 0)
        //{
        //    Debug.Log("上に傾いている");
        //}
        //else if (0 < Input.GetAxisRaw("Vertical2"))
        //{
        //    Debug.Log("下に傾いている");
        //}
        //else
        //{
        //    Debug.Log("上下に傾いていない");
        //}
        //if (Input.GetAxisRaw("Horizontal2") < 0)
        //{
        //    Debug.Log("左に傾いている");
        //}
        //else if (0 < Input.GetAxisRaw("Horizontal2"))
        //{
        //    Debug.Log("右に傾いている");
        //}
        //else
        //{
        //    Debug.Log("左右方向には傾いていない");
        //}


        //反時計回りに回転
        if (Input.GetKey(KeyCode.Q) || Input.GetKey("joystick button 4") || Input.GetAxisRaw("Horizontal2") < 0 || Input.GetKey(KeyCode.J))
        {
            if (0 == Input.GetAxisRaw("Vertical2"))
            {
                // targetの位置のY軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * -50f);
            }
        }

        //時計回りに回転
        if (Input.GetKey(KeyCode.E) || Input.GetKey("joystick button 5") || 0 < Input.GetAxisRaw("Horizontal2") || Input.GetKey(KeyCode.L))
        {
            if (0 == Input.GetAxisRaw("Vertical2"))
            {
                // targetの位置のY軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 50f);
            }
        }

        //上に移動
        //上スティックを傾けている時
        //if (Input.GetAxisRaw("Vertical2") < 0 || Input.GetKey(KeyCode.I))
        //{
        //    if (0 == Input.GetAxisRaw("Horizontal2"))
        //    {
        //        // targetの位置のX軸を中心に、回転（公転）する
        //        transform.RotateAround(targetPos, transform.right, Time.deltaTime * 50f);
        //    }
        //}

        ////下に移動
        ////下スティックを傾けている時
        //if (0 < Input.GetAxisRaw("Vertical2") || Input.GetKey(KeyCode.K))
        //{
        //    if (0 == Input.GetAxisRaw("Horizontal2"))
        //    {
        //        // targetの位置のX軸を中心に、回転（公転）する
        //        transform.RotateAround(targetPos, transform.right, Time.deltaTime * -50f);
        //    }
        //}
        //}

        //見上げ式
        //上に移動
        //上スティックを傾けている時
        if (Input.GetAxisRaw("Vertical2") < 0 || Input.GetKey(KeyCode.I))
        {
            if (0 == Input.GetAxisRaw("Horizontal2"))
            {
                //時間をカウントする
                KeyInputCnt += Time.deltaTime;

                if (KeyInputCnt >= KeyInputTime)
                {
                        CameraUpFlg = true;      
                }
            }
        }
        //下に移動
        //下スティックを傾けている時
        if (0 < Input.GetAxisRaw("Vertical2") || Input.GetKey(KeyCode.K))
        {
            if (0 == Input.GetAxisRaw("Horizontal2"))
            {
                KeyInputCnt += Time.deltaTime;

                if (KeyInputCnt >= KeyInputTime)
                {
                    CameraDownFlg = true;                
                }
            }
        }
        
        if(CameraUpFlg == true)
        {
            CameraRotCnt += Time.deltaTime;
            if (CameraRotCnt <= CameraRotTime)
            {
                // targetの位置のX軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, transform.right, Time.deltaTime * 50f);
            }
            else
            {
                CameraUpFlg = false;
            }
        }
        if(CameraDownFlg == true)
        {
            CameraRotCnt += Time.deltaTime;
            if (CameraRotCnt <= CameraRotTime)
            {
                // targetの位置のX軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, transform.right, Time.deltaTime * -50f);
            }
            else
            {
                CameraDownFlg = false;
            }
        }


        if (0 == Input.GetAxisRaw("Vertical2"))
        {
            //カウント初期化
            KeyInputCnt = 0.0f;
            CameraRotCnt = 0.0f;
            CameraDownFlg = false;
            CameraUpFlg = false;
        }
    }
}