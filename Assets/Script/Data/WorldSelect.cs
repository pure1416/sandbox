using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelect : MonoBehaviour
{
    public GameObject NowWorld;     //今選択しているワールド

    // Start is called before the first frame update
    void Start()
    {
        //初期位置にセット
        this.transform.position = NowWorld.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //方向キーを押したら、方向に応じてUnlockFlgを取得して、trueなら移動する
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Horizontal") > 0)
        {
            CursorMove(0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            CursorMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Horizontal") < 0)
        {
            CursorMove(2);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            CursorMove(3);
        }

        //スペースキーでシーン遷移
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
        {
            NowWorld.GetComponent<WorldUnlock>().GoSceneChange();
        }
    }

    //移動関数
    private void CursorMove(int dir)
    {
        //取得
        GameObject tmp = NowWorld.GetComponent<WorldUnlock>().GetNextWorlds(dir);

        //今のと同じだったら何もしない
        if (tmp != NowWorld &&
            tmp.GetComponent<WorldUnlock>().GetUnlockFlg())
        {
            //移動
            this.transform.position = tmp.transform.position;
            //今の場所更新
            NowWorld = tmp;
        }
    }
}
