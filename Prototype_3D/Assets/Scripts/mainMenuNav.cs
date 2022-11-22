using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class mainMenuNav : MonoBehaviour
{
    private RectTransform fader;
    IEnumerator PostMenuPress(string scene)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1832371320", scene);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }
    // private void Awake() {
        
    //     fader.anchorMin = Vector2.zero;
    //     fader.anchorMax = Vector2.one;
    //     fader.sizeDelta = Vector2.zero;
    // }

    private void Start() {
        fader = GameObject.FindGameObjectWithTag("transition").GetComponent<RectTransform>();
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 1, 0);
        LeanTween.alpha(fader, 0, 0.5f).setOnComplete(() =>{
            fader.gameObject.SetActive(false);
        });
    }

    public void LoadScene(string sceneName){
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>{
            StartCoroutine(PostMenuPress(sceneName));
            SceneManager.LoadScene(sceneName);
            // fader.gameObject.SetActive(false);
        });
        
    }

    public void Mute() {
        if(AudioListener.pause){
            AudioListener.pause = false;
        } else {
            AudioListener.pause = true;
        }
    }
}
