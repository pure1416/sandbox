using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public float speed = 0.01f;  //透明化の速さ
    float alfa;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数
    bool FadeInFlg;   //FadeInのフラグ
    bool FadeOutFlg;   //FadeOutのフラグ
    int FadeSceneNo;   //シーン遷移の番号保存変数


    // Start is called before the first frame update
    void Start()
    {
        //Panelの色を取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alfa = GetComponent<Image>().color.a;

        FadeInFlg = true;
        FadeOutFlg = false;
        FadeSceneNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(FadeOutFlg);

        if (FadeInFlg == true)
        {
            FadeIn();
        }
        if (FadeOutFlg == true)
        {
            FadeOut();
        }
    }

    //フェードイン
    public void FadeIn()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        if (alfa > 0.0f)
        {
            alfa -= speed;
        }
        if (alfa == 0.0f)
        {
            FadeInFlg = false;

        }
    }

    //フェードアウト
    public void FadeOut()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        if (alfa < 255.0f)
        {
            alfa += speed;
        }
        if (alfa >= 255.0f)
        {
            alfa = 1.0f;
            SceneManager.LoadScene(FadeSceneNo);
            FadeOutFlg = false;
        }
    }

    //FadeInFlgフラグのSetter
    public void SetFadeInFlg(bool F1)
    {
        FadeInFlg = F1;
    }

    //FadeOutFlgフラグのSetter
    public void SetFadeOutFlg(bool F2)
    {
        FadeOutFlg = F2;
    }

    //シーン遷移
    public void FadeScene(int SceneNo)
    {
        FadeSceneNo = SceneNo;
        FadeOutFlg = true;
    }

}
