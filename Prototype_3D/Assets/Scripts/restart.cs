using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public void RestartGame() {
            Debug.Log("Inside restartFunc");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
         }  
    public void NextLevel() {
        int y = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Inside nextLevelFunc");
        SceneManager.LoadScene(y+1); // loads current scene
    }
    public void MainMenu() {
        Debug.Log("Inside nextLevelFunc");
        SceneManager.LoadScene("Menu"); // loads current scene
    }
}
