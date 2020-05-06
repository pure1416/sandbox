using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaUnl : MonoBehaviour
{
    [Header("Flags")]
    public bool UnlockFlg;    //アンロックフラグ
    public bool ClearFlg;     //クリアフラグ

    [Header("Text & Image")]
    public Text ClearText;    //クリアテキスト
    public Image MaskImage;   //上に貼っとくやつ

    [Header("Name")]
    public string StageName;

    [Header("Scene")]
    public int GoSceneNo;   //行先のシーン番号

    // Start is called before the first frame update
    void Start()
    {
        //UnlockFlg = ClearFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnlockFlg)
        {
            if (ClearFlg)
            {
                //クリアしてたらクリアを出す
                ClearText.enabled = true;
            }
            else
            {
                ClearText.enabled = false;
            }
            MaskImage.enabled = false;
            Debug.Log("ここ２");
        }
        else
        {
            //アンロックされていない場合は上張りを張ってクリアフラグを切る
            MaskImage.enabled = true;
            ClearText.enabled = false;
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

    //UnlockフラグのGetter
    public bool GetUnlockFlg()
    {
        return UnlockFlg;
    }

    //ClearフラグのGetter
    public bool GetClearFlg()
    {
        return ClearFlg;
    }

    //遷移先NoのGetter
    public int GetGoSceneNo()
    {
        return GoSceneNo;
    }

    //ワールド名のGetter
    public string GetStageName()
    {
        return StageName;
    }
}
