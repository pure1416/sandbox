using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public GameObject targetObj;
    Vector3 targetPos;
    Vector3 Correct;
    Vector3 PlayerDir;
    Vector3 CameraVertical;
    float CameraRot;

    void Start()
    {
        //targetObj = GameObject.Find("playerModel");
        //PlayerDir = targetObj.GetComponent<PlayerControler>().GetPlayerDir();

        Correct = new Vector3(0.0f, -1.5f, 0.5f);
        targetPos = targetObj.transform.position + Correct;
        CameraVertical = new Vector3(0.0f, 0.0f, 0.0f);

        CameraRot = transform.localEulerAngles.y / 180;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        Debug.Log(transform.right);
        //Debug.Log(transform.localEulerAngles);

        //右スティック（追加）
        if (Input.GetAxisRaw("Vertical2") < 0)
        {
            Debug.Log("上に傾いている"); 
        }
        else if (0 < Input.GetAxisRaw("Vertical2"))
        {
            Debug.Log("下に傾いている");
        }
        else
        {
            Debug.Log("上下に傾いていない");
        }
        if (Input.GetAxisRaw("Horizontal2") < 0)
        {
            Debug.Log("左に傾いている");
        }
        else if (0 < Input.GetAxisRaw("Horizontal2"))
        {
            Debug.Log("右に傾いている");
        }
        else
        {
            Debug.Log("左右方向には傾いていない");
        }


        //反時計回りに回転
        if (Input.GetKey(KeyCode.Q) || Input.GetKey("joystick button 4") || Input.GetAxisRaw("Horizontal2") < 0)
        {
            if (0 == Input.GetAxisRaw("Vertical2"))
            {
                // targetの位置のY軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * -50f);
            }
        }

        //時計回りに回転
        if (Input.GetKey(KeyCode.E) || Input.GetKey("joystick button 5") || 0 < Input.GetAxisRaw("Horizontal2"))
        {
            if (0 == Input.GetAxisRaw("Vertical2"))
            {
                // targetの位置のY軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 50f);
            }
        }

        //上に移動
        //上スティックを傾けている時
        if (Input.GetAxisRaw("Vertical2") < 0)
        {
            if (0 == Input.GetAxisRaw("Horizontal2"))
            {
                // targetの位置のX軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, transform.right, Time.deltaTime * 50f);
            }
        }

        //下に移動
        //下スティックを傾けている時
        if (0 < Input.GetAxisRaw("Vertical2"))
        {
            if (0 == Input.GetAxisRaw("Horizontal2"))
            {
                // targetの位置のX軸を中心に、回転（公転）する
                transform.RotateAround(targetPos, transform.right, Time.deltaTime * -50f);
            }
        }
    }
}
