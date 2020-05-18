using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    //定数宣言
    private const int FIRST_OPT = 0;
    private const int LAST_OPT = 3;

    [Header("現在選択されているオプション")]
    [SerializeField] public int NowSelOpt;

    [Header("各種オブジェクト")]
    public GameObject[] option; //オプションオブジェクト
    public GameObject cursor; //カーソル部品　本体
    public GameObject cursorL; //カーソル部品　L
    public GameObject cursorR; //カーソル部品　R
    [SerializeField] private OptCorsorMove OpCM; //カーソル動かすコンポネ

    public float dist;      //距離

    // Start is called before the first frame update
    void Start()
    {
        OpCM = cursor.GetComponent<OptCorsorMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //カーソル移動中は入力できないようにする
        //if (OpCM.GetMoveEnd())
        {
            //音量バーを選択している
            if (NowSelOpt < 2)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //次へ
                    //option[NowSelOpt].GetComponent<VolumeChange>().VolUp();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    //前へ
                    //option[NowSelOpt].GetComponent<VolumeChange>().VolDown();
                }
            }
            else if (NowSelOpt == 2)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    //次へ
                    //option[NowSelOpt].GetComponent<HowTo>().HTOpen();
                }
            }

            //キー操作で操作できるようにする
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //上へ
                OpCM.GoPrev();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //下へ
                OpCM.GoNext();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //戻る
                Debug.Log("b!");
                option[0].SetActive(false);
            }
        }
    }

    //NowSelStageの設定、ワールド名の変更
    public void SetNowSelOpt(int OptNo)
    {
        NowSelOpt = NowSelOpt + OptNo;
    }

    //次オプションのGetter
    public bool GetNextOpt()
    {
        if (NowSelOpt == LAST_OPT)
        {
            //最後のステージを選択していたら移動させない
            return false;
        }

        return true;
    }

    //前オプションのGetter
    public bool GetPrevOpt()
    {
        if (NowSelOpt == FIRST_OPT)
        {
            //最初のステージを選択していたら移動させない
            return false;
        }

        return true;
    }

    //次のステージの場所Getter
    public Vector3 GetNextPos()
    {
        //さらに調整
        int ajuL, ajuR;
        if (NowSelOpt + 1 >= 2)
        {
            ajuL = 130;
            ajuR = -130;
        }
        else
        {
            ajuL = -20;
            ajuR = -50;
        }

        float tmp = option[NowSelOpt + 1].transform.position.x -
                                                        (option[NowSelOpt + 1].GetComponent<RectTransform>().sizeDelta.x / 2) - dist + ajuL;
        cursorL.GetComponent<OptLRMove>().SetEndPosX(tmp);

        tmp = option[NowSelOpt + 1].transform.position.x +
                                                        (option[NowSelOpt + 1].GetComponent<RectTransform>().sizeDelta.x / 2) + dist + ajuR;
        cursorR.GetComponent<OptLRMove>().SetEndPosX(tmp);
        return option[NowSelOpt + 1].transform.position;
    }

    //前ステージの場所Getter
    public Vector3 GetPrevPos()
    {
        //さらに調整
        int ajuL, ajuR;
        if (NowSelOpt - 1 == 2)
        {
            ajuL = 130;
            ajuR = -130;
        }
        else
        {
            ajuL = -20;
            ajuR = -50;
        }

        float tmp = option[NowSelOpt - 1].transform.position.x -
                                                (option[NowSelOpt - 1].GetComponent<RectTransform>().sizeDelta.x / 2) - dist + ajuL;
        cursorL.GetComponent<OptLRMove>().SetEndPosX(tmp);

        tmp = option[NowSelOpt - 1].transform.position.x +
                                                        (option[NowSelOpt - 1].GetComponent<RectTransform>().sizeDelta.x / 2) + dist + ajuR;
        cursorR.GetComponent<OptLRMove>().SetEndPosX(tmp);
        return option[NowSelOpt - 1].transform.position;
    }
}
