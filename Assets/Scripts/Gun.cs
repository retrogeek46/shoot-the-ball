using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Sprite[] gunSprite;
    public GameObject bullet;                                           //bullet prefab
    public Transform  firePoint;                                        //get transform of where to instantiate bullet
    float speed = 350f, zRotate;                                        //speed of gun rotation
    public static float noOfShots;
    private static int gunIndex;

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(transform.rotation.x ,transform.rotation.y, zRotate));

        //Reset gun postion
        if (Input.GetKey(KeyCode.A)){
            MoveGun(0);
        }
        else MoveGun(speed);

        //Shoot bullets if mouse is pressed, only 4 can exist at a time
        if (Input.GetMouseButtonDown(0) && !Score.GameOver) {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            noOfShots++;
        }
    }

    //Rotate and aim gun based on x position of mouse
    void MoveGun(float speed) {
        zRotate = -Input.GetAxis("Mouse X") * Time.deltaTime * speed;
    }
    
    //Change gun model if gun is clicked
    public void ChangeGun() {
        GetComponent<SpriteRenderer>().sprite = gunSprite[gunIndex % gunSprite.Length];         //%gunsprite.Length to remove array out of bounds error
        gunIndex++;
    }
}
