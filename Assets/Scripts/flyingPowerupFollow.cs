using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingPowerupFollow : MonoBehaviour
{
   public GameObject player;        //Public variable to store a reference to the player game object
   public bool poweredUp = false;
   public float offset = 0.0f;
   [SerializeField] float mergeSpeed =0.15f;

    // Use this for initialization
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        if(!poweredUp)
        {
            followPlayer();
        }
        else if(poweredUp)
        {
            wingsEquip();
        }
    }
    void followPlayer()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z -= 12f;
        playerPos.y += 15f;
        transform.position = Vector3.MoveTowards(transform.position , playerPos, 1f);
    }
    public void wingsEquip()
    {
        Vector3 playerPos =  player.transform.position;
        
        playerPos.y -= (3.5f+offset);
        playerPos.z -= (0.7f);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, mergeSpeed);
    }
   
}
