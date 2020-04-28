using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WSManager : MonoBehaviour
{
    [Header("ワールド")]
    public GameObject[] worlds; //ワールドの管理オブジェクトを入れる？

    [Flags]
    private enum WorldFlags
    {
        NoWF = 0,           //素
        World_1 = 1 << 0,   //0001
        World_2 = 1 << 1,   //0010
        World_3 = 1 << 2,   //0100
        World_4 = 1 << 3,   //1000
    }

    [SerializeField] WorldFlags wf;      //WorldFlag格納

    // Start is called before the first frame update
    void Start()
    {
        wf = (WorldFlags)PlayerPrefs.GetInt("WORLD_FLAG", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //World_1クリア済み
        if ((wf & WorldFlags.World_1) == WorldFlags.World_1)
        {
            worlds[0].GetComponent<WorUnl>().SetClearFlg(true);
            worlds[1].GetComponent<WorUnl>().SetUnlockFlg(true);
        }
        else
        {
            worlds[0].GetComponent<WorUnl>().SetClearFlg(false);
            worlds[0].GetComponent<WorUnl>().SetUnlockFlg(true);
            worlds[1].GetComponent<WorUnl>().SetUnlockFlg(false);
        }

        //World_2クリア済み
        if ((wf & WorldFlags.World_2) == WorldFlags.World_2)
        {
            worlds[1].GetComponent<WorUnl>().SetClearFlg(true);
            worlds[2].GetComponent<WorUnl>().SetUnlockFlg(true);
        }
        else
        {
            worlds[1].GetComponent<WorUnl>().SetClearFlg(false);
            worlds[2].GetComponent<WorUnl>().SetUnlockFlg(false);
        }

        //World_3クリア済み
        if ((wf & WorldFlags.World_3) == WorldFlags.World_3)
        {
            worlds[2].GetComponent<WorUnl>().SetClearFlg(true);
            worlds[3].GetComponent<WorUnl>().SetUnlockFlg(true);
        }
        else
        {
            worlds[2].GetComponent<WorUnl>().SetClearFlg(false);
            worlds[3].GetComponent<WorUnl>().SetUnlockFlg(false);
        }

        //World_4クリア済み
        if ((wf & WorldFlags.World_4) == WorldFlags.World_4)
        {
            worlds[3].GetComponent<WorUnl>().SetClearFlg(true);
        }
        else
        {
            worlds[3].GetComponent<WorUnl>().SetClearFlg(false);
        }

        //書置き。ロックアンロックに関してはメモりつつ仕組みを考えた方がいい
    }
}
