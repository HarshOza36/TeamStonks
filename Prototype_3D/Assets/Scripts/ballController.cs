using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public static TMP_Text gameStart;
    public bool gameStartBool = false;
    public float Super_Jump = 10f;
    public float poison_time = 0f;
    public float poison_multiplier = 5f;
    public bool isTwoPuzzle = false;
    public bool twoPuzzlePos = false;

    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
         // Create a temporary reference to the current scene.
         Scene currentScene = SceneManager.GetActiveScene ();
         // Retrieve the name of this scene.
         string sceneName = currentScene.name;
         if(sceneName == "TwoPuzzle"){
	isTwoPuzzle = true;
         }

        gameStart = GameObject.Find("GameStart").GetComponent<TMP_Text>();
        StartCoroutine(CountdownCoroutine());
        Debug.Log(gameStartBool);

        var val = 1;
        StartCoroutine(Post(val.ToString()));



        audioData = GetComponent<AudioSource>();
        //restart = GetComponent<restart>
        rb = GetComponent<Rigidbody>();
        timeRemaining = GetComponent<timer>();
    }

    IEnumerator CountdownCoroutine() {
        Debug.Log("Game Start Countdown");
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

    IEnumerator Post(string s1) {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1410873621", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostEnd(string s1) {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1924280004", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    IEnumerator PostEndTime(string s1) {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1546907951", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStartBool) {
            if (timeRemaining.timeRemaining != 0 && !gameWon) {
               if(isTwoPuzzle == true){
	if(Input.GetKeyDown(KeyCode.M)){
		if(twoPuzzlePos == false){
			gameObject.transform.position = transform.position +  new Vector3(8.5f,0f,0.25f);
			Center_Cylinder = GameObject.Find("Center_CylinderB");
			twoPuzzlePos = true;
		}
		else
		{
			gameObject.transform.position = transform.position +  new Vector3(-8.5f,0f,-0.25f);
			Center_Cylinder = GameObject.Find("Center_Cylinder");
			twoPuzzlePos = false;
		}
                         }
                }

                if (poison_time > 0)
                {               
                    poison_time -= Time.deltaTime;
                    Debug.Log("poison_time = " + poison_time.ToString());
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
                
                if (Input.GetKeyDown("space")) {
                    

                    if (IsGrounded())
                    {
                        if (poison_time > 0)
                        {
                            rb.velocity = Vector3.up * poison_multiplier;
                        }
                        else
                        {
                            rb.velocity = Vector3.up * jump_multiplier;
                        }
                        rb.AddForce(Vector3.up, ForceMode.Impulse);
                        canDoubleJump = true;

                    } else if (canDoubleJump)
                    
                    {
                        if (poison_time > 0)
                        {
                            rb.velocity = Vector3.up * poison_multiplier;
                        }
                        else
                        {
                            rb.velocity = Vector3.up * jump_multiplier;
                        }
                        rb.AddForce(Vector3.up, ForceMode.Impulse);
                        canDoubleJump = false;
                    }
                }

                if (Input.GetButton("Horizontal"))
                {
                    OrbitLeft(true);
                } else if (Input.GetButton("Vertical"))
                {
                    OrbitLeft(false);
                }
            } else {
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
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }

    void OnCollisionEnter(Collision obj)
    {
        audioData.Play(0);
        // Debug.Log(obj.gameObject.name);
        if (obj.gameObject.name == "RedStar" || obj.gameObject.name == "RedStarB")
        {
            gameWon = true;
            var val = 1;
            StartCoroutine(PostEnd(val.ToString()));
            StartCoroutine(PostEndTime(timeRemaining.timeRemaining.ToString()));
            // It is object B
        }

        if (obj.gameObject.name == "spike" && !gameWon)
        {
            if (timeRemaining.timeRemaining > 5)
            {
                Destroy(obj.gameObject);
                //TODO: display -5
                timeRemaining.timeRemaining -= 5;
            }
            else
            {
                timeRemaining.timeRemaining = 0;
                gameWon = false;
            }

        }

        if (obj.gameObject.name == "Power_Up" && !gameWon)
        {
            obj.gameObject.SetActive(false);
            rb.velocity = new Vector3(0, Super_Jump * 2, 0);
            obj.gameObject.SetActive(true);
        }

        if (obj.gameObject.name == "Power_Down" && !gameWon)
        {
            Destroy(obj.gameObject);
            poison_time += 5f;
            Debug.Log("poison_time = " + poison_time.ToString());
        }


    }

}
