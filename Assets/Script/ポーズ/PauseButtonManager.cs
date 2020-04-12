using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable>();
        sel.Select();
    }

    public void PushReStartButton()
    {
        Debug.Log("ゲームに戻る");
    }
    public void PushReturnStageSerectButton()
    {
        Debug.Log("ステージ選択に戻る");
    }
    public void PushOptionButton()
    {
        Debug.Log("オプションを開く");
    }
}
