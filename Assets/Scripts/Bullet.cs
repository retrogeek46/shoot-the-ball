using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public int speed = 20;
    private float waitTime;                         //Time before bullet is destroyed after firing
    Rigidbody2D bullet;

	// Use this for initialization
	void Start () {
        waitTime = 0.5f;
        bullet = GetComponent<Rigidbody2D>();
        bullet.AddForce(transform.up * 20);
    }
	
	// Update is called once per frame
	void Update () {
        if (waitTime > 0) {
            waitTime -= Time.deltaTime;
        }
        if (waitTime <= 0) {
            Destroy(gameObject);
        }
    }

    //Check if bullet hit a ball
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ball") {
            FallingBall.ballLetter = other.gameObject.GetComponentInChildren<Text>().text;
            FallingBall.ballShot = true;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
