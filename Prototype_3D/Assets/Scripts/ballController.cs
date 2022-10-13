using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


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
    bool canDoubleJump = false;  //the flag to check whether double jump is legal
    public static TMP_Text gameStart;
    public bool gameStartBool = false;
    public float Super_Jump = 10f;
    public float poison_time = 0f;
    public float poison_multiplier = 5f;
    public bool isTwoPuzzle = false;
    public bool twoPuzzlePos = false;
    private Vector3 vec;
    public bool Inverse_Flag = false;

    //doodle jump
    public GameObject dooleJump;
    private doodleJump ddlJmp;


    //private bool IsGround = true;

    AudioSource audioData;

    public float acceleration;
    public float distancemoved = 0f;
    public float lastdistancemoved = 0f;
    public float last;

    private bool percentage_event_submitted = false;

    private GameObject Star;

    private float ball_start_pos;
private TMP_Text minus;
    

    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        Physics.gravity = new Vector3(0, -9.8f, 0);
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;
        Star = GameObject.Find("RedStar");
        minus = GameObject.Find("minus").GetComponent<TMP_Text>();
        minus.text = "";
        ball_start_pos = this.transform.position.y;
        ddlJmp = dooleJump.GetComponent<doodleJump>();
        if (sceneName == "TwoPuzzle" || sceneName == "Level4")
        {
            isTwoPuzzle = true;
        }else{	
            isTwoPuzzle = false;
        }

        //Debug.Log(Physics.gravity);

        gameStart = GameObject.Find("GameStart").GetComponent<TMP_Text>();
        StartCoroutine(CountdownCoroutine());
        //.Log(gameStartBool)

        
        //StartCoroutine(Post(sceneName));



        audioData = GetComponent<AudioSource>();
        //restart = GetComponent<restart>
        rb = GetComponent<Rigidbody>();
        timeRemaining = GetComponent<timer>();

        last = transform.position[1];
    }

    IEnumerator CountdownCoroutine()
    {
        //Debug.Log("Game Start Countdown");
        gameStart.text = "3";
        yield return new WaitForSeconds(1.0f);
        gameStart.text = "2";
        yield return new WaitForSeconds(1.0f);
        gameStart.text = "1";
        yield return new WaitForSeconds(1.0f);
        gameStart.text = "Go!";
        // start the game here
        yield return new WaitForSeconds(1.0f);
        gameStart.text = "";
        gameStartBool = true;
        yield return null;
    }

    IEnumerator Post(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1410873621", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostEnd(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1924280004", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostEndTime(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1546907951", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostSpacePress(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1352511414", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostAPress(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.156529221", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostDPress(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1560130765", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostMPress(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1524196135", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostPercentageCompleted(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1348635493", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();
    }

    IEnumerator PostBoosterCollected(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1828337754", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();
    }

    IEnumerator PostPoisonCollected(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1824796752", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();
    }
    
    IEnumerator PostSpikeTouched(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1345590531", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(-1*Physics.gravity, ForceMode.Force);
        Scene currentScene = SceneManager.GetActiveScene();
        if (gameStartBool)
        {
            if (timeRemaining.timeRemaining != 0 && !gameWon)
            {
                if (isTwoPuzzle == true)
                {
                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        //StartCoroutine(PostMPress(currentScene.name));
                        if (twoPuzzlePos == false)
                        {
                            // 8.5f,0f,0.25f
                            gameObject.transform.position = transform.position + new Vector3(7.75f, 0f, -0.09f);
                            Center_Cylinder = GameObject.Find("Center_CylinderB");
                            twoPuzzlePos = true;
                        }
                        else
                        {
                            // -8.5f,0f,-0.25f
                            gameObject.transform.position = transform.position + new Vector3(-7.75f, 0f, 0.09f);
                            Center_Cylinder = GameObject.Find("Center_Cylinder");
                            twoPuzzlePos = false;
                        }
                    }
                }

                if (poison_time > 0)
                {
                    poison_time -= Time.deltaTime;
                    //Debug.Log("poison_time = " + poison_time.ToString());
                }

                if (canDoubleJump || IsGrounded())
                {   //If the ball is able to jump: red
                    if (poison_time <= 0)
                    {
                        GetComponent<Renderer>().material.color = Color.red;
                    }
                    else
                    {
                        GetComponent<Renderer>().material.color = Color.black;
                    }
                }
                else
                {   //If the ball is not able to jump: white
                    GetComponent<Renderer>().material.color = Color.white;
                }

                if (Input.GetKeyDown("space"))
                {
                    if (Inverse_Flag)
                    {
                        vec = Vector3.down;
                    }
                    else
                    {
                        vec = Vector3.up;
                    }

                    if (IsGrounded())
                    {
                        if (poison_time > 0)
                        {
                            rb.velocity = vec * poison_multiplier;
                        }
                        else
                        {
                            rb.velocity = vec * jump_multiplier;
                        }
                        rb.AddForce(vec, ForceMode.Impulse);
                        StartCoroutine(PostSpacePress(currentScene.name));
                        if (!Inverse_Flag)
                        {
                            canDoubleJump = true;
                        }

                    }
                    else if (canDoubleJump)

                    {
                        Debug.Log("Double Jump");
                        if (poison_time > 0)
                        {
                            rb.velocity = vec * poison_multiplier;
                        }
                        else
                        {
                            rb.velocity = vec * jump_multiplier;
                        }
                        rb.AddForce(vec, ForceMode.Impulse);
                        StartCoroutine(PostSpacePress(currentScene.name));
                        if (!Inverse_Flag)
                        {
                            canDoubleJump = false;
                        }
                    }

                }

                

                if(Input.GetKeyDown(KeyCode.A)){
                    //StartCoroutine(PostAPress(currentScene.name));
                }

                if(Input.GetKeyDown(KeyCode.D)){
                    //StartCoroutine(PostDPress(currentScene.name));
                    
                }

                if (Input.GetButton("Horizontal"))
                {
                    OrbitLeft(true);
                }
                else if (Input.GetButton("Vertical"))
                {
                    OrbitLeft(false);
                }
                
            }
            else
            {
                if(!percentage_event_submitted){

                    float st = Star.transform.position.y - ball_start_pos;
                    float bl = this.transform.position.y - ball_start_pos;

                    Debug.Log("Game End Positions:::: " + st.ToString() + "  " +bl.ToString());

                    if(st<bl){
                        //StartCoroutine(PostPercentageCompleted(currentScene.name + "_" + "1"));
                    }else{
                        //StartCoroutine(PostPercentageCompleted(currentScene.name + "_" + (bl / st).ToString()));
                    }
                    percentage_event_submitted = true;
                }

                rb.useGravity = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
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
        if (Inverse_Flag)
        {
            return Physics.Raycast(transform.position, Vector3.up, GetComponent<SphereCollider>().radius);
        }
        else
        {
            return Physics.Raycast(transform.position, Vector3.down, groundDistance);
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        Debug.Log(obj.gameObject.name);
        audioData.Play(0);
        Scene currentScene = SceneManager.GetActiveScene();
        // Debug.Log(obj.gameObject.name);
        if (obj.gameObject.name == "RedStar" || obj.gameObject.name == "RedStarB")
        {
            gameWon = true;
            //StartCoroutine(PostEnd(currentScene.name));
            //StartCoroutine(PostEndTime(currentScene.name + "_" + timeRemaining.timeRemaining.ToString()));
            // It is object B
        }

        if (obj.gameObject.name == "spike" && !gameWon)
        {

            //StartCoroutine(PostSpikeTouched(currentScene.name));
            if (timeRemaining.timeRemaining > 5)
            {
                Destroy(obj.gameObject);
                
                timeRemaining.timeRemaining -= 5;
                //StartCoroutine(HealthCouroutine());
                
            }
            else
            {
                timeRemaining.timeRemaining = 0;
                gameWon = false;
            }

        }

        //-----------------------------------------for dangerous platforms
        if (obj.gameObject.tag == "danger")
        {
            //Debug.Log("platform status: "+obj.gameObject.tag);
            //Debug.Log("ball height: " + rb.transform.position.y);
            //Debug.Log("dangerous platform height: " + obj.transform.position.y);
            float ballHeight = rb.transform.position.y;
            float dangerPlatformHeight = obj.transform.position.y;
            if(ballHeight > dangerPlatformHeight)
            {
                Destroy(obj.gameObject);
            }

        }

        //--------------------------------------------------------for doodle jump
        if (ddlJmp.hasDoodleJump)
        {
            Debug.Log("doodle collision on arc"+obj.collider.name);
            if (rb.position.y < obj.collider.transform.position.y && obj.gameObject.tag != "CenterCylinder")
            {
                obj.collider.enabled = false;
            }

        }


        if (obj.gameObject.name == "Power_Up" && !gameWon)
        {
            //obj.gameObject.SetActive(false);
            //StartCoroutine(PostBoosterCollected(currentScene.name));
            //Vector3 pos = this.transform.position;

            rb.velocity = new Vector3(0, Super_Jump * 2, 0);
            //obj.gameObject.SetActive(true);
        }

        if (obj.gameObject.name == "Power_Down" && !gameWon)
        {
            //StartCoroutine(PostPoisonCollected(currentScene.name));
            Destroy(obj.gameObject);
            poison_time += 5f;
            //Debug.Log("poison_time = " + poison_time.ToString());
        }
        if(obj.gameObject.tag == "MovingArc")
	    {
	        transform.parent  = obj.transform.parent.transform.Find("Empty_parent").transform;
	    }

        // For Inversion
        if (obj.gameObject.name == "Inverse" && !gameWon)
        {
            if (!Inverse_Flag) 
            {
                Physics.gravity = new Vector3(0, 7, 0);
                Inverse_Flag = !Inverse_Flag;
                canDoubleJump = false;
                Destroy(obj.gameObject);
            }   
            else
            {   
                Physics.gravity = new Vector3(0, -9.8f, 0);
                Inverse_Flag = !Inverse_Flag;
		canDoubleJump = true;
                Destroy(obj.gameObject);
            }
        }

    }
   
 IEnumerator HealthCouroutine()
    {
        minus.text = "-5";
        yield return new WaitForSeconds(1.0f);
        minus.text = "";
        yield return null;
    }

    void OnCollisionExit(Collision obj)
    {
	    if(obj.gameObject.tag == "MovingArc")
	    {
	        transform.parent = null;
	    }

        if (ddlJmp.hasDoodleJump)
        {
            Debug.Log("exit doodle collision on arc");
            obj.collider.enabled = true;
            
        }

    }
	
	
}
