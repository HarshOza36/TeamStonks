using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class tutorialMenu : MonoBehaviour
{
 public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
