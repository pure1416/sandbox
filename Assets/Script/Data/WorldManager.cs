using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldManager : MonoBehaviour
{
    //ワールドごとのクリアフラグ管理用
    [Flags]
    private enum WorldFlag
    {
        NoWF    = 0,        //0000
        World_1 = 1 << 0,   //0001
        World_2 = 1 << 1,   //0010
        World_3 = 1 << 2,   //0100
        World_4 = 1 << 3,   //1000
    }

    WorldFlag wf;           //WorldFlag格納用
    public Text WorldFlagView;     //見る用

    // Start is called before the first frame update
    void Start()
    {
        int tmp = PlayerPrefs.GetInt("WORLD_FLAG", 0);
        wf = (WorldFlag)tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //リセット
            wf = wf & 0;

            Debug.Log("リセット");
        }

        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            //セーブ
            PlayerPrefs.SetInt("WORLD_FLAG", (int)wf);
            PlayerPrefs.Save();
            Debug.Log("セーブ");
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //ロード
            wf = (WorldFlag)PlayerPrefs.GetInt("WORLD_FLAG", 0);
            Debug.Log("ロード");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //World_1にフラグを立てる
            wf = wf | WorldFlag.World_1;
            Debug.Log("World_1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //World_2にフラグを立てる
            wf = wf | WorldFlag.World_2;
            Debug.Log("World_2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //World_3にフラグを立てる
            wf = wf | WorldFlag.World_3;
            Debug.Log("World_3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //World_4にフラグを立てる
            wf = wf | WorldFlag.World_4;
            Debug.Log("World_4");
        }

        WorldFlagView.text = "WorldFlag：" + wf.ToString();
    }
}
