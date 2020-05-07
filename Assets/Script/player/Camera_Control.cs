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
    int CameraInputRotCnt;

    void Start()
    {
        //targetObj = GameObject.Find("playerModel");

        Correct = new Vector3(0.0f, -1.5f, 0.5f);
        targetPos = targetObj.transform.position + Correct;
        CameraVertical = new Vector3(0.0f, 0.0f, 0.0f);

        CameraMaxRot = 300;
        CameraMinRot = 80;
        KeyInputTime = 0.5f;
        KeyInputCnt = 0.0f;
        CameraRotCnt = 0.0f;
        CameraRotTime = 1.2f;
        CameraUpFlg = false;
        CameraDownFlg = false;
        CameraInputRotCnt = 0;
    }

    void Update()
    {
        //targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        //Debug.Log(CameraInputRotCnt);
        Debug.Log(this.transform.localEulerAngles.x);

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




        ////上に移動
        ////上スティックを傾けている時
        //if (Input.GetAxisRaw("Vertical2") < 0 || Input.GetKey(KeyCode.I))
        //{
        //   // if (0 == Input.GetAxisRaw("Horizontal2"))
        //  //  {
        //        if ((this.transform.localEulerAngles.x <= 60.0f && this.transform.localEulerAngles.x >= 0) || (this.transform.localEulerAngles.x >= 300 && this.transform.localEulerAngles.x <= 360))
        //        {
        //            // targetの位置のX軸を中心に、回転（公転）する
        //            transform.RotateAround(targetPos, transform.right, Time.deltaTime * 50f);
        //        }
        ////    }
        //}

        ////下に移動
        ////下スティックを傾けている時
        //if (0 < Input.GetAxisRaw("Vertical2") || Input.GetKey(KeyCode.K))
        //{
        //    //if (0 == Input.GetAxisRaw("Horizontal2"))
        //    //{
        //        if ((this.transform.localEulerAngles.x <= 60.0f && this.transform.localEulerAngles.x >= 0) || (this.transform.localEulerAngles.x >= 300 && this.transform.localEulerAngles.x <= 360))
        //        {
        //            // targetの位置のX軸を中心に、回転（公転）する
        //            transform.RotateAround(targetPos, transform.right, -Time.deltaTime * 50f);
        //        }
        //  //  }
        
        //}

        //if(this.transform.localEulerAngles.x > 60.0f && this.transform.localEulerAngles.x < 65)
        //{
        //    this.transform.localEulerAngles = new Vector3(60.0f, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        //}
        //if (this.transform.localEulerAngles.x < 300.0f && this.transform.localEulerAngles.x > 298.0f)
        //{
        //    this.transform.localEulerAngles = new Vector3(300.0f, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        //}


        //見上げ式
        //上に移動
        //上スティックを傾けている時
        if ((Input.GetAxisRaw("Vertical2") < 0 || Input.GetKey(KeyCode.I) )&& CameraInputRotCnt < 1)
        {
            if (0 == Input.GetAxisRaw("Horizontal2"))
            {
                //時間をカウントする
                KeyInputCnt += Time.deltaTime;
        
                if (KeyInputCnt >= KeyInputTime)
                {
                    CameraUpFlg = true;
                    CameraInputRotCnt++;
                }
            }
        }
        //下に移動
        //下スティックを傾けている時
        if ((0 < Input.GetAxisRaw("Vertical2") || Input.GetKey(KeyCode.K) )&& CameraInputRotCnt > -1)
        {
            if (0 == Input.GetAxisRaw("Horizontal2"))
            {
                KeyInputCnt += Time.deltaTime;
        
                if (KeyInputCnt >= KeyInputTime)
                {
                    CameraDownFlg = true;
                    CameraInputRotCnt--;
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
            KeyInputCnt = 0.0f;
        }
        if(CameraDownFlg == true)
        {
            CameraRotCnt += Time.deltaTime;
            if (CameraRotCnt <= CameraRotTime)
            {
                // targetの位置のX軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, transform.right, Time.deltaTime * -50f);
            }
            KeyInputCnt = 0.0f;
        
        }
        
        if (CameraRotCnt > CameraRotTime && (CameraUpFlg == true || CameraDownFlg == true))
        {
            CameraRotCnt = 0.0f;
            KeyInputCnt = 0.0f;
            CameraDownFlg = false;
            CameraUpFlg = false;
        }
    }
}