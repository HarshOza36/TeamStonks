using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Unity.VisualScripting;

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
    private pauseMenu pm;
    private levelLocking levelLock;
    private GameObject camera;

    public static GameObject Popup;

    //doodle jump
    public GameObject doodleJumpA;
    private doodleJump ddlJmpA;
    public GameObject doodleJumpB;
    private doodleJump ddlJmpB;
    public GameObject doodleJumpC;
    private doodleJump ddlJmpC;

    public float tutorialPopUpTime = 0.0f;

    


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
    public GameObject[] selectionPowerUpMenu;
    public GameObject selectionPowerInverse;
    [SerializeField] private GameObject prefabInv;
    [SerializeField] private Vector3 prefabInvPos;
    public GameObject clone;

    public Material matFace1;
    public Material matFace4;



    // Start is called before the first frame update
    void Start()
    {
        try {
        GameObject.FindGameObjectWithTag("bgMusic").GetComponent<bgMusic>().PlayMusic();
        }
        catch(Exception e) {
            Debug.Log(e);
        }
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        Physics.gravity = new Vector3(0, -9.8f, 0);
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;
        Star = GameObject.Find("RedStar");
        minus = GameObject.Find("minus").GetComponent<TMP_Text>();
        minus.text = "";
        selectionPowerUpMenu =  GameObject.FindGameObjectsWithTag("SelectionPowerUp");
        camera = GameObject.Find("Main Camera");
        levelLock = camera.GetComponent<levelLocking>();
        // selectionPowerInverse = GameObject.FindWithTag("spInverse");
        // selectionPowerInverse.SetActive(false);

        Popup = GameObject.Find("TutorialPopUp");
        
        foreach (GameObject obj in selectionPowerUpMenu)  {
            obj.SetActive(false);
        }
        ball_start_pos = this.transform.position.y;
        // ddlJmp = doodleJump.GetComponent<doodleJump>();
        if (sceneName == "Level2" || sceneName == "Level4" || sceneName == "Level6")
        {
            isTwoPuzzle = true;
        }else{	
            isTwoPuzzle = false;
        }
        if(sceneName == "Level4"){
            ddlJmpA = doodleJumpA.GetComponent<doodleJump>();
            ddlJmpB = doodleJumpB.GetComponent<doodleJump>();
            ddlJmpC = doodleJumpC.GetComponent<doodleJump>();
        }

        //Debug.Log(Physics.gravity);

        gameStart = GameObject.Find("GameStart").GetComponent<TMP_Text>();
        

        StartCoroutine(CountdownCoroutine());
        
        
        /// StartCoroutine(Post(sceneName));

        audioData = GetComponent<AudioSource>();
        //restart = GetComponent<restart>
        rb = GetComponent<Rigidbody>();
        timeRemaining = GetComponent<timer>();

        last = transform.position[1];

        pm = GetComponent<pauseMenu>();


    }

    IEnumerator CountdownCoroutine()
    {
        if (Popup != null)
        {
            yield return new WaitForSeconds(tutorialPopUpTime);
            Destroy(Popup);
        }

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
        Debug.Log("input value " + GameManager.IsInputEnabled());
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

    IEnumerator PostInversionCollected(string s1)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1120736174", s1);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();
    }


    // Update is called once per frame
    void Update()
    {
        if (!pm.gamePauseBool)
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
                            /// StartCoroutine(PostMPress(currentScene.name));
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
                        GameObject child = transform.GetChild(0).gameObject;
                        GameObject face = child.transform.GetChild(1).gameObject;
                        SkinnedMeshRenderer renderer = face.GetComponent<SkinnedMeshRenderer>();
                        Material color = renderer.materials[0];

                        if (poison_time <= 0)
                        {
                            //GetComponent<Renderer>().material.color = Color.red;
                            GameObject top = child.transform.GetChild(2).gameObject;
                            Material[] mats = new Material[] { color, matFace1 };
                            renderer.materials = mats;
                            top.SetActive(true);

                        }
                        else
                        {
                            //GetComponent<Renderer>().material.color = Color.black;
                            
                            Material[] mats = new Material[] { color, matFace4 };
                            renderer.materials = mats;

                        }
                    }
                    else
                    {   //If the ball is not able to jump: white
                        //GetComponent<Renderer>().material.color = Color.white;
                        GameObject child = transform.GetChild(0).gameObject;
                        GameObject top = child.transform.GetChild(2).gameObject;
                        top.SetActive(false);

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
                            /// StartCoroutine(PostSpacePress(currentScene.name));
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
                            /// StartCoroutine(PostSpacePress(currentScene.name));
                            if (!Inverse_Flag)
                            {
                                canDoubleJump = false;
                            }
                        }

                    }

                    

                    if(GameManager.IsInputEnabled() && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))){
                        /// StartCoroutine(PostAPress(currentScene.name));
                    }

                    if(GameManager.IsInputEnabled() && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))){
                        /// StartCoroutine(PostDPress(currentScene.name));
                        
                    }

                    if (GameManager.IsInputEnabled() && (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
                    {
                        OrbitLeft(true);
                    }
                    else if (GameManager.IsInputEnabled() && (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
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
                            /// StartCoroutine(PostPercentageCompleted(currentScene.name + "_" + "1"));
                        }else{
                            /// StartCoroutine(PostPercentageCompleted(currentScene.name + "_" + (bl / st).ToString()));
                        }
                        percentage_event_submitted = true;
                    }

                    rb.useGravity = false;
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
        } 
    }

    //Orbit movement
    void OrbitLeft(bool left)
    {
        if (left)
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
            int level = (int)Char.GetNumericValue(currentScene.name[currentScene.name.Length - 1]);
            if (level == 0) 
            {
                Debug.Log(levelLocking.level2Bool);
                levelLocking.level2Bool = true;
            }
            if (level == 1) 
            {
                Debug.Log(levelLocking.level2Bool);
                levelLocking.level3Bool = true;
            }
            if (level == 2) 
            {
                Debug.Log(levelLocking.level2Bool);
                levelLocking.level4Bool = true;
            }
            if (level == 3) 
            {
                Debug.Log(levelLocking.level2Bool);
                levelLocking.level5Bool = true;
            }
            if (level == 4) 
            {
                Debug.Log(levelLocking.level2Bool);
                levelLocking.level6Bool = true;
            }
            if (level == 6) 
            {
                Debug.Log(levelLocking.level2Bool);
                levelLocking.level7Bool = true;
            }
            /// StartCoroutine(PostEnd(currentScene.name));
            /// StartCoroutine(PostEndTime(currentScene.name + "_" + timeRemaining.timeRemaining.ToString()));
            // It is object B
        }

        // if (obj.gameObject.name == "spike" && !gameWon)
        // {

        //     //StartCoroutine(PostSpikeTouched(currentScene.name));
        //     if (timeRemaining.timeRemaining > 5)
        //     {
        //         Destroy(obj.gameObject);
                
        //         timeRemaining.timeRemaining -= 5;
        //         StartCoroutine(HealthCouroutine());
                
        //     }
        //     else
        //     {
        //         timeRemaining.timeRemaining = 0;
        //         gameWon = false;
        //     }

        // }

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
        if(currentScene.name == "Level4"){
            //--------------------------------------------------------for doodle jump
            if (ddlJmpA.hasDoodleJump || ddlJmpB.hasDoodleJump || ddlJmpC.hasDoodleJump)
            {
                Debug.Log("doodle collision on arc"+obj.collider.name);
                if (rb.position.y < obj.collider.transform.position.y && obj.gameObject.tag != "CenterCylinder")
                {
                    obj.collider.enabled = false;
                    transform.parent=obj.transform.parent.transform.Find("Untagged").transform;
                }

            }
        }


        // if (obj.gameObject.name == "Power_Up" && !gameWon)
        // {
        //     //obj.gameObject.SetActive(false);
        //     //StartCoroutine(PostBoosterCollected(currentScene.name));
        //     //Vector3 pos = this.transform.position;

        //     rb.velocity = new Vector3(0, Super_Jump * 2, 0);
        //     //obj.gameObject.SetActive(true);
        // }

        // if (obj.gameObject.name == "Power_Down" && !gameWon)
        // {
        //     //StartCoroutine(PostPoisonCollected(currentScene.name));
        //     Destroy(obj.gameObject);
        //     poison_time += 5f;
        //     //Debug.Log("poison_time = " + poison_time.ToString());
        // }
        if(obj.gameObject.tag == "MovingArc")
	    {
	        transform.parent  = obj.transform.parent.transform.Find("Empty_parent").transform;
	    }

        // For Inversion
        // if (obj.gameObject.name == "Inverse" && !gameWon)
        // {
        //     if (!Inverse_Flag) 
        //     {
        //         Physics.gravity = new Vector3(0, 7, 0);
        //         Inverse_Flag = !Inverse_Flag;
        //         canDoubleJump = false;
        //         Destroy(obj.gameObject);
        //     }   
        //     else
        //     {   
        //         Physics.gravity = new Vector3(0, -9.8f, 0);
        //         Inverse_Flag = !Inverse_Flag;
		// canDoubleJump = true;
        //         Destroy(obj.gameObject);
        //     }
        // }

    }
   
    IEnumerator HealthCouroutine()
    {
        minus.text = "-5";
        yield return new WaitForSeconds(1.0f);
        minus.text = "";
        yield return null;
    }

    IEnumerator SelectionPowerUpCour(){
        foreach (GameObject objj in selectionPowerUpMenu)  {
                objj.SetActive(true);
        }
        while(true){
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Debug.Log("INVERSION");
                clone = Instantiate(prefabInv, prefabInvPos, Quaternion.identity);
                clone.name = "Inverse";
                break;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                Debug.Log("Doodle Jump");
                doodleJumpC.SetActive(true);
                break;
            }
            yield return null;
        }
        foreach (GameObject objj in selectionPowerUpMenu)  {
                objj.SetActive(false);
        }
       
        yield return null;
    }

    void OnCollisionExit(Collision obj)
    {
        Scene currentScene = SceneManager.GetActiveScene();
	    if(obj.gameObject.tag == "MovingArc")
	    {
	        transform.parent = null;
	    }
        if(currentScene.name == "Level4"){
            if(obj.gameObject.tag == "Untagged")
            {
                transform.parent = null;
            }

            if (ddlJmpA.hasDoodleJump || ddlJmpB.hasDoodleJump || ddlJmpC.hasDoodleJump)
            {
                Debug.Log("exit doodle collision on arc");
                obj.collider.enabled = true;
                
            }
        }

    }

    private void OnTriggerEnter(Collider obj)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (obj.gameObject.name == "SelectionPower" && !gameWon)
        {   Destroy(obj.gameObject);
            StartCoroutine(SelectionPowerUpCour());
            
            
        }
        if (obj.gameObject.name == "Power_Up" && !gameWon)
        {
            Debug.Log("In here");
            //obj.gameObject.SetActive(false);
            /// StartCoroutine(PostBoosterCollected(currentScene.name));
            //Vector3 pos = this.transform.position;

            rb.velocity = new Vector3(0, Super_Jump * 2, 0);
            //obj.gameObject.SetActive(true);
        }

        if (obj.gameObject.name == "Power_Down" && !gameWon)
        {
            /// StartCoroutine(PostPoisonCollected(currentScene.name));
            Destroy(obj.gameObject);
            poison_time += 5f;
            //Debug.Log("poison_time = " + poison_time.ToString());
        }

        if (obj.gameObject.name == "spike" && !gameWon)
        {

            /// StartCoroutine(PostSpikeTouched(currentScene.name));
            if (timeRemaining.timeRemaining > 5)
            {
                Destroy(obj.gameObject);
                timeRemaining.timeRemaining -= 5;
                StartCoroutine(HealthCouroutine());
            }
            else
            {
                timeRemaining.timeRemaining = 0;
                gameWon = false;
            }

        }

        if (obj.gameObject.name == "Inverse" && !gameWon)
        {
            /// StartCoroutine(PostInversionCollected(currentScene.name));
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

    public void disableGravity(){
        rb.useGravity = false;
    }

    public void enableGravity()
    {
        rb.useGravity = true;
    }
}
