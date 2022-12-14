using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(AudioSource))]

public class timer : MonoBehaviour
{
    private Text timerText;
    private GameObject frontObject;
    public static TMP_Text timeText;
    public float timeRemaining = 100;
    public static bool timerIsRunning = false;
    public GameObject[] gameOver;
    public GameObject[] gameWon;
    public ballController ballContr;
    AudioSource timeFinishingAudio;
    // Start is called before the first frame update
    void Start()
    {  
        ballContr = GetComponent<ballController>();
        gameOver =  GameObject.FindGameObjectsWithTag("gameOver");
        gameWon = GameObject.FindGameObjectsWithTag("gameWon");
        frontObject = GameObject.Find("Timer");
        foreach (GameObject obj in gameOver)  {
            obj.SetActive(false);
        }
        foreach (GameObject obj in gameWon)  {
            obj.SetActive(false);
        }

        timeText = frontObject.GetComponent<TMP_Text>();
        timeFinishingAudio = frontObject.GetComponent<AudioSource>();
        // Debug.Log(timeText.text);
        StartCoroutine(WaitCoroutine());
        // Debug.Log(frontObject.tag);
        //frontObject = GameObject.FindGameObjectWithTag("timer");
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(timeText);
        //frontObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        if(ballContr.gameStartBool){
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    // Debug.Log(timeRemaining);
                    DisplayTime(timeRemaining);
                    if ((timeRemaining > 3f || ballContr.gameWon == true) && timeFinishingAudio.isPlaying) {
                        timeFinishingAudio.Stop();
                    }
                    if (timeRemaining <= 3f && !timeFinishingAudio.isPlaying && !ballContr.gameWon) {
                        // Debug.Log("3 seconds");
                        timeFinishingAudio.Play();
                    }
                    if (ballContr.gameWon) {
                        timerIsRunning = false;
                        GameObject.Find("PauseGame").SetActive(false);
                        foreach (GameObject obj in gameWon)  {
                            obj.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (timeFinishingAudio.isPlaying) {
                        timeFinishingAudio.Stop();
                    }
                    // Debug.Log("Time has run out!");
                    // Debug.Log(timeText.text);
                    timeRemaining = 0;
                    timerIsRunning = false;
                    foreach (GameObject obj in gameOver)  {
                        obj.SetActive(true);
                    }
                    GameObject.Find("PauseGame").SetActive(false);
                }
            }
        }
    }
    public static void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (minutes < 0 || seconds <0){
            minutes = 0;
            seconds = 0;
        } 

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator WaitCoroutine(){
        yield return new WaitForSeconds(ballContr.tutorialPopUpTime);
        timeFinishingAudio.Play(0);
    }
    
}
