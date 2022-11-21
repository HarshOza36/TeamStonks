using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameCompleteBackground : MonoBehaviour
{
    private ballController ballContr;
    RawImage components;
    // Start is called before the first frame update
    void Start()
    {
        components = GetComponent<RawImage>();
        components.enabled = false;
        ballContr = GameObject.Find("Player_Ball").GetComponent<ballController>();

    }

    // Update is called once per frame
    void Update()
    {   if(ballContr.gameWon == true) {
        components.enabled = true;
    }
        
    }
}
