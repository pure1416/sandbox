using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //定数宣言
    private const int FIRST_OPT = 0;
    private const int LAST_OPT = 3;

    //enumみたいな
    private const int OPT_BGM = 0;
    private const int OPT_SE = 1;
    private const int OPT_HOWTO = 2;
    private const int OPT_TITLE = 3;

    [Header("現在選択されているオプション")]
    [SerializeField] public int NowSelOpt;

    [Header("各種オブジェクト")]
    public GameObject[] option; //オプションオブジェクト
    public GameObject cursor; //カーソル部品　本体
    public GameObject cursorL; //カーソル部品　L
    public GameObject cursorR; //カーソル部品　R
    [SerializeField] private OptCorsorMove OpCM; //カーソル動かすコンポネ

    public Button button;

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
        if (OpCM.GetMoveEnd())
        {
            //操作説明表示中は操作できない
            if (option[OPT_HOWTO].GetComponent<HowToChange>().GetHowToFlg() == false)
            {
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
                    button.Select();
                    this.gameObject.SetActive(false);
                }
            }

            //選んでいる項目によって操作を変える
            switch (NowSelOpt)
            {
                //BGM
                case OPT_BGM:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        //音量上げ
                        option[NowSelOpt].GetComponent<VolumeChange>().VolUp();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        //音量下げ
                        option[NowSelOpt].GetComponent<VolumeChange>().VolDown();
                    }

                    break;
                //SE
                case OPT_SE:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        //音量上げ
                        option[NowSelOpt].GetComponent<VolumeChange>().VolUp();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        //音量下げ
                        option[NowSelOpt].GetComponent<VolumeChange>().VolDown();
                    }

                    break;
                //操作説明
                case OPT_HOWTO:
                    if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Return))
                    {
                        //決定
                        option[NowSelOpt].GetComponent<HowToChange>().HowToOpen();
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        //閉じる
                        option[NowSelOpt].GetComponent<HowToChange>().HowToClose();
                    }

                        break;
                //タイトルに戻る
                case OPT_TITLE:
                    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Return))
                    {
                        //決定ボタンでオプション消す
                        button.Select();
                        this.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //NowSelOptの設定
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

    //次のオプションの場所Getter
    public Vector3 GetNextPos()
    {
        //さらに調整
        float ajuL, ajuR;
        float scale = this.GetComponent<RectTransform>().localScale.x;
        if (NowSelOpt + 1 >= 2)
        {
            ajuL = 130 * scale;
            ajuR = -130 * scale;
        }
        else
        {
            ajuL = -20 * scale;
            ajuR = -50 * scale;
        }

        float tmp = option[NowSelOpt + 1].transform.position.x -
                                                        (option[NowSelOpt + 1].GetComponent<RectTransform>().sizeDelta.x * scale / 2) - (dist * scale) + ajuL;
        cursorL.GetComponent<OptLRMove>().SetEndPosX(tmp);

        tmp = option[NowSelOpt + 1].transform.position.x +
                                                        (option[NowSelOpt + 1].GetComponent<RectTransform>().sizeDelta.x * scale / 2) + (dist * scale) + ajuR;
        cursorR.GetComponent<OptLRMove>().SetEndPosX(tmp);
        return option[NowSelOpt + 1].transform.position;
    }

    //前オプションの場所Getter
    public Vector3 GetPrevPos()
    {
        //さらに調整
        float ajuL, ajuR;
        float scale = this.GetComponent<RectTransform>().localScale.x;
        if (NowSelOpt - 1 == 2)
        {
            ajuL = 130 * scale;
            ajuR = -130 * scale;
        }
        else
        {
            ajuL = -20 * scale;
            ajuR = -50 * scale;
        }

        float tmp = option[NowSelOpt - 1].transform.position.x -
                                                (option[NowSelOpt - 1].GetComponent<RectTransform>().sizeDelta.x * scale / 2) - (dist * scale) + ajuL;
        cursorL.GetComponent<OptLRMove>().SetEndPosX(tmp);

        tmp = option[NowSelOpt - 1].transform.position.x +
                                                        (option[NowSelOpt - 1].GetComponent<RectTransform>().sizeDelta.x * scale / 2) + (dist * scale) + ajuR;
        cursorR.GetComponent<OptLRMove>().SetEndPosX(tmp);
        return option[NowSelOpt - 1].transform.position;
    }
}
