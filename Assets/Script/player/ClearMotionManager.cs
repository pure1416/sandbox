using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMotionManager : MonoBehaviour
{
    private GameObject PlayerObg;
    private bool GameClearFlg;
    private GameObject Rupee;
    float ClearModelAnimTime;
    Animator animator;
    private Vector3 p0,p1, p2, p3;
    bool RupeeAnimFlg;
    float t;

    bool testbejie;
    // Start is called before the first frame update
    void Start()
    {
        Rupee = GameObject.Find("rupee");
        PlayerObg = GameObject.Find("player");
        animator = GetComponent<Animator>();
        ClearModelAnimTime = 0.0f;
        t = 0.0f;
        testbejie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            





        }
        Debug.Log("P0"+p0);
        Debug.Log("P1"+p1);
        Debug.Log("P2"+p2);
        
        GameClearFlg = PlayerObg.GetComponent<PlayerControler>().GetGameClearFlg();
        if(GameClearFlg == true)
        {
            animator.SetBool("clear", true);
            ClearModelAnimTime += Time.deltaTime;
        }
        if (ClearModelAnimTime >= 1.5f)
        {
            p0 = Rupee.transform.position;
            p1 = (Rupee.transform.position + PlayerObg.transform.position) / 2;
            p1.y += Rupee.transform.position.y + 5.0f;
            p2 = PlayerObg.transform.position;
        }

        if (testbejie == true)
        {
            t += Time.deltaTime / 3;
            Vector3 Q0 = Vector3.Lerp(p0, p1, t);
            Vector3 Q1 = Vector3.Lerp(p1, p2, t);
            Vector3 Q2 = Vector3.Lerp(Q0, Q1, t);

            Rupee.transform.position = Q2;    // 黒色の点
        }
     }
}
