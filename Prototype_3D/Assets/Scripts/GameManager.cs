using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static bool input=true;
    public static GameManager singleton;
    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    public static bool IsInputEnabled(){
        return input;
    }

    public static void EnableInput(){
        Debug.Log("Enabling Input");
        input = true;
    }

    public static void DisableInput()
    {
        Debug.Log("Disabling Input");
        input = false;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    
}
