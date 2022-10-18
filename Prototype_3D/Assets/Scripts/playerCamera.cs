using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCamera : MonoBehaviour
{   
    public ballController ball;
    private Vector3 offset;
    private Vector3 oriOffset;
    public float lerpSpeed;
    public float ballOffsetMultiplier;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LevelReverse"){
            offset = new Vector3(0.0f,-1.0f,-10.0f);
        }
        else {
            offset = transform.position - 6* ball.transform.position;
            oriOffset = offset;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(ball.Inverse_Flag)
        {
            offset = new Vector3(0.0f,-1.0f,-10.0f);
        }
        else
        {
            offset = oriOffset;
        }

        Vector3 newPos = Vector3.Lerp(transform.position, ball.transform.position + offset, lerpSpeed);
        transform.position = newPos;
    }
}
// 0 + x = 0
// 1.523412 + YieldInstruction  = 0.5
// 1.1 + WindZone = -5