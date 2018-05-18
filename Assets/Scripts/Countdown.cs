/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 controls the countdown text at the start of gameplay
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {
    // sprites that hold the countdown text
	public Sprite one;
	public Sprite two;
	public Sprite three;
	public Sprite go;
	public GameObject ready;

	// Use this for initialization
	void Start () {
        // call dtsrting countdown function using StartCourtine function that is
        // part of unity library
		StartCoroutine(Example1());
	}

	// Update is called once per frame
	void Update () {
		
	}

    // displays number 3 for 1 second
	IEnumerator Example1()
	{
		gameObject.GetComponent<SpriteRenderer> ().sprite = three;
		yield return new WaitForSeconds(1);
		StartCoroutine(Example2());

	}

    // displays number 2 for 1 second
	IEnumerator Example2()
	{
		gameObject.GetComponent<SpriteRenderer> ().sprite = two;
		yield return new WaitForSeconds(1);   //Wait
		StartCoroutine(Example3());
	}
    // displays number 1 for 1 second
	IEnumerator Example3()
	{
		gameObject.GetComponent<SpriteRenderer> ().sprite = one;
		yield return new WaitForSeconds(1);   //Wait
		StartCoroutine(Example4());
	}

    // displays word "Go!" for .5 seconds
	IEnumerator Example4()
	{
		gameObject.GetComponent<SpriteRenderer> ().sprite = go;
		ready.GetComponent<SpriteRenderer> ().sprite = null;
		yield return new WaitForSeconds(.5f);   //Wait
		gameObject.GetComponent<SpriteRenderer> ().sprite = null;

	}
}
