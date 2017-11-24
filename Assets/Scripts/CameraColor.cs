using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour {

    Camera cam;

    //Change camera color, hence background color, based on button pressed in Main Menu 
	void Awake () {
        cam = GetComponent<Camera>();
        switch (LevelManager.bgColor) {
            case "Red"  : cam.backgroundColor = new Color32(223, 158, 158, 1); break;
            case "Green": cam.backgroundColor = new Color32(158, 223, 158, 1); break;
            case "Blue" : cam.backgroundColor = new Color32(100, 110, 255, 1); break;
        }
	}
}
