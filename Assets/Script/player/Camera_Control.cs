using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public GameObject targetObj;
    Vector3 targetPos;

    void Start()
    {
        //targetObj = GameObject.Find("playerModel");
        targetPos = targetObj.transform.position;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        //反時計回りに回転
        if (Input.GetKey(KeyCode.Q))
        {
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * -50f);
        }
        // 右に移動

        if (Input.GetKey(KeyCode.E))
        {
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 50f);
        }
    }
}
