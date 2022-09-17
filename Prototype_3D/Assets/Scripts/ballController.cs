using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    private Rigidbody rb;
    private timer timeRemaining;
    private restart restartGame;
    public bool gameWon = false;
    public float jump_multiplier = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //restart = GetComponent<restart>
        rb = GetComponent<Rigidbody>();
        timeRemaining = GetComponent<timer>();
        //Debug.Log(timeRemaining.timeRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining.timeRemaining != 0 && !gameWon) {
            if (Input.GetKeyDown("space")){
                rb.velocity = Vector3.up * jump_multiplier;
                rb.AddForce(Vector3.up, ForceMode.Impulse);
            }
        }
        else {
            rb.useGravity = false;
            rb.velocity = new Vector3(0,0,0);

        }
    }

    void OnCollisionEnter(Collision obj)
    {
        Debug.Log(obj.gameObject.name);
        if(obj.gameObject.name == "RedStar")
        {
            gameWon = true;
            // It is object B
        }

        if(obj.gameObject.name == "spike")
        {
            timeRemaining.timeRemaining -= 5;
            timer.timeText.color = Color.red;
            timer.DisplayTime(timeRemaining.timeRemaining);
            //timer.timeText.color = Color.white;
        }
    }

}
