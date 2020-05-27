using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public bool turn;

    // Start is called before the first frame update
    void Start()
    {
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(turn)
            {
                this.GetComponent<Animator>().Play("Turn_1");
                turn = false;
                Debug.Log("１");
            }
            else
            {
                this.GetComponent<Animator>().Play("Turn_2");
                turn = true;
                Debug.Log("２");
            }
        }
    }
}
