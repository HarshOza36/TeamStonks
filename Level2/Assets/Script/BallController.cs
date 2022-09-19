using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce;

    private GameManager gm;
    private int ringPassAllowance;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        ringPassAllowance = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = Vector3.up * jumpForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        //if colliding with platform
        if(collision.gameObject.tag == "Platform" & gm.getNumPowerUps() < 3)
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }
        else if(collision.gameObject.tag == "Platform" & gm.getNumPowerUps() == 3)
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            if(ringPassAllowance == 0)
            {
                collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
                gm.resetNumPowerUps();
                rb.AddForce(Vector3.down * 2);
                ringPassAllowance = 3;
            }
            else{
                ringPassAllowance--;
            }

        }
        //if collding with power up
        else if(collision.gameObject.tag == "PowerUP")
        {
            //power up +=1
            
            //Debug.Log("collide with power up");
            gm.IncreasePowerUps(1);
            if(gm.getNumPowerUps() < 3)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector3(0, jumpForce * 2, 0);
            }
            else if(gm.getNumPowerUps() == 3)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector3(0, jumpForce * 10, 0);
                
            }
            //string debut_str = "power number: " + gm.getNumPowerUps();
            Debug.Log("power number: " + gm.getNumPowerUps());
        }
    }


}
