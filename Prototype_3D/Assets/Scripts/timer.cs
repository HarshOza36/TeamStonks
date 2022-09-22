using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(AudioSource))]

public class timer : MonoBehaviour
{
    //public Text timerText;
    public GameObject frontObject;
    public static TMP_Text timeText;
    public float timeRemaining = 100;
    public bool timerIsRunning = false;
    public GameObject[] gameOver;
    public ballController ballContr;
    AudioSource timeFinishingAudio;
    // Start is called before the first frame update
    void Start()
    {  
        ballContr = GetComponent<ballController>();
        gameOver =  GameObject.FindGameObjectsWithTag("gameOver");
        frontObject = GameObject.Find("Timer");
        foreach (GameObject obj in gameOver)  {
            obj.SetActive(false);
        }

        timeText = frontObject.GetComponent<TMP_Text>();
        timeFinishingAudio = frontObject.GetComponent<AudioSource>();
        // Debug.Log(timeText.text);
        timeFinishingAudio.Play(0);
        // Debug.Log(frontObject.tag);
        //frontObject = GameObject.FindGameObjectWithTag("timer");
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(timeText);
        //frontObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                // Debug.Log(timeRemaining);
                DisplayTime(timeRemaining);
                if (timeRemaining > 3f && timeFinishingAudio.isPlaying) {
                    timeFinishingAudio.Stop();
                }
                if (timeRemaining <= 3f && !timeFinishingAudio.isPlaying) {
                    // Debug.Log("3 seconds");
                    timeFinishingAudio.Play(0);
                }
                if (ballContr.gameWon) {
                    timerIsRunning = false;
                    foreach (GameObject obj in gameOver)  {
                        if(obj.name == "GameOverText") {
                            obj.GetComponent<TMP_Text>().text = "Game Won!";
                        }
                        obj.SetActive(true);
                    }
                }
            }
            else
            {
                // Debug.Log("Time has run out!");
                // Debug.Log(timeText.text);
                timeRemaining = 0;
                timerIsRunning = false;
                foreach (GameObject obj in gameOver)  {
                    obj.SetActive(true);
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
}
