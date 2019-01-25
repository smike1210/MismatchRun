/* 
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 script to control game when paused
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class pauseScript : MonoBehaviour {
    // declares vars for all game objects that will be effected
    public Button pauseButton;
    public Sprite pauseSprite;
    public Sprite playSprite;
    public Button btn11;
    public Button btn12;
    public Button btn13;
    public Button btn21;
    public Button btn22;
    public Button btn23;
    public Button btn31;
    public Button btn32;
    public Button btn33;
    public Button btn41;
    public Button btn42;
    public Button btn43;
    public Button btn51;
    public Button btn52;
    public Button btn53;
    public Button btn61;
    public Button btn62;
    public Button btn63;
    public Button[] buttons;

    public GameObject chain1;
    public GameObject chain2;
    public GameObject chain3;
    public GameObject chain4;
    public GameObject chain5;
    public GameObject chain6;
    public GameObject[] chains;

    public GameObject FallingCat;

    public Button mainMenu;
    public Camera mainCamera;

    public void Start()
    {
        // find falling category object by tag;
        FallingCat = GameObject.FindWithTag("FallingCat");

        // initialize buttons array and gameobjects arrays when game starts
        buttons = new Button[] { btn11, btn12, btn13, btn21, btn22, btn23, btn31, btn32, btn33,btn41, btn42, btn43,btn51, btn52, btn53,btn61, btn62, btn63};
        chains = new GameObject[] { chain1, chain2, chain3, chain4, chain5, chain6, FallingCat };
    }

    public void pause()
    {

        // pause button is pressed when the game is going on, so pause game.
        // timescale is time change per frame
        if (Time.timeScale == 1)
        {
            // change pause sprite to play sprite 
            pauseButton.image.overrideSprite = playSprite;
            Time.timeScale = 0;
            // push all chains off main camera frame so user can't 
            // interact with them
            foreach(Button ele in buttons)
            {
                Vector3 left = new Vector3(-2000f, 0, 0);
                ele.interactable = false;
                ele.transform.position = ele.transform.position + left;

            }
            // push all buttons off main camera frame so user can't 
            // interact with them
            foreach(GameObject ele in chains)
            {
                Vector3 left = new Vector3(-2000f, 0, 0);
                ele.transform.position = ele.transform.position + left;
            }

            // add button to go back to main menu on game screen
            mainMenu.transform.position = mainCamera.transform.position + new Vector3(0, 0, 80f);

        }

        // pause button is pressed when game is paused. Unpause game
        else
        {
            pauseButton.image.overrideSprite = pauseSprite;
            // return chains back to normal playing positions
            foreach (Button ele in buttons)
            {
                Vector3 right = new Vector3(2000f, 0, 0);
                ele.interactable = true;
                ele.transform.position = ele.transform.position + right;
            }
            // returns buttton back to normal playing positions
            foreach (GameObject ele in chains)
            {
                Vector3 right = new Vector3(2000f, 0, 0);
                ele.transform.position = ele.transform.position + right;
            }

            // take main menu button of main screen
            mainMenu.transform.position = mainMenu.transform.position + new Vector3(-1000f, 0, 0);
            // set time back to normal
            Time.timeScale = 1;
        }
    }
}
