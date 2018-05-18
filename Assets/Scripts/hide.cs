/* 
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 ignore file for now - may come in use later
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
