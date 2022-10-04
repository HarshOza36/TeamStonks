using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuNav : MonoBehaviour
{
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void Mute() {
        if(AudioListener.pause){
            AudioListener.pause = false;
        } else {
            AudioListener.pause = true;
        }
    }
}
