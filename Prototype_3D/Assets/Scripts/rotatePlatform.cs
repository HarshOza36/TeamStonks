using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlatform : MonoBehaviour
{
    public GameObject ball;
    public timer timeRemaining;
    private ballController ballContr;
    public float rotating_speed = 50f;
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
                transform.Rotate(new Vector3(0, 0, rotating_speed) * Time.deltaTime, Space.Self);
            }
        }
    }
}
