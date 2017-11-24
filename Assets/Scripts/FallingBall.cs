using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingBall : MonoBehaviour
{
    public GameObject Ball;                                 //Ball prefab
    public GameObject ball1, ball2, ball3;                  //Instantiated balls
    public static int noOfIterations = 0;                   //Counter to keep track of how many times balls have been created
    public Words word;                                      //Instance of Word class to call word shuffle methods
    public static string ballLetter;                        //Letter shown on ball
    public static bool ballShot;                            //Bool to check if a ball has been shot
    public Score score;

    private char[] letter = new char[3];                    //Letters to put in ball 1, 2 and 3
    private decimal moveSpeed = 30;                         //Speed of balls
    private bool ballExists, isRunning = false;

    void Start() {
        noOfIterations = 0;
        ballExists = false;
    }

    void Update() {
        //Create balls if all three balls have been destroyed and lives is not zero
        if (!ballExists && !Score.GameOver && noOfIterations <= word.q.Length && Words.doCreate) {
            ballExists = true;
            if (!isRunning) {
                StartCoroutine(DelayBall());
            }
        }

        //If balls exist, count how many. If zero, change ballExist bool to false
        if (ballExists) {
            CheckPos();
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            if (balls.Length == 0)
                ballExists = false;
            score.DisplayStats();
        }

        //If a ball has been shot
        if (ballShot) {
            score.CheckLetter(ballLetter);
            ballShot = false;
        }
    }

    //Function to create ball and put text, called by coroutine
    public GameObject CreateBall(GameObject ball, int xPos, char a) {
        ball = Instantiate(Ball, new Vector3(xPos, 230, transform.position.z), transform.rotation);
        ball.transform.SetParent(GameObject.Find("Canvas").transform, false);
        ball.GetComponentInChildren<Text>().text = "" + a;
        MoveBall(ball);
        return ball;
    }

    //Function to move balls with random velocities
    public void MoveBall(GameObject ball) {
        Rigidbody2D ball2D = ball.GetComponent<Rigidbody2D>();
        decimal r = (decimal)UnityEngine.Random.Range(0.5f, 2f);
        ball2D.velocity = new Vector2(ball2D.velocity.x, -(float)decimal.Multiply(moveSpeed, r));
    }

    //Check if balls are below line, then destroy it. If correct ball is missed, deduct life
    public void CheckPos() {
        if (ball1 != null && ball1.transform.position.y <= 75) {
            if (CheckCorrectBall(ball1))
                score.DestroyLife();
            Destroy(ball1);
        }
        if (ball2 != null && ball2.transform.position.y <= 75) {
            if (CheckCorrectBall(ball2))
                score.DestroyLife();
            Destroy(ball2);
        }
        if (ball3 != null && ball3.transform.position.y <= 75) {
            if (CheckCorrectBall(ball3))
                score.DestroyLife();
            Destroy(ball3);
        }
    }

    //Check if correct ball has ben missed, if true, deduct one life
    private bool CheckCorrectBall(GameObject gameObject) {
        if (gameObject.GetComponentInChildren<Text>().text == word.q[noOfIterations - 1].ToString())
            return true;
        else return false;
    }

    //Coroutine to create balls with a short delay
    public IEnumerator DelayBall() {
        isRunning = true;
        yield return new WaitForSeconds(0.5f);

        //Randomly put correct word in one ball
        int r = UnityEngine.Random.Range(0, 3);
        if (noOfIterations < word.q.Length && Words.doCreate) {
            switch (r) {
                case 0:
                    letter[0] = word.q[noOfIterations];
                    letter[1] = word.a[noOfIterations];
                    letter[2] = word.b[noOfIterations];
                    break;
                case 1:
                    letter[0] = word.a[noOfIterations];
                    letter[1] = word.q[noOfIterations];
                    letter[2] = word.b[noOfIterations];
                    break;
                case 2:
                    letter[0] = word.b[noOfIterations];
                    letter[1] = word.a[noOfIterations];
                    letter[2] = word.q[noOfIterations];
                    break;
            }
            ball1 = CreateBall(ball1, -150, letter[0]);
            ball2 = CreateBall(ball2, 0, letter[1]);
            ball3 = CreateBall(ball3, 150, letter[2]);
        }
        noOfIterations++;
        yield return new WaitForSeconds(0.5f);
        isRunning = false;
    }
}
