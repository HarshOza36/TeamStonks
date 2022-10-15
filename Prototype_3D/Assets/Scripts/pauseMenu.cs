using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour
{
    public bool gamePauseBool = false;
    public GameObject[] gamePause;
    public AudioSource[] allAudioSource;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
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

    public void pauseGame()
    {
        foreach(AudioSource obj in allAudioSource) {
             obj.Pause();
        }
        SetActive();
    }
    public void resumeGame()
    {
        foreach(AudioSource obj in allAudioSource) {
            obj.UnPause();
        }
        SetDeactive();
    }
    public void MainMenu()
    {
        SetDeactive();
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame()
    {
        SetDeactive();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
