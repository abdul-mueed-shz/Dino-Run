using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossStart : MonoBehaviour
{
    public Animator TA;
    void OnTriggerEnter(Collider bc)
    {
        
        
            TA.SetBool("BS", true);
            SetVolume.audioSrc.Stop();
            DinoMovement.bossMusic.Play();
            
        
    }
}
