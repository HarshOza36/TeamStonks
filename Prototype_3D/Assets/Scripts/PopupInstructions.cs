using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInstructions : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject uiObject;
    public ballController ball;
    public float popUpSeconds=0.0f;
    public bool coll;
    // public bool pause;
    void Start()
    {
        coll = false;
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    { 
        if (Time.timeScale == 0) { 
            Debug.Log("rti");
            if(coll);
            {   Debug.Log(uiObject);
                Destroy(uiObject);
                Destroy(gameObject);
            }
        } 
    }

    void OnTriggerEnter (Collider player) {
        Debug.Log("OnTrigger from popup fired");
        if (player.gameObject.tag == "Player")
        {
            StartCoroutine("PauseGame");
            coll = true;
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }

        
    }



    IEnumerator PauseGame(){
        uiObject.SetActive(true);
        //Time.timeScale = 0;
        ball.disableGravity();
        timer.timerIsRunning = false;
        GameManager.DisableInput();
        yield return new WaitForSeconds(popUpSeconds);
        //Time.timeScale = 1;
        ball.enableGravity();
        timer.timerIsRunning = true;
        GameManager.EnableInput();
    }

    IEnumerator WaitForSec(){
        yield return new WaitForSeconds(8);
        coll = false;
        Destroy(uiObject);
        Destroy(gameObject);
    }
}
