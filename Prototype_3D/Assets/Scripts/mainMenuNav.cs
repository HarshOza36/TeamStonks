using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class mainMenuNav : MonoBehaviour
{
    IEnumerator PostMenuPress(string scene)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1832371320", scene);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }

    public void LoadScene(string sceneName){
        StartCoroutine(PostMenuPress(sceneName));
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
