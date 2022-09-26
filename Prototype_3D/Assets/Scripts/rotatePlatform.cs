using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlatform : MonoBehaviour
{
    public GameObject ball;
    public timer timeRemaining;
    public ballController ballContr;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Player_Ball");
        timeRemaining = ball.GetComponent<timer>();
        ballContr = ball.GetComponent<ballController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballContr.gameStartBool) {
            if(timeRemaining.timeRemaining != 0 && !ballContr.gameWon) {
                transform.Rotate(new Vector3(0, 0, 50f) * Time.deltaTime, Space.Self);
            }
        }
    }
}
