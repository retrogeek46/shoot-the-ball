using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Words word;
    public FallingBall ball;
    public GameObject panel;
    public Text scoreText, timesPlayedText, endText;
    public GameObject heartImg;
    public static bool GameOver;

    public GameObject[] life = new GameObject[lives];
    public static int lives = 3;

    public int score, timesPlayed;
    public float accuracy;
    public string message;

    void Start () {
        Time.timeScale = 1;
        GameOver = false;
        lives = 3;
        Gun.noOfShots = 0;
        if (PlayerPrefs.HasKey("Times Played")) {
            timesPlayed = PlayerPrefs.GetInt("Times Played");
        }
        timesPlayed++;
        PlayerPrefs.SetInt("Times Played", timesPlayed);
        DisplayStats();
        for (int i = 0; i < lives; i++) {
            life[i] = Instantiate(heartImg, new Vector3 (130 + (30*i), 245, 0), transform.rotation);
        }
    }
	
	void Update () {
        if (lives <= 0 || !Words.doCreate) {
            GameOver = true;
            EndText();
        }
    }

    //Function to check accuracy
    public void Accuracy() {
        accuracy = (((Gun.noOfShots) - (Gun.noOfShots - score)) / Gun.noOfShots)*100;
        accuracy = Mathf.Round(accuracy * 100f) / 100f;
    }

    //Function to check if letter shot is correct
    public void CheckLetter(string letter) {
        char tmp = word.q[FallingBall.noOfIterations - 1];
        if (FallingBall.ballLetter == tmp.ToString()) {
            score++;
            foreach (GameObject qCreated in GameObject.FindGameObjectsWithTag("Question")) {
                if (qCreated.GetComponent<Text>().text == FallingBall.ballLetter) {
                    Destroy(qCreated);
                }
            }
        }
        else if (FallingBall.ballLetter != tmp.ToString()) {
            DestroyLife();
        }
        DisplayStats();
    }

    //Display stats
    public void DisplayStats() {
        scoreText.text = "Score : " + score;
        timesPlayedText.text = "Times Played : " + timesPlayed;
    }

    //Deduct lives
    public void DestroyLife() {
        Destroy(life[lives - 1]);
        lives--;
    }

    //Text to be displayed at the end of game
    public void EndText() {
        Accuracy();
        Time.timeScale = 0;
        panel.SetActive(true);
        panel.transform.SetAsLastSibling();
        message = "Your Score is : " + score + "\nYour accuracy is : " + accuracy + "%";
        endText.text = message.Replace("\\n","\n");
    }
}
