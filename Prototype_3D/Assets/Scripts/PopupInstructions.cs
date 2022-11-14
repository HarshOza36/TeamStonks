using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInstructions : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject uiObject;
    public bool pause;
    void Start()
    {
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    { 
        if (Time.timeScale == 0) { 
            Time.timeScale = 1; 
            Destroy(uiObject);
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter (Collider player) {
        Debug.Log("OnTrigger from popup fired");
        if (player.gameObject.tag == "Player")
        {   
            if(pause != true){	   
            	uiObject.SetActive(true);
            	StartCoroutine("WaitForSec");
            }else{
		        PauseGame();
	        }
        }
    }

    public void PauseGame(){
	    uiObject.SetActive(true);
	    Time.timeScale = 0;	
    }

    IEnumerator WaitForSec(){
        yield return new WaitForSeconds(8);
        Destroy(uiObject);
        Destroy(gameObject);
    }
}
