/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 script to control the final score that is displayed on the game over scene
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class finalscore : MonoBehaviour {
    // vars to hold the previous game score and what the user's current highscore is.
    public Text finalScore;
    public Text highScore;
    public static int highscore;

	// Use this for initialization
	internal void Start () {
        highscore = PlayerPrefs.GetInt("highscore", StopperScript.gameScore);
        finalScore.text = StopperScript.gameScore + "";
        // check if last score is higher the current hight score, and update it 
        // if so
        if(StopperScript.gameScore >= highscore)
        {
            highscore = StopperScript.gameScore;
            PlayerPrefs.SetInt("highscore", highscore);
            PlayerPrefs.Save();
        }
        highScore.text = "" + highscore;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
