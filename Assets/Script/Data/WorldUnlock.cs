using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldUnlock : MonoBehaviour
{
    [Header("Flags")]
    public bool UnlockFlg;    //アンロックフラグ
    public bool ClearFlg;     //クリアフラグ

    [Header("Next Stages")]
    public GameObject[] NextWorlds = new GameObject[4];      //次のステージ(0:上、1:右、2:下、3:左)

    [Header("Scene")]
    [SerializeField] private string GoScene;   //行先のシーン
    [SerializeField] private int GoSceneNo;   //行先のシーン番号

    private GameObject FadeObj; //フェードのオブジェクト
    // Start is called before the first frame update
    void Start()
    {
        FadeObj = GameObject.Find("FadePanel");
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

    //次のステージのUnlockFlg取得
    public GameObject GetNextWorlds(int dir)
    {
        //0:上　1:右　2:下　3:左
        //設定されてなかったら自分を返す
        if (NextWorlds[dir] == null)
        {
            return this.gameObject;
        }

        return NextWorlds[dir]; ;
    }

    //シーン遷移
    public void GoSceneChange()
    {
        //フェードしてシーン遷移
        FadeObj.GetComponent<FadeManager>().FadeScene(GoSceneNo);

        //SceneManager.LoadScene(GoScene);
    }
}
