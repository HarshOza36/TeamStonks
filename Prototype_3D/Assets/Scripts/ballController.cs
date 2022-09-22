using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class ballController : MonoBehaviour
{
    private Rigidbody rb;
    private timer timeRemaining;
    private restart restartGame;
    public bool gameWon = false;
    public float jump_multiplier = 10f;
    public float groundDistance = 0.5f; //distance to judge whether the ball is on the air
    public float orbitSpeed = 2.5f;   //the speed that the ball rotates
    public GameObject Center_Cylinder; //the game obj that the ball rotates around
    bool canDoubleJump = true;  //the flag to check whether double jump is legal
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        

        var val = 1;
        StartCoroutine(Post(val.ToString()));
        

    
        audioData = GetComponent<AudioSource>();
        //restart = GetComponent<restart>
        rb = GetComponent<Rigidbody>();
        timeRemaining = GetComponent<timer>();
        //Debug.Log(timeRemaining.timeRemaining);
    }
    IEnumerator Post(string s1){
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1410873621", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining.timeRemaining != 0 && !gameWon) {
            if (canDoubleJump || IsGrounded())
            {   //If the ball is able to jump: red
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {   //If the ball is not able to jump: white
                GetComponent<Renderer>().material.color = Color.white;
            }
       
            if (Input.GetKeyDown("space")){              
                if (IsGrounded())
                {
                    rb.velocity = Vector3.up * jump_multiplier;
                    rb.AddForce(Vector3.up, ForceMode.Impulse);
                    canDoubleJump = true;
                }else if (canDoubleJump)
                {
                    rb.velocity = Vector3.up * jump_multiplier;
                    rb.AddForce(Vector3.up, ForceMode.Impulse);
                    canDoubleJump = false;
                } 
            }

            if (Input.GetButton("Horizontal"))
            {             
                OrbitLeft(true);
            }else if (Input.GetButton("Vertical"))
            {
                OrbitLeft(false);
            }    
        }else {
            rb.useGravity = false;
            rb.velocity = new Vector3(0,0,0);
        }

        
    }

    //Orbit movement
    void OrbitLeft(bool left)
    {
        if (left == true)
        {
            transform.RotateAround(Center_Cylinder.transform.position, Vector3.up, orbitSpeed);
        }
        else
        {
            transform.RotateAround(Center_Cylinder.transform.position, Vector3.down, orbitSpeed);
        }
        
    }
    
    //Check whether the ball is on a platform or not
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }

    void OnCollisionEnter(Collision obj)
    {
        audioData.Play(0);
        // Debug.Log(obj.gameObject.name);
        if(obj.gameObject.name == "RedStar")
        {
            gameWon = true;
            // It is object B
        }

        if(obj.gameObject.name == "spike" && !gameWon)
        {
            if (timeRemaining.timeRemaining > 5)
            {
                Destroy(obj.gameObject);
                timeRemaining.timeRemaining -= 5;        
            }
            else
            {
                timeRemaining.timeRemaining = 0;
                gameWon = false;
            }
            
        }


    }

}
