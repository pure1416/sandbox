using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManagement : MonoBehaviour
{
    GameObject PlayerObj;
    GameObject GameOverText;
    bool GameOverFlg;

    // Start is called before the first frame update
    void Start()
    {
        GameOverFlg = false;
        PlayerObj = GameObject.Find("player");
        GameOverText = GameObject.Find("Gameover");

    }

    // Update is called once per frame
    void Update()
    {
        GameOverFlg = PlayerObj.GetComponent<PlayerControler>().GetGameOverFlg();

        if (GameOverFlg == true) {
            //ゲームオーバーテキストを表示
            GameOverText.SetActive(true);

            //Aボタンまたはスペースキーを押したとき現在のシーンを読み込み
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
            {
                // 現在のScene名を取得する
                Scene loadScene = SceneManager.GetActiveScene();
                // Sceneの読み直し
                SceneManager.LoadScene(loadScene.name);
            }
        }
        if (GameOverFlg == false)
        {
            GameOverText.SetActive(false);
        }
    }
}
