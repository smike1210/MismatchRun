/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 Controls volumne slider in main menu scene
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeControl : MonoBehaviour {
    //create variable for the main music 
	public AudioSource mainMusic;
    // create float for volume slider intance. 1.0 because that is wahts audiosources volume's are out of [0.0 - 1.0]
	public static float volume = 1.0f;
    // the volume slider
    public Slider audioSlider;

    // on start, set value to the float
    void Start()
    {
        audioSlider.value = volume;
    }

    // if the slider is changed, set the volume of the music object to that value
    void Update()
	{
        mainMusic = GameObject.FindGameObjectsWithTag("music")[0].GetComponent<AudioSource>();
		mainMusic.volume = volume;
	}
    //every time the slider is adjusted, it calls this function with the value it
    //it is changed to passed in as the parameter
	public void adjustVolume(float newVolume)
	{
		volume = newVolume;
	}
}
