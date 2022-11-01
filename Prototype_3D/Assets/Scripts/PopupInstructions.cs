using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInstructions : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject uiObject;
    void Start()
    {
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider player) {
        
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    IEnumerator WaitForSec(){
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        Destroy(gameObject);
    }
}
