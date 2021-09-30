using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour {

    // Reference to Audio Source component
    public static AudioSource audioSrc;


    // Music volume variable that will be modified
    // by dragging slider knob
    public static float musicVolume = 1f;

	// Use this for initialization
	void Start () {

        // Assign Audio Source component to control it
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        // Setting volume option of Audio Source to be equal to musicVolume
        audioSrc.volume = musicVolume;
        
	}

    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void volume_set(float vol)
    {
        musicVolume = vol;
    }
}