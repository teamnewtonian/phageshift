﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object


    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start() {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate() {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;

        if (Input.GetKey(KeyCode.Z)) //Rotates right when Z is pressed
        {
            transform.RotateAround(player.transform.position, Vector3.up, -50 * Time.deltaTime);
            offset = transform.position - player.transform.position;
        }
        if (Input.GetKey(KeyCode.X)) //Rotates left when X is pressed
        {
            transform.RotateAround(player.transform.position, Vector3.up, 50 * Time.deltaTime);
            offset = transform.position - player.transform.position;
        }

    }

}
