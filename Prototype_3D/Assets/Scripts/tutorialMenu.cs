using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class tutorialMenu : MonoBehaviour
{
    public RectTransform fader;
    private bool transition;
    private void Start() {
        fader = GameObject.FindGameObjectWithTag("transition").GetComponent<RectTransform>();
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 1, 0);
        transition = true;
    }
    private void Update() {
        if (transition) {
            transition = false;
            LeanTween.alpha(fader, 0, 1f).setOnComplete(() =>{
            fader.gameObject.SetActive(false);
        });
        }
    }
    public void LoadScene(string sceneName){
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>{
            SceneManager.LoadScene(sceneName);
        });
    }
}
