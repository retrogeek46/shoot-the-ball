using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {

    public Words word;
    public Score score;
    private string question;
    public GameObject earth, gear, burn;

    //Change lives image if player clicks on current icon based on current question
    public void SetLiveIcon() {
        question = word.CurrentQuestion();
        if (question == "EARTH") {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("LifeIcon")) {
                Destroy(go);
            }
            for (int i = 0; i < Score.lives; i++) {
                score.life[i] = Instantiate(earth, new Vector3(130 + (30 * i), 245, 0), transform.rotation);
            }
        }
        if (question == "GEAR") {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("LifeIcon")) {
                Destroy(go);
            }
            for (int i = 0; i < Score.lives; i++) {
                score.life[i] = Instantiate(gear, new Vector3(130 + (30 * i), 245, 0), transform.rotation);
            }
        }
        if (question == "BURN") {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("LifeIcon")) {
                Destroy(go);
            }
            for (int i = 0; i < Score.lives; i++) {
                score.life[i] = Instantiate(burn, new Vector3(130 + (30 * i), 245, 0), transform.rotation);
            }
        }
    }
}
