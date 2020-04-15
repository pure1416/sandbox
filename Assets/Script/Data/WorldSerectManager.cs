using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WorldSerectManager : MonoBehaviour
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
            worlds[0].GetComponent<WorldUnlock>().SetClearFlg(true);
            worlds[1].GetComponent<WorldUnlock>().SetUnlockFlg(true);
        }

        //World_2クリア済み
        if ((wf & WorldFlags.World_2) == WorldFlags.World_2)
        {
            worlds[1].GetComponent<WorldUnlock>().SetClearFlg(true);
            worlds[2].GetComponent<WorldUnlock>().SetUnlockFlg(true);
        }

        //World_3クリア済み
        if ((wf & WorldFlags.World_3) == WorldFlags.World_3)
        {
            worlds[2].GetComponent<WorldUnlock>().SetClearFlg(true);
            worlds[3].GetComponent<WorldUnlock>().SetUnlockFlg(true);
        }

        //World_4クリア済み
        if ((wf & WorldFlags.World_4) == WorldFlags.World_4)
        {
            worlds[3].GetComponent<WorldUnlock>().SetClearFlg(true);
        }

        //書置き。ロックアンロックに関してはメモりつつ仕組みを考えた方がいい
    }

    //一気にLockする関数
    void Lock(int num)
    {
        for (; num < 4; num++)
        {
            worlds[4 - num].GetComponent<WorldUnlock>().SetUnlockFlg(false);
        }
    }
}
