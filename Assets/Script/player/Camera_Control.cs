using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public Transform Target;
    public float DistanceToPlayerM = 8f;    // カメラとプレイヤーとの距離[m]
    public float SlideDistanceM = 0f;       // カメラを横にスライドさせる；プラスの時右へ，マイナスの時左へ[m]
    public float HeightM = 1.2f;            // 注視点の高さ[m]
    public float RotationSensitivity = 100f;// 感度
    float rotX;
    float rotY;
    void Start()
    {
        if (Target == null)
        {
            Debug.LogError("ターゲットが設定されていない");
            Application.Quit();
        }
        // カーソル非表示
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        //if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        //{
        //    rotX = Input.GetAxisRaw("Horizontal2") * 0.8f * Time.deltaTime * RotationSensitivity;
        //    rotY = Input.GetAxisRaw("Vertical2") * 0.8f * Time.deltaTime * RotationSensitivity;
        //}
        ////マウスの場合
        //if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        //{
        //    rotX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity;
        //    rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSensitivity;
        //}

        rotX =( Input.GetAxisRaw("Horizontal2") * 0.8f * Time.deltaTime * RotationSensitivity) +( Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity * 4);
        rotY =( Input.GetAxisRaw("Vertical2") * 0.8f * Time.deltaTime * RotationSensitivity )+ (Input.GetAxis("Mouse Y") * Time.deltaTime * -RotationSensitivity * 4);
        var lookAt = Target.position + Vector3.up * HeightM;

        // 回転
        transform.RotateAround(lookAt, Vector3.up, rotX);
        // カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
        if (transform.forward.y > 0.9f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y < -0.9f && rotY > 0)
        {
            rotY = 0;
        }
        transform.RotateAround(lookAt, transform.right, rotY);

        // カメラとプレイヤーとの間の距離を調整
        transform.position = lookAt - transform.forward * DistanceToPlayerM;

        // 注視点の設定
        transform.LookAt(lookAt);

        // カメラを横にずらして中央を開ける
        transform.position = transform.position + transform.right * SlideDistanceM;
    }


    //public GameObject targetObj;
    //Vector3 targetPos;          //ターゲットのオブジェクトの位置
    //Vector3 Correct;            //プレイヤーの離れている距離
    //Vector3 CameraVertical;
    //float CameraMaxRot;         //カメラの最大角度
    //float CameraMinRot;         //カメラの最大角度

    ////見上げ式の変数
    //float KeyInputTime; //キーを入力している時間
    //float KeyInputCnt;  //キーを入力している時間のカウント
    //float CameraRotCnt; //カメラの回転する時間
    //float CameraRotTime;//カメラの回転する時間カウント
    //bool CameraUpFlg;
    //bool CameraDownFlg;
    //int CameraInputRotCnt;
    //void Start()
    //{
    //    //targetObj = GameObject.Find("playerModel");

    //    Correct = new Vector3(0.0f, -1.5f, 0.5f);
    //    targetPos = targetObj.transform.position + Correct;
    //    CameraVertical = new Vector3(0.0f, 0.0f, 0.0f);

    //    CameraMaxRot = 300;
    //    CameraMinRot = 80;
    //    KeyInputTime = 0.5f;
    //    KeyInputCnt = 0.0f;
    //    CameraRotCnt = 0.0f;
    //    CameraRotTime = 1.2f;
    //    CameraUpFlg = false;
    //    CameraDownFlg = false;
    //    CameraInputRotCnt = 0;


    //}

    //void Update()
    //{
    //    //targetの移動量分、自分（カメラ）も移動する
    //    transform.position += targetObj.transform.position - targetPos;
    //    targetPos = targetObj.transform.position;

    //    //Debug.Log(CameraInputRotCnt);
    //    Debug.Log(this.transform.localEulerAngles.x);

    //    ////右スティック（追加）
    //    //if (Input.GetAxisRaw("Vertical2") < 0)
    //    //{
    //    //    Debug.Log("上に傾いている");
    //    //}
    //    //else if (0 < Input.GetAxisRaw("Vertical2"))
    //    //{
    //    //    Debug.Log("下に傾いている");
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("上下に傾いていない");
    //    //}
    //    //if (Input.GetAxisRaw("Horizontal2") < 0)
    //    //{
    //    //    Debug.Log("左に傾いている");
    //    //}
    //    //else if (0 < Input.GetAxisRaw("Horizontal2"))
    //    //{
    //    //    Debug.Log("右に傾いている");
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("左右方向には傾いていない");
    //    //}


    //    //反時計回りに回転
    //    if (Input.GetKey(KeyCode.Q) || Input.GetKey("joystick button 4") || Input.GetAxisRaw("Horizontal2") < 0 || Input.GetKey(KeyCode.J))
    //    {

    //            // targetの位置のY軸を中心に、回転（公転）する
    //            transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * -75f);

    //    }

    //    //時計回りに回転
    //    if (Input.GetKey(KeyCode.E) || Input.GetKey("joystick button 5") || 0 < Input.GetAxisRaw("Horizontal2") || Input.GetKey(KeyCode.L))
    //    {
    //            // targetの位置のY軸を中心に、回転（公転）する
    //            transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 75f);
    //    }

    //    //===========================================================================
    //    //自由移動
    //    //===========================================================================
    //    //上に移動
    //    //上スティックを傾けている時
    //    if (Input.GetAxisRaw("Vertical2") < 0 || Input.GetKey(KeyCode.I))
    //    {
    //       // if (0 == Input.GetAxisRaw("Horizontal2"))
    //      //  {
    //            if ((this.transform.eulerAngles.x <= 60.0f && this.transform.eulerAngles.x >= 0) || (this.transform.eulerAngles.x >= 300 && this.transform.eulerAngles.x <= 360))
    //            {
    //                // targetの位置のX軸を中心に、回転（公転）する
    //                transform.RotateAround(targetPos, transform.right, Time.deltaTime * 75f);
    //            }
    //    //    }
    //    }

    //    //下に移動
    //    //下スティックを傾けている時
    //    if (0 < Input.GetAxisRaw("Vertical2") || Input.GetKey(KeyCode.K))
    //    {
    //        //if (0 == Input.GetAxisRaw("Horizontal2"))
    //        //{
    //            if ((this.transform.eulerAngles.x <= 60.0f && this.transform.eulerAngles.x >= 0) || (this.transform.eulerAngles.x >= 300 && this.transform.eulerAngles.x <= 360))
    //            {
    //                // targetの位置のX軸を中心に、回転（公転）する
    //                transform.RotateAround(targetPos, transform.right, -Time.deltaTime * 75f);
    //            }
    //      //  }

    //    }

    //    //float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
    //    //transform.eulerAngles = new Vector3(
    //    //    Mathf.Clamp(angle_x, -60, 60),
    //    //    transform.eulerAngles.y,
    //    //    transform.eulerAngles.z
    //    //);


    //    //this.transform.localEulerAngles = new Vector3(Mathf.Clamp(this.transform.transform.localEulerAngles.x, -60, 60),
    //    //                                        this.transform.localEulerAngles.y,
    //    //                                         this.transform.localEulerAngles.z);

    //    if(this.transform.localEulerAngles.x > 60.0f && this.transform.localEulerAngles.x < 180)
    //    {
    //        this.transform.localEulerAngles = new Vector3(60.00000f, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
    //    }
    //    if (this.transform.localEulerAngles.x < 300.0f && this.transform.localEulerAngles.x > 180)
    //    {
    //        this.transform.localEulerAngles = new Vector3(300.00000f, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
    //    }


    //    //===========================================================================
    //    //見上げ式
    //    //===========================================================================
    //    ////上に移動
    //    ////上スティックを傾けている時
    //    //if ((Input.GetAxisRaw("Vertical2") < 0 || Input.GetKey(KeyCode.I) )&& CameraInputRotCnt < 1)
    //    //{
    //    //    if (0 == Input.GetAxisRaw("Horizontal2"))
    //    //    {
    //    //        //時間をカウントする
    //    //        KeyInputCnt += Time.deltaTime;

    //    //        if (KeyInputCnt >= KeyInputTime)
    //    //        {
    //    //            CameraUpFlg = true;
    //    //            CameraInputRotCnt++;
    //    //        }
    //    //    }
    //    //}
    //    ////下に移動
    //    ////下スティックを傾けている時
    //    //if ((0 < Input.GetAxisRaw("Vertical2") || Input.GetKey(KeyCode.K) )&& CameraInputRotCnt > -1)
    //    //{
    //    //    if (0 == Input.GetAxisRaw("Horizontal2"))
    //    //    {
    //    //        KeyInputCnt += Time.deltaTime;

    //    //        if (KeyInputCnt >= KeyInputTime)
    //    //        {
    //    //            CameraDownFlg = true;
    //    //            CameraInputRotCnt--;
    //    //        }
    //    //    }
    //    //}

    //    //if(CameraUpFlg == true)
    //    //{
    //    //    CameraRotCnt += Time.deltaTime;
    //    //    if (CameraRotCnt <= CameraRotTime)
    //    //    {
    //    //        // targetの位置のX軸を中心に、回転（公転）する
    //    //        transform.RotateAround(targetPos, transform.right, Time.deltaTime * 50f);
    //    //    }
    //    //    KeyInputCnt = 0.0f;
    //    //}
    //    //if(CameraDownFlg == true)
    //    //{
    //    //    CameraRotCnt += Time.deltaTime;
    //    //    if (CameraRotCnt <= CameraRotTime)
    //    //    {
    //    //        // targetの位置のX軸を中心に、回転（公転）する
    //    //        transform.RotateAround(targetPos, transform.right, Time.deltaTime * -50f);
    //    //    }
    //    //    KeyInputCnt = 0.0f;

    //    //}

    //    //if (CameraRotCnt > CameraRotTime && (CameraUpFlg == true || CameraDownFlg == true))
    //    //{
    //    //    CameraRotCnt = 0.0f;
    //    //    KeyInputCnt = 0.0f;
    //    //    CameraDownFlg = false;
    //    //    CameraUpFlg = false;
    //    //}
    //}
}