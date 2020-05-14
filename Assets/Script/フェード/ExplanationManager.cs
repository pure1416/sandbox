using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationManager : MonoBehaviour
{
    public GameObject ExplanationObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown("joystick button 2"))
        {
            ExplanationObj.SetActive(false);
        }
    }
}
