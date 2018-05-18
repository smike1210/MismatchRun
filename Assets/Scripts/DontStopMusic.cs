/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 This code keeps music place when a new scene is loaded 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStopMusic : MonoBehaviour {

	void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("music");
        //destroy all main music objects except main one
		if (objs.Length > 1)
			Destroy (this.gameObject);
        // dont destroy main music game object when new scene is loaded
		DontDestroyOnLoad (this.gameObject);
	}

}
