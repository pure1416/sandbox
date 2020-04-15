using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUnlock : MonoBehaviour
{
    [Header("Flags")]
    public bool UnlockFlg;    //アンロックフラグ
    public bool ClearFlg;     //クリアフラグ

    // Start is called before the first frame update
    void Start()
    {
        UnlockFlg = ClearFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(UnlockFlg)
        {
            if (ClearFlg)
            {
                //クリアしてたら緑にする
                this.GetComponent<Image>().color = Color.green;
            }
            else
            {
                //アンロックだけなら青くする
                this.GetComponent<Image>().color = Color.blue;
            }
        }
        else
        {
            //アンロックされていない場合は赤くしてクリアフラグを切る
            this.GetComponent<Image>().color = Color.red;
            ClearFlg = false;
        }
    }

    //UnlockフラグのSetter
    public void SetUnlockFlg(bool bU)
    {
        UnlockFlg = bU;
    }

    //ClearフラグのSetter
    public void SetClearFlg(bool bC)
    {
        ClearFlg = bC;
    }
}
