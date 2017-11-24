using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static string bgColor;
    public string buttonName;

    //Function to change scenes and pass which button was pressed
    public void OnPress(string btnName) {
        switch (btnName) {
            case "Red": bgColor = "Red"; break;
            case "Green": bgColor = "Green"; break;
            case "Blue": bgColor = "Blue"; break;
        }
        SceneManager.LoadScene("Game Round");
    }

    public void FromGame() {
        SceneManager.LoadScene("Main Menu");
    }
}
