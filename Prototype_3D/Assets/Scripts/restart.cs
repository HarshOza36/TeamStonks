using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    
    public RectTransform fader;
    private bool transition;
    private void Start() {
        fader = GameObject.FindGameObjectWithTag("transition").GetComponent<RectTransform>();
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 1, 0);
        transition = true;
        // LeanTween.alpha(fader, 0, 1f).setOnComplete(() =>{
        //     fader.gameObject.SetActive(false);
        // });
    }
    private void Update() {
        if (transition) {
            transition = false;
            LeanTween.alpha(fader, 0, 1f).setOnComplete(() =>{
            fader.gameObject.SetActive(false);
        });
        }
    }
    public void RestartGame() {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>{
            Debug.Log("Inside restartFunc");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        }  
    public void NextLevel() {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>{
            int y = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("Inside nextLevelFunc");
            SceneManager.LoadScene(y+1); // loads current scene
        });
    }
    public void MainMenu() {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>{
            Debug.Log("Inside nextLevelFunc");
            SceneManager.LoadScene("Menu"); // loads current scene
        });
    }
}
