using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    public Text scoreText;

    private int numPowerUps;
    public Text powerUpCount;
    // Start is called before the first frame update
    void Start()
    {
        numPowerUps = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        powerUpCount.text = "Power: " + numPowerUps.ToString();
    }

    public void IncreaseGameScore(int ringScore)
    {
        score += ringScore;
        scoreText.text = score.ToString();
    }

    public void IncreasePowerUps(int powerup)
    {
        numPowerUps += powerup;
        powerUpCount.text = "Power: " + numPowerUps.ToString();
        Debug.Log(powerUpCount.text);
    }

    public int getNumPowerUps()
    {
        return numPowerUps;
    }

    public void resetNumPowerUps()
    {
        numPowerUps = 0;
        powerUpCount.text = "Power: " + numPowerUps.ToString();
        Debug.Log(powerUpCount.text);
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
