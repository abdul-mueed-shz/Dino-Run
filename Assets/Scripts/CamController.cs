using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamController : MonoBehaviour
{
    // public GameObject player;        //Public variable to store a reference to the player game object


    // private Vector3 offset;   
    
    // public  float zPos = 0f;         //Private variable to store the offset distance between the player and camera

    // // Use this for initialization
    // void Start () 
    // {
    //     offset = transform.position - player.transform.position;
    //     //Calculate and store the offset value by getting the distance between the player's position and camera's position.
    // }

    // // LateUpdate is called after Update each frame
    // void LateUpdate () 
    // {
    //     // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
    //     CamFollow();
    // }
    // void CamFollow()
    // {
    //     Vector3 playerPos = player.transform.position;
    //     playerPos.z -= zPos; 
    //     playerPos.y += zPos; 
    //     transform.position = playerPos + offset;
    // }


}
