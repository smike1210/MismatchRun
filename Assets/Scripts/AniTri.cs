/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 Controls the traingles that move around on the main start screen
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTri : MonoBehaviour {
    // rigid body object that will be assocaited with traingle
	public Rigidbody2D rb1;
    // sprite objects for each color sprite I have made
	public Sprite TLB;
	public Sprite TLBL;
	public Sprite TLG;
	public Sprite TLGR;
	public Sprite TLO;
	public Sprite TLP;
	public Sprite TLR;
	public Sprite TLW;
	public Sprite TLY;
	// Use this for initialization
	void Start () {
		Sprite[] sprites = new Sprite[] { TLB, TLBL, TLG, TLGR, TLO, TLP, TLR, TLW, TLY};
        // use initial force of rigid body to make it move at the very start
        rb1.AddForce (new Vector2(Random.Range(-6,6),Random.Range(-6,6)), ForceMode2D.Impulse);
        // assign random sprite from sprites array to game object
        gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Random.Range (0, 8)];
        // give object random size
        float scale = Random.Range (2, 12);
        // scale the object based on random number from above
		transform.localScale += new Vector3 (scale, scale, scale);

	}
	
    // Update is called once per frame, makes sure that none of the objects are 
    // moving to fast (which causes a lot of lag). Set velocity to 0 if this 
    // happens. The object will move again because it will eventually be hit.
	void Update () {
		if (rb1.velocity.magnitude > 10) 
		{
			rb1.velocity = new Vector2 (0, 0);
		}
		
		
	}
}
