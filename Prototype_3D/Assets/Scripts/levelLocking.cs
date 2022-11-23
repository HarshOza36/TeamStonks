using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelLocking : MonoBehaviour
{
    public static bool level1Bool = true;
    public static bool level2Bool = false;
    public static bool level3Bool = false;
    public static bool level4Bool = false;
    public static bool level5Bool = false;
    public static bool level6Bool = false;
    public static bool level7Bool = false;
    Button level1;
    Button level2;
    Button level3;
    Button level4;
    Button level5;
    Button level6;
    Button level7;
    Button UnlockAll;
    Button Tutorial;

    private void Start() {
        level1 = GameObject.Find("Level1Button").GetComponent<Button>();
        level2 = GameObject.Find("Level2Button").GetComponent<Button>();
        level3 = GameObject.Find("Level3Button").GetComponent<Button>();
        level4 = GameObject.Find("Level4Button").GetComponent<Button>();
        level5 = GameObject.Find("Level5Button").GetComponent<Button>();
        level6 = GameObject.Find("Level6Button").GetComponent<Button>();
        level7 = GameObject.Find("Level7Button").GetComponent<Button>();
        Tutorial = GameObject.Find("Tutorials").GetComponent<Button>();
        UnlockAll = GameObject.Find("UnlockAll").GetComponent<Button>();
	UnlockAll.onClick.AddListener(TaskOnClick);	

        if(level2Bool)
        {
            level2.interactable = true;
        }
        else
        {
            level2.interactable = false;
        }
        if(level3Bool)
        {
            level3.interactable = true;
        }
        else
        {
            level3.interactable = false;
        }
        if(level4Bool)
        {
            level4.interactable = true;
        }
        else
        {
            level4.interactable = false;
        }
        if(level5Bool)
        {
            level5.interactable = true;
        }
        else
        {
            level5.interactable = false;
        }
        if(level6Bool)
        {
            level6.interactable = true;
        }
        else
        {
            level6.interactable = false;
        }
        if(level7Bool)
        {
            level7.interactable = true;
	    Tutorial.interactable = true;
        }
        else
        {
            level7.interactable = false;
	    Tutorial.interactable = false;
        }
        
    }
    
    private void Update() {	
        if(level2Bool)
        {
            level2.interactable = true;
        }
        else
        {
            level2.interactable = false;
        }
        if(level3Bool)
        {
            level3.interactable = true;
        }
        else
        {
            level3.interactable = false;
        }
        if(level4Bool)
        {
            level4.interactable = true;
        }
        else
        {
            level4.interactable = false;
        }
        if(level5Bool)
        {
            level5.interactable = true;
        }
        else
        {
            level5.interactable = false;
        }
        if(level6Bool)
        {
            level6.interactable = true;
        }
        else
        {
            level6.interactable = false;
        }
        if(level7Bool)
        {
            level7.interactable = true;
            Tutorial.interactable = true;
        }
        else
        {
            level7.interactable = false;
	    Tutorial.interactable = false;
        }
    }
	
    void TaskOnClick(){	
		level2Bool = true;
		level3Bool = true;
		level4Bool = true;
		level5Bool = true;
		level6Bool = true;
		level7Bool = true;
    }

}
