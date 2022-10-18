using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class vanishingPlatform : MonoBehaviour
{
    public bool flag = true;
    float alphaVal;
    public float timeRemaining;
    public GameObject[] VanishPlatform;

    // Start is called before the first frame update
    void Start()
    {
        VanishPlatform = GameObject.FindGameObjectsWithTag("VanishArc");

        timeRemaining = 3;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!flag){
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0){
                Debug.Log("Flag false");
                Debug.Log(VanishPlatform);
                foreach (GameObject obj in VanishPlatform){
                    obj.SetActive(false);
                }
                // VanishPlatform.GetComponent<Renderer>().enabled = false;

                flag = !flag;
                timeRemaining = 3;
            }

        }
        else{
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0){
                Debug.Log("Flag true");
                Debug.Log(VanishPlatform);
                foreach (GameObject obj in VanishPlatform){
                    obj.SetActive(true);
                }
                // VanishPlatform.GetComponent<Renderer>().enabled = true;
                flag = !flag;
                timeRemaining = 3;
            }
        }

        
    }
}
