using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMotionManager : MonoBehaviour
{
    private GameObject PlayerObg;
    private bool GameClearFlg;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObg = GameObject.Find("player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GameClearFlg = PlayerObg.GetComponent<PlayerControler>().GetGameClearFlg();
        if(GameClearFlg == true)
        {
            animator.SetBool("clear", true);
        }
    }
}
