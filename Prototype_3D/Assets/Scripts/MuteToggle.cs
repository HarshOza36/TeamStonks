using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
public class MuteToggle : MonoBehaviour
{   
    Toggle myToggle;

    // Start is called before the first frame update
    void Start()
    {
        myToggle = GetComponent<Toggle>();

        if(AudioListener.volume == 0){
            myToggle.isOn = false;
        }
        
    }

    public void ToggleAudioOnValueChange(bool audioIn)
    {
        Debug.Log(audioIn);
        if(audioIn)
        {
            AudioListener.volume=1;
            Debug.Log(GameObject.Find("Background"));
            Debug.Log(GameObject.Find("Checkmark"));
            //GameObject.Find("Background").SetActive(false);
            //GameObject.Find("Checkmark").SetActive(true);
        }
        else
        {
            Debug.Log(GameObject.Find("Background"));
            Debug.Log(GameObject.Find("Checkmark"));
            AudioListener.volume=0;
            //GameObject.Find("Background").SetActive(true);
            //GameObject.Find("Checkmark").SetActive(false);
        }
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        
    }
}
