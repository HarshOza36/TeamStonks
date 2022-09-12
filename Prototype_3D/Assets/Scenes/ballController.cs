using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    private Rigidbody rb;
    private timer timeRemaining;
    private restart restartGame;
    public bool gameWon = false;
    //public float pushForce = 400f;
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
                //rb.AddForce (new Vector3 (0f, pushForce, 0f));
                rb.velocity = Vector3.up * 5f;
                rb.AddForce(Vector3.up, ForceMode.Impulse);
            }
        }
        else {
            rb.useGravity = false;
            rb.velocity = new Vector3(0,0,0);

        }
    }

    void OnCollisionEnter(Collision star)
    {
        Debug.Log(star.gameObject.name);
        if(star.gameObject.name == "RedStar")
        {
            gameWon = true;
            // It is object B
        }
    }

}
