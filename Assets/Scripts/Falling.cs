/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 controls each falling chain during gameplay
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class Falling : MonoBehaviour {

    // these sprites let the chain know when it is at the bottom.
    // When it is, the stop redCircle sprite will have the sprite rendered of
    // stop.
    public SpriteRenderer stop;
    public Sprite redCircle;


    float speed;

    void Start()
    {
        // speed of the falling chains. Can be edited later to increase/decrease
        // difficulty
        speed = 1.3f;
    }

    // Update is called once per frame. If it is at bottom, do not go down. 
    // This event is not handled here. If it is not at bottom, go down
    void FixedUpdate()
    {
        if(stop.sprite != redCircle)
        {
            godown(); 
        }


    }

    void godown()
    {
        // make chain go down at set speed by changing posittion.
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
