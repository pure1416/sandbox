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
            p0 = Rupee.transform.position;
            p1 = PlayerObg.transform.position + ((Rupee.transform.position - PlayerObg.transform.position) / 3);
            p2 = PlayerObg.transform.position + ((Rupee.transform.position - PlayerObg.transform.position) / 3) * 2;
            p3 = PlayerObg.transform.position;
            testbejie = true;
        }
        GameClearFlg = PlayerObg.GetComponent<PlayerControler>().GetGameClearFlg();
        if(GameClearFlg == true)
        {
            animator.SetBool("clear", true);
            ClearModelAnimTime += Time.deltaTime;
        }
        if (ClearModelAnimTime >= 1.5f)
        {
           
        }

        if (testbejie == true)
        {
            t += Time.deltaTime;
            Vector3 a = Vector3.Lerp(p0, p1, t); // 緑色の点1
            Vector3 b = Vector3.Lerp(p1, p2, t); // 緑色の点2
            Vector3 c = Vector3.Lerp(p2, p3, t); // 緑色の点3

            Vector3 d = Vector3.Lerp(a, b, t);   // 青色の点1
            Vector3 e = Vector3.Lerp(b, c, t);   // 青色の点2

            Rupee.transform.position += Vector3.Lerp(d / 100, e / 100, t / 100);    // 黒色の点
        }
     }
}
