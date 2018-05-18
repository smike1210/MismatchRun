/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 contains the functions needed for the main menu scene's button's actions
 for on-click events
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class menuCTRL : MonoBehaviour 
{
    //change scene to the scene with name passed in. 
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
        Time.timeScale = 1;
	}

    //When starting a game, don't simply load that scene, randomly decide if an 
    //add should show up or not (need that $$ son - jk I have no downloads :/  )
    public void startGame()
    {
        //check that they have purchased add free (will be 1 if true
        if(PlayerPrefs.GetInt("ads",0) == 0)
        {
            // 1/3 chance that an add plays. If so, play add then game scene,
            // otherwise just play the game
            if(Random.Range(0,2) == 0)
            {
                Advertisement.Show();
                SceneManager.LoadScene("Gameplay");
            }
            else{
                SceneManager.LoadScene("Gameplay");
            }
        }
        // if user has bought add free - just load game scene
        else{
            SceneManager.LoadScene("Gameplay");
        }


    }
   
}
