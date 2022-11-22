using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class pauseMenu : MonoBehaviour
{

    public bool gamePauseBool = false;
    public GameObject[] gamePause;
    public AudioSource[] allAudioSource;
    public AudioSource audio;
    private restart rs;

    IEnumerator PostPausePress(string scene)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.648485061", scene);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }

    IEnumerator PostResumePress(string scene)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.495179311", scene);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }

    IEnumerator PostQuitPress(string scene)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1732825903", scene);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }

    IEnumerator PostRestartPress(string scene)
    {
        string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfBJSg2NgGPIug2J2KGqGy-j4rRFrmqX-EXD9gmhO4Up2oP3A/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.2075030863", scene);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }

    // Start is called before the first frame update
    void Start()
    {
        rs = gameObject.GetComponent<restart>();
        // fader = GameObject.FindGameObjectWithTag("transition").GetComponent<RectTransform>();
        allAudioSource = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        gamePause = GameObject.FindGameObjectsWithTag("PauseGame");
        foreach (GameObject obj in gamePause)  {
            obj.SetActive(false);
        }
    }

    void SetActive()
    {
        foreach (GameObject obj in gamePause)  {
            obj.SetActive(true);
        }
        Time.timeScale = 0;
        gamePauseBool = true;
    }

    void SetDeactive()
    {
        foreach (GameObject obj in gamePause)  {
            obj.SetActive(false);
        }
        Time.timeScale = 1;
        gamePauseBool = false;
    }

    //pause btn
    public void pauseGame()
    {
        StartCoroutine(PostPausePress(SceneManager.GetActiveScene().name));
        foreach(AudioSource obj in allAudioSource) {
             obj.Pause();
        }
        SetActive();
    }

    //resume btn
    public void resumeGame()
    {
        StartCoroutine(PostResumePress(SceneManager.GetActiveScene().name));
        foreach(AudioSource obj in allAudioSource) {
            obj.UnPause();
        }
        SetDeactive();
    }

    //quit btn
    public void MainMenu()
    {
        
        rs.fader.gameObject.SetActive(true);
        LeanTween.alpha(rs.fader, 0, 0);
        LeanTween.alpha(rs.fader, 1, 0.5f).setIgnoreTimeScale(true).setOnComplete(() =>{
            StartCoroutine(PostQuitPress(SceneManager.GetActiveScene().name));
            SceneManager.LoadScene("Menu");
            SetDeactive();
        });
    }

    //restart btn
    public void RestartGame()
    {
        
        rs.fader.gameObject.SetActive(true);
        LeanTween.alpha(rs.fader, 0, 0);
        LeanTween.alpha(rs.fader, 1, 0.5f).setIgnoreTimeScale(true).setOnComplete(() =>{
            StartCoroutine(PostRestartPress(SceneManager.GetActiveScene().name));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SetDeactive();
        });
    }
}
