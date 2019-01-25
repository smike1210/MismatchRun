/*
 By    : Michael Shea
 email : mjshea3@illinois.edu
 phone : 708 - 203 - 8272
 Description: 
 Main game flow script
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Button))]
public class StopperScript : MonoBehaviour {
    // image and animation used to fade out when game over
    public Image Fade;
    public Animator animFade;
    public Camera mainCamera;

    // int and object to hold current game score
    public static int gameScore=0;
    public Text gameScoreObject;

    // bool used for control flow - more obvious later down
    public bool started = false;

    //declarations of all game objects and sprites
    public SpriteRenderer countDown;
    public Sprite go;
    public SpriteRenderer mainCatImage;
    public AudioSource mainCatSound;
    public SpriteRenderer stop;
    public Sprite redCircle;

    //six chains will be used for the game:
    public GameObject chain1;
    public Button btn11;
    public Button btn12;
    public Button btn13;

    public GameObject chain2;
    public Button btn21;
    public Button btn22;
    public Button btn23;

    public GameObject chain3;
    public Button btn31;
    public Button btn32;
    public Button btn33;

    public GameObject chain4;
    public Button btn41;
    public Button btn42;
    public Button btn43;

    public GameObject chain5;
    public Button btn51;
    public Button btn52;
    public Button btn53;

    public GameObject chain6;
    public Button btn61;
    public Button btn62;
    public Button btn63;

    // gameobject that holds the falling category change announcement
    public GameObject FallingCat;

    // images used to display the categories
    public Sprite colorImage;
    public Sprite sizeImage;
    public Sprite shapeImage;

    // audio clips used to announce new categories
    public AudioClip colorSound;
    public AudioClip sizeSound;
    public AudioClip shapeSound;


    // helper vars to place game objects during countdown
    public Vector3 chainHomePos;
    public GameObject chainStarter1;
    public GameObject chainStarter2;
    public GameObject chainStarter3;
    public GameObject chainStarter4;
    public GameObject chainStarter5;
    public GameObject chainStarter6;
    public GameObject catStarter;
    public GameObject fallingCatStarter;

    // arrays to hold sprites and sounds
    public Sprite[] catImages;
    public AudioClip[] catSounds;
    public Sprite[,] allShapes;

    // hold the sizes of the scaled objects
    public float[] scales; 

    //ints that will hold values of certain game objects states
    public int tilSwitch = 0;
    public int count = 0;
    public int catType = 0;

    //vars to allow for gap to appear in chains when category changes
    public int number_paused;
    public int how_many_to_pause = 6;
    public int cur_pausing;
    public volatile bool pausing;
    public bool bar_at_bottom;

    //vectors that hold the initial chain positions during countdown
    public Vector3 chain1Start;
    public Vector3 chain2Start;
    public Vector3 chain3Start;
    public Vector3 chain4Start;
    public Vector3 chain5Start;
    public Vector3 chain6Start;
    public Vector3 fallingCatStart;

    //all the sprite variables for each shape and color
    public Sprite CLEnd;
    public Sprite CLGood;
    public Sprite CLB;
    public Sprite CLBL;
    public Sprite CLG;
    public Sprite CLGR;
    public Sprite CLO;
    public Sprite CLP;
    public Sprite CLR;
    public Sprite CLW;
    public Sprite CLY;
    public Sprite PLEnd;
    public Sprite PLGood;
    public Sprite PLB;
    public Sprite PLBL;
    public Sprite PLG;
    public Sprite PLGR;
    public Sprite PLO;
    public Sprite PLP;
    public Sprite PLR;
    public Sprite PLW;
    public Sprite PLY;
    public Sprite SLEnd;
    public Sprite SLGood;
    public Sprite SLB;
    public Sprite SLBL;
    public Sprite SLG;
    public Sprite SLGR;
    public Sprite SLO;
    public Sprite SLP;
    public Sprite SLR;
    public Sprite SLW;
    public Sprite SLY;
    public Sprite RLEnd;
    public Sprite RLGood;
    public Sprite RLB;
    public Sprite RLBL;
    public Sprite RLG;
    public Sprite RLGR;
    public Sprite RLO;
    public Sprite RLP;
    public Sprite RLR;
    public Sprite RLW;
    public Sprite RLY;
    public Sprite TLEnd;
    public Sprite TLGood;
    public Sprite TLB;
    public Sprite TLBL;
    public Sprite TLG;
    public Sprite TLGR;
    public Sprite TLO;
    public Sprite TLP;
    public Sprite TLR;
    public Sprite TLW;
    public Sprite TLY;

    // ints to hold which item on each chain is the correct one (messed up
    // on the nameing scheme - will update it at a later date)
    public int chain1wrong;
    public int chain2wrong;
    public int chain3wrong;
    public int chain4wrong;
    public int chain5wrong;
    public int chain6wrong;

    // sprites for the two differerent colored chains
    public Sprite redChain;
    public Sprite blackChain;

    // arrays to hold the buttons of each chain
    public Button[] chain1buts;
    public Button[] chain2buts;
    public Button[] chain3buts;
    public Button[] chain4buts;
    public Button[] chain5buts;
    public Button[] chain6buts;
    public int[,] butShapes;

    // array of chain game objects
    public GameObject[] chains;


    // int used for control flow at start to make sure chains do not come down
    // early
    public int replay;

    void Start()
    {
        // set pausing to true so the falling category will fall initially.
        pausing = true;
        cur_pausing = -1;
        bar_at_bottom = false;

        // find falling category object by tag;
        FallingCat = GameObject.FindWithTag("FallingCat");
        // find falling category start object by tag;
        fallingCatStarter = GameObject.FindWithTag("FallingCatStart");

        // create new buttons for each chain
        chain1buts = new Button[] { btn11, btn12, btn13 };
        chain2buts = new Button[] { btn21, btn22, btn23 };
        chain3buts = new Button[] { btn31, btn32, btn33 };
        chain4buts = new Button[] { btn41, btn42, btn43 };
        chain5buts = new Button[] { btn51, btn52, btn53 };
        chain6buts = new Button[] { btn61, btn62, btn63 };

        // init chains array
        chains = new GameObject[] { chain1, chain2, chain3, chain4, chain5, chain6};


        // shapes of all buttons for now null (0)
        butShapes = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        // init game score
        gameScore = 0;

        //the different sizes of each shape. Chosen by trial and error to see what good sizes are
        scales = new float[] { .0016f, .003f };
        // 2d srrsy - rows are the shape, colms are the diff colors
        allShapes = new Sprite[,] {{ CLEnd, CLGood, CLB, CLBL, CLG, CLGR, CLO, CLP, CLR, CLW, CLY },
        { PLEnd, PLGood, PLB, PLBL, PLG, PLGR, PLO, PLP, PLR, PLW, PLY },
        { SLEnd, SLGood, SLB, SLBL, SLG, SLGR, SLO, SLP, SLR, SLW, SLY },
        { RLEnd, RLGood, RLB, RLBL, RLG, RLGR, RLO, RLP, RLR, RLW, RLY },
        { TLEnd, TLGood, TLB, TLBL, TLG, TLGR, TLO, TLP, TLR, TLW, TLY }};

        //init replay
        replay = 0;
        //image array for category images
        catImages = new Sprite[] {colorImage, sizeImage, shapeImage};
        //audio array for category sounds
        catSounds = new AudioClip[] { colorSound, sizeSound, shapeSound };

        //initial category
        catType = Random.Range(0, 3);
        //determines amount of chains until the first switch
        tilSwitch = Random.Range(1, 5);

        //set initial chain positions
        chain1Start = chainStarter1.transform.position;
        chain2Start = chainStarter2.transform.position;
        chain3Start = chainStarter3.transform.position;
        chain4Start = chainStarter4.transform.position;
        chain5Start = chainStarter5.transform.position;
        chain6Start = chainStarter6.transform.position;
        fallingCatStart = fallingCatStarter.transform.position;

        // this is the position chains will reset to to when they get to the bottom
        // and the user had selected the item correctly
        chainHomePos = fallingCatStarter.transform.position;
    }
    void Update()
    {
        // if the countdown is over and rep;ay is 0, meaning we are in initial starting
        // phase, start the game
        if(countDown.sprite == go && replay==0 )
        {
            //increase replay so that we notify other code we are started
            replay++;
            //initialize chains based off previously slected random vars 
            mainCatImage.sprite = catImages[catType];
            mainCatSound.clip = catSounds[catType];
            mainCatSound.Play();
            // set the falling category sprite correctly
            FallingCat.GetComponent<SpriteRenderer>().sprite = catImages[catType];
            // intit all chains and respective buttons
            resetChain(btn11, btn12, btn13, 1);
            resetChain(btn21, btn22, btn23, 2);
            resetChain(btn31, btn32, btn33, 3);
            resetChain(btn41, btn42, btn43, 4);
            resetChain(btn51, btn52, btn53, 5);
            resetChain(btn61, btn62, btn63, 6);
            //first chain of each cateogry is red
            chain1.GetComponent<SpriteRenderer>().sprite = redChain;
            //reset falling category
            FallingCat.transform.position = chainHomePos;
            //set started bool to true
            started = true;
        }
        // if we havent started yet, ensure all game objects are continuously at
        // starting positions
        if (started == false)
        {
            chain1.transform.position = chain1Start;
            chain2.transform.position = chain2Start;
            chain3.transform.position = chain3Start;
            chain4.transform.position = chain4Start;
            chain5.transform.position = chain5Start;
            chain6.transform.position = chain6Start;
            FallingCat.transform.position = fallingCatStart;
        }

        if (!pausing)
        {
            //reset falling category
            FallingCat.transform.position = chainHomePos;
        }

        // if we are pausing chain to make room for for category change notification, pause the current chain the just hit bottom
        if (pausing && cur_pausing != -1 && !bar_at_bottom)
        {
            chains[cur_pausing - 1].transform.position = chainHomePos;
        }
    }

    // fade function when the game is over, and then load game over scene
    IEnumerator Fading()
    {
        animFade.SetBool("Fade",true);
        yield return new WaitUntil(() => Fade.color.a == 1);
        SceneManager.LoadScene("GameOver");
    }

    // function used when the game is paused, so that user cannot interact with
    // any game object buttons
    public void disBut()
    {
        foreach (Button ele in chain1buts)
        {
            ele.interactable = false;
        }
        foreach (Button ele in chain2buts)
        {
            ele.interactable = false;
        }
        foreach (Button ele in chain3buts)
        {
            ele.interactable = false;
        }
        foreach (Button ele in chain4buts)
        {
            ele.interactable = false;
        }
        foreach (Button ele in chain5buts)
        {
            ele.interactable = false;
        }
        foreach (Button ele in chain6buts)
        {
            ele.interactable = false;
        }
    }  

    //called on each button click
    public void click()
    {
        //get var of clicked game object
        var go = EventSystem.current.currentSelectedGameObject;
        //ensure it is not null
        if (go != null)
        {
            //determine what number chain was clicked (button names have what chain they are part of)
            int chain = int.Parse((go.name[3]).ToString());
            // determine what button on the chain was pushed from 1 to 3
            int btn = int.Parse((go.name[4]).ToString());
            //temp button ints that will hold the int of the other buttons
            int btnalt = btn;
            int btnaltalt = btn;
            //assign the other button ints
            while (btnalt == btn)
            {
                btnalt = Random.Range(1, 4);
            }

            while ((btnaltalt == btn) || (btnaltalt == btnalt))
            {
                btnaltalt = Random.Range(1, 4);
            } 


            // use switch case to check if correct button was chosen. chain#wrong really means correct
            // button, so do not get confused by that
            switch(chain)
            {
                case 1:
                    if(chain1wrong == btn )
                    {
                        if ((chain1buts[btn - 1].image.overrideSprite != allShapes[butShapes[0, btn - 1], 1]))
                        {
                            // if correct, change the clicked shape to be green with a check and increase score
                            chain1buts[btn - 1].image.overrideSprite = allShapes[butShapes[0, btn - 1], 1];
                            gameScoreObject.text = (++gameScore).ToString();
                        }
                    }
                    else
                    {
                        // if the correct one was not clicked, make sure that 
                        // the row that was clicked has now already been correctly
                        // chosen. if it has, do nothing, otherwise, disable all
                        // other game buttons and end game
                        if((chain1buts[btnalt-1].image.overrideSprite == allShapes[butShapes[0, btnalt - 1], 1]) || (chain1buts[btnaltalt-1].image.overrideSprite == allShapes[butShapes[0, btnaltalt - 1], 1])) {}
                        else
                        {
                            // wrong button was pressed on row that was not solved
                            // already, ending game. Give the shape the sprite that
                            // is red with a cross symbolizing the pick was wrong
                            Sprite old = chain1buts[btn - 1].image.sprite;
                            Sprite bad = allShapes[butShapes[0, btn - 1], 0];
                            stop.sprite = redCircle;
                            chain1buts[btn - 1].image.overrideSprite = allShapes[butShapes[0, btn - 1], 0];
                            disBut();
                            StartCoroutine(waitsec());

                        }
                    }
                    break;

                case 2:
                    if (chain2wrong == btn )
                    {
                        if ((chain2buts[btn - 1].image.overrideSprite != allShapes[butShapes[1, btn - 1], 1]))
                        {
                            // if correct, change the clicked shape to be green with a check and increase score
                            chain2buts[btn - 1].image.overrideSprite = allShapes[butShapes[1, btn - 1], 1];
                            gameScoreObject.text = (++gameScore).ToString();

                        }

                    }
                    else
                    {
                        // if the correct one was not clicked, make sure that 
                        // the row that was clicked has now already been correctly
                        // chosen. if it has, do nothing, otherwise, disable all
                        // other game buttons and end game
                        if ((chain2buts[btnalt - 1].image.overrideSprite == allShapes[butShapes[1, btnalt - 1], 1]) || (chain2buts[btnaltalt - 1].image.overrideSprite == allShapes[butShapes[1, btnaltalt - 1], 1])) { }
                        else
                        {
                            // wrong button was pressed on row that was not solved
                            // already, ending game. Give the shape the sprite that
                            // is red with a cross symbolizing the pick was wrong
                            stop.sprite = redCircle;
                            chain2buts[btn - 1].image.overrideSprite = allShapes[butShapes[1, btn - 1], 0];
                            disBut();
                            StartCoroutine(waitsec());

                        }
                    }
                    break;

                case 3:
                    if (chain3wrong == btn)
                    {
                        if ((chain3buts[btn - 1].image.overrideSprite != allShapes[butShapes[2, btn - 1], 1]))
                        {
                            // if correct, change the clicked shape to be green with a check and increase score
                            chain3buts[btn - 1].image.overrideSprite = allShapes[butShapes[2, btn - 1], 1];
                            gameScoreObject.text = (++gameScore).ToString();

                        }

                    }
                    else
                    {
                        // if the correct one was not clicked, make sure that 
                        // the row that was clicked has now already been correctly
                        // chosen. if it has, do nothing, otherwise, disable all
                        // other game buttons and end game
                        if ((chain3buts[btnalt - 1].image.overrideSprite == allShapes[butShapes[2, btnalt - 1], 1]) || (chain3buts[btnaltalt - 1].image.overrideSprite == allShapes[butShapes[2, btnaltalt - 1], 1])) { }
                        else
                        {
                            // wrong button was pressed on row that was not solved
                            // already, ending game. Give the shape the sprite that
                            // is red with a cross symbolizing the pick was wrong
                            stop.sprite = redCircle;
                            chain3buts[btn - 1].image.overrideSprite = allShapes[butShapes[2, btn - 1], 0];
                            disBut();
                            StartCoroutine(waitsec());

                        }
                    }
                    break;

                case 4:
                    if (chain4wrong == btn)
                    {
                        if ((chain4buts[btn - 1].image.overrideSprite != allShapes[butShapes[3, btn - 1], 1]))
                        {
                            // if correct, change the clicked shape to be green with a check and increase score
                            chain4buts[btn - 1].image.overrideSprite = allShapes[butShapes[3, btn - 1], 1];
                            gameScoreObject.text = (++gameScore).ToString();

                        }

                    }
                    else
                    {
                        // if the correct one was not clicked, make sure that 
                        // the row that was clicked has now already been correctly
                        // chosen. if it has, do nothing, otherwise, disable all
                        // other game buttons and end game
                        if ((chain4buts[btnalt - 1].image.overrideSprite == allShapes[butShapes[3, btnalt - 1], 1]) || (chain4buts[btnaltalt - 1].image.overrideSprite == allShapes[butShapes[3, btnaltalt - 1], 1])) { }
                        else
                        {
                            // wrong button was pressed on row that was not solved
                            // already, ending game. Give the shape the sprite that
                            // is red with a cross symbolizing the pick was wrong
                            stop.sprite = redCircle;
                            chain4buts[btn - 1].image.overrideSprite = allShapes[butShapes[3, btn - 1], 0];
                            disBut();
                            StartCoroutine(waitsec());

                        }
                    }
                    break;

                case 5:
                    if (chain5wrong == btn)
                    {
                        if ((chain5buts[btn - 1].image.overrideSprite != allShapes[butShapes[4, btn - 1], 1]))
                        {
                            // if correct, change the clicked shape to be green with a check and increase score
                            chain5buts[btn - 1].image.overrideSprite = allShapes[butShapes[4, btn - 1], 1];
                            gameScoreObject.text = (++gameScore).ToString();

                        }

                    }
                    else
                    {
                        // if the correct one was not clicked, make sure that 
                        // the row that was clicked has now already been correctly
                        // chosen. if it has, do nothing, otherwise, disable all
                        // other game buttons and end game
                        if ((chain5buts[btnalt - 1].image.overrideSprite == allShapes[butShapes[4, btnalt - 1], 1]) || (chain5buts[btnaltalt - 1].image.overrideSprite == allShapes[butShapes[4, btnaltalt - 1], 1])) { }
                        else
                        {
                            // wrong button was pressed on row that was not solved
                            // already, ending game. Give the shape the sprite that
                            // is red with a cross symbolizing the pick was wrong
                            stop.sprite = redCircle;
                            chain5buts[btn - 1].image.overrideSprite = allShapes[butShapes[4, btn - 1], 0];
                            disBut();
                            StartCoroutine(waitsec());

                        }
                    }
                    break;

                case 6:
                    if (chain6wrong == btn)
                    {
                        if ((chain6buts[btn - 1].image.overrideSprite != allShapes[butShapes[5, btn - 1], 1]))
                        {
                            // if correct, change the clicked shape to be green with a check and increase score
                            chain6buts[btn - 1].image.overrideSprite = allShapes[butShapes[5, btn - 1], 1];
                            gameScoreObject.text = (++gameScore).ToString();

                        }

                    }
                    else
                    {
                        // if the correct one was not clicked, make sure that 
                        // the row that was clicked has now already been correctly
                        // chosen. if it has, do nothing, otherwise, disable all
                        // other game buttons and end game
                        if ((chain6buts[btnalt - 1].image.overrideSprite == allShapes[butShapes[5, btnalt - 1], 1]) || (chain6buts[btnaltalt - 1].image.overrideSprite == allShapes[butShapes[5, btnaltalt - 1], 1])) { }
                        else
                        {
                            // wrong button was pressed on row that was not solved
                            // already, ending game. Give the shape the sprite that
                            // is red with a cross symbolizing the pick was wrong
                            stop.sprite = redCircle;
                            chain6buts[btn - 1].image.overrideSprite = allShapes[butShapes[5, btn - 1], 0];
                            disBut();
                            StartCoroutine(waitsec());

                        }
                    }
                    break;
            }
        }
        Debug.Log("out");
    }

    // call everytime a cahin reaches bottom and the game is not over,
    // or at the start of the gamefor each chain. It assigns random buttons
    // that create a mismatch based off of current category, and saves the info 
    // the chain#wrong global var
    void resetChain(Button btn1, Button btn2, Button btn3, int chainNum)
    {
        //all ints needed to describe chain and it's buttons
        int goodColor;
        int badColor;
        int btn1shape;
        int btn2shape;
        int btn3shape;
        int btn1size;
        int btn2size;
        int btn3size;
        int wrongButton = 0;
        int goodSize;
        int badSize;
        int btn1color;
        int btn2color;
        int btn3color;
        int goodShape;
        int badShape;

        // switch case with the curernt category as the decider 
        // 0 means color
        // 1 means size
        // 2 means shape
        switch(catType)
        {
            case 0:
                // current category is color. obtain correct and incorrect color
                goodColor = Random.Range(2, 11);
                badColor = goodColor;
                while(badColor == goodColor)
                {
                    badColor = Random.Range(2, 11);
                }

                // all buttons shapes and sizes don't matter for this category
                btn1shape = Random.Range(0, 5);
                btn2shape = Random.Range(0, 5);
                btn3shape = Random.Range(0, 5);
                btn1size = Random.Range(0, 2);
                btn2size = Random.Range(0, 2);
                btn3size = Random.Range(0, 2);
                // randomly decide which button is the wrong one
                wrongButton = Random.Range(1, 4);

                // fill in chain depending on what the wrong number is. then make
                // that button have the wrong color and the other 2 have the correct color
                // and then give all the other button properties (size, shape) the ones
                // decided above
                switch(wrongButton)
                {
                    case 1:
                        btn1.image.overrideSprite = allShapes[btn1shape, badColor];
                        btn1.transform.localScale = new Vector3(scales[btn1size], scales[btn1size], 0);
                        btn2.image.overrideSprite = allShapes[btn2shape, goodColor];
                        btn2.transform.localScale = new Vector3(scales[btn2size], scales[btn2size], 0);
                        btn3.image.overrideSprite = allShapes[btn3shape, goodColor];
                        btn3.transform.localScale = new Vector3(scales[btn3size], scales[btn3size], 0);
                        butShapes[chainNum - 1, 0] = btn1shape;
                        butShapes[chainNum - 1, 1] = btn2shape;
                        butShapes[chainNum - 1, 2] = btn3shape;
                        break;
                    case 2:
                        btn1.image.overrideSprite = allShapes[btn1shape, goodColor];
                        btn1.transform.localScale = new Vector3(scales[btn1size], scales[btn1size], 0);
                        btn2.image.overrideSprite = allShapes[btn2shape, badColor];
                        btn2.transform.localScale = new Vector3(scales[btn2size], scales[btn2size], 0);
                        btn3.image.overrideSprite = allShapes[btn3shape, goodColor];
                        btn3.transform.localScale = new Vector3(scales[btn3size], scales[btn3size], 0);
                        butShapes[chainNum - 1, 0] = btn1shape;
                        butShapes[chainNum - 1, 1] = btn2shape;
                        butShapes[chainNum - 1, 2] = btn3shape;
                        break;
                    case 3:
                        btn1.image.overrideSprite = allShapes[btn1shape, goodColor];
                        btn1.transform.localScale = new Vector3(scales[btn1size], scales[btn1size], 0);
                        btn2.image.overrideSprite = allShapes[btn2shape, goodColor];
                        btn2.transform.localScale = new Vector3(scales[btn2size], scales[btn2size], 0);
                        btn3.image.overrideSprite = allShapes[btn3shape, badColor];
                        btn3.transform.localScale = new Vector3(scales[btn3size], scales[btn3size], 0);
                        butShapes[chainNum - 1, 0] = btn1shape;
                        butShapes[chainNum - 1, 1] = btn2shape;
                        butShapes[chainNum - 1, 2] = btn3shape;
                        break;
                }
                break;

            case 1:
                // current category is size. obtain correct and incorrect size
                goodSize = Random.Range(0, 2);
                badSize = goodSize;
                while (badSize == goodSize)
                {
                    badSize = Random.Range(0, 2);
                }
                // all buttons shapes and colors don't matter for this category
                btn1shape = Random.Range(0, 5);
                btn2shape = Random.Range(0, 5);
                btn3shape = Random.Range(0, 5);
                btn1color = Random.Range(2, 11);
                btn2color = Random.Range(2, 11);
                btn3color = Random.Range(2, 11);
                // randomly decide which button is the wrong one
                wrongButton = Random.Range(1, 4);

                // fill in chain depending on what the wrong number is. then make
                // that button have the wrong sizw and the other 2 have the correct size
                // and then give all the other button properties (color, shape) the ones
                // decided above
                switch (wrongButton)
                {
                    case 1:
                        btn1.image.overrideSprite = allShapes[btn1shape, btn1color];
                        btn1.transform.localScale = new Vector3(scales[badSize], scales[badSize], 0);
                        btn2.image.overrideSprite = allShapes[btn2shape, btn2color];
                        btn2.transform.localScale = new Vector3(scales[goodSize], scales[goodSize], 0);
                        btn3.image.overrideSprite = allShapes[btn3shape, btn3color];
                        btn3.transform.localScale = new Vector3(scales[goodSize], scales[goodSize], 0);
                        butShapes[chainNum - 1, 0] = btn1shape;
                        butShapes[chainNum - 1, 1] = btn2shape;
                        butShapes[chainNum - 1, 2] = btn3shape;
                        break;
                    case 2:
                        btn1.image.overrideSprite = allShapes[btn1shape, btn1color];
                        btn1.transform.localScale = new Vector3(scales[goodSize], scales[goodSize], 0);
                        btn2.image.overrideSprite = allShapes[btn2shape, btn2color];
                        btn2.transform.localScale = new Vector3(scales[badSize], scales[badSize], 0);
                        btn3.image.overrideSprite = allShapes[btn3shape, btn3color];
                        btn3.transform.localScale = new Vector3(scales[goodSize], scales[goodSize], 0);
                        butShapes[chainNum - 1, 0] = btn1shape;
                        butShapes[chainNum - 1, 1] = btn2shape;
                        butShapes[chainNum - 1, 2] = btn3shape;
                        break;
                    case 3:
                        btn1.image.overrideSprite = allShapes[btn1shape, btn1color];
                        btn1.transform.localScale = new Vector3(scales[goodSize], scales[goodSize], 0);
                        btn2.image.overrideSprite = allShapes[btn2shape, btn2color];
                        btn2.transform.localScale = new Vector3(scales[goodSize], scales[goodSize], 0);
                        btn3.image.overrideSprite = allShapes[btn3shape, btn3color];
                        btn3.transform.localScale = new Vector3(scales[badSize], scales[badSize], 0);
                        butShapes[chainNum - 1, 0] = btn1shape;
                        butShapes[chainNum - 1, 1] = btn2shape;
                        butShapes[chainNum - 1, 2] = btn3shape;
                        break;
                }
                break;

            case 2:
                // current category is shape. obtain correct and incorrect shape
                goodShape = Random.Range(0, 5);
                badShape = goodShape;
                while (badShape == goodShape)
                {
                    badShape = Random.Range(0, 5);
                }

                // all buttons sizes and colors don't matter for this category
                btn1size = Random.Range(0, 2);
                btn2size = Random.Range(0, 2);
                btn3size = Random.Range(0, 2);
                btn1color = Random.Range(2, 11);
                btn2color = Random.Range(2, 11);
                btn3color = Random.Range(2, 11);
                // randomly decide which button is the wrong one
                wrongButton = Random.Range(1, 4);

                // fill in chain depending on what the wrong number is. then make
                // that button have the wrong shape and the other 2 have the correct shape
                // and then give all the other button properties (color, size) the ones
                // decided above
                switch (wrongButton)
                {
                    case 1:
                        btn1.image.overrideSprite = allShapes[badShape, btn1color];
                        btn1.transform.localScale = new Vector3(scales[btn1size], scales[btn1size], 0);
                        btn2.image.overrideSprite = allShapes[goodShape, btn2color];
                        btn2.transform.localScale = new Vector3(scales[btn2size], scales[btn2size], 0);
                        btn3.image.overrideSprite = allShapes[goodShape, btn3color];
                        btn3.transform.localScale = new Vector3(scales[btn3size], scales[btn3size], 0);
                        butShapes[chainNum - 1, 0] = badShape;
                        butShapes[chainNum - 1, 1] = goodShape;
                        butShapes[chainNum - 1, 2] = goodShape;
                        break;
                    case 2:
                        btn1.image.overrideSprite = allShapes[goodShape, btn1color];
                        btn1.transform.localScale = new Vector3(scales[btn1size], scales[btn1size], 0);
                        btn2.image.overrideSprite = allShapes[badShape, btn2color];
                        btn2.transform.localScale = new Vector3(scales[btn2size], scales[btn2size], 0);
                        btn3.image.overrideSprite = allShapes[goodShape, btn3color];
                        btn3.transform.localScale = new Vector3(scales[btn3size], scales[btn3size], 0);
                        butShapes[chainNum - 1, 0] = goodShape;
                        butShapes[chainNum - 1, 1] = badShape;
                        butShapes[chainNum - 1, 2] = goodShape;
                        break;
                    case 3:
                        btn1.image.overrideSprite = allShapes[goodShape, btn1color];
                        btn1.transform.localScale = new Vector3(scales[btn1size], scales[btn1size], 0);
                        btn2.image.overrideSprite = allShapes[goodShape, btn2color];
                        btn2.transform.localScale = new Vector3(scales[btn2size], scales[btn2size], 0);
                        btn3.image.overrideSprite = allShapes[badShape, btn3color];
                        btn3.transform.localScale = new Vector3(scales[btn3size], scales[btn3size], 0);
                        butShapes[chainNum - 1, 0] = goodShape;
                        butShapes[chainNum - 1, 1] = goodShape;
                        butShapes[chainNum - 1, 2] = badShape;
                        break;
                }
                break;
        }

        // update the global variable for the current chain noting what the 
        // new wrong button is
        switch (chainNum)
        {
            case 1:
                chain1wrong = wrongButton;
                break;
            case 2:
                chain2wrong = wrongButton;
                break;
            case 3:
                chain3wrong = wrongButton;
                break;
            case 4:
                chain4wrong = wrongButton;
                break;
            case 5:
                chain5wrong = wrongButton;
                break;
            case 6:
                chain6wrong = wrongButton;
                break;

        }
    }


    // detect when chains get to the bottom
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Chain1") || col.gameObject.CompareTag("Chain2") || col.gameObject.CompareTag("Chain3") || col.gameObject.CompareTag("Chain4") || col.gameObject.CompareTag("Chain5") || col.gameObject.CompareTag("Chain6"))
        {
            // keep track of amount of chains have passed for the current category. 
            // if it has reached set amount, change category
            if (++count == tilSwitch)
            {
                count = 0;
                tilSwitch = Random.Range(6, 10);
                int temp = catType;
                // make sure new category is not the current one
                while (catType == temp)
                {
                    catType = Random.Range(0, 1000) % 3;
                }
                // update text at bottom of gameplay screen and announce audio
                // category
                mainCatImage.sprite = catImages[catType];
                mainCatSound.clip = catSounds[catType];
                mainCatSound.Play();
                // pause the next chains to allow for gap between chains
                pausing = true;
                cur_pausing = -1;
                // set the falling category sprite correctly
                FallingCat.GetComponent<SpriteRenderer>().sprite = catImages[catType];
            }
        }
        // check what chain was selected. If the chain has been corectly answered
        // (meaining it has "good" in name which means there is a green shape
        // with a check), then reset the chain. If the chain will now be a start
        // of a new category (when count == 0), then make it red. If the chain 
        // has not been answered , then disable all button and end game using waitsec().
        if (col.gameObject.CompareTag("Chain1"))
        {
            bool answered = false;
            foreach(Button ele in chain1buts)
            {
                if(ele.image.overrideSprite.name.Contains("Good"))
                {
                    answered = true;
                }
            }
            if (answered == false)
            {
                bar_at_bottom = true;
                stop.sprite = redCircle;
                disBut();
                StartCoroutine(waitsec());
            }
            else
            {
                chain1.gameObject.transform.position = chainHomePos;
                resetChain(btn11, btn12, btn13, 1);
                if (count == 0)
                {
                    chain1.GetComponent<SpriteRenderer>().sprite = redChain;
                }
                else
                {
                    chain1.GetComponent<SpriteRenderer>().sprite = blackChain;
                }
                if (pausing)
                {
                    cur_pausing = 1;
                }
            }
        }
        else if (col.gameObject.CompareTag("Chain2"))
        {
            bool answered = false;
            foreach (Button ele in chain2buts)
            {
                if (ele.image.overrideSprite.name.Contains("Good"))
                {
                    answered = true;
                }
            }
            if (answered == false)
            {
                bar_at_bottom = true;
                stop.sprite = redCircle;
                disBut();
                StartCoroutine(waitsec());
            }
            else
            {
                chain2.gameObject.transform.position = chainHomePos;
                resetChain(btn21, btn22, btn23, 2);
                if (count == 0)
                {
                    chain2.GetComponent<SpriteRenderer>().sprite = redChain;
                }
                else
                {
                    chain2.GetComponent<SpriteRenderer>().sprite = blackChain;
                } 
            }
            if (pausing)
            {
                if (number_paused == how_many_to_pause)
                {
                    pausing = false;
                    return;
                }
                number_paused++;
                cur_pausing = 2;
            }
        }
        else if (col.gameObject.CompareTag("Chain3"))
        {
            bool answered = false;
            foreach (Button ele in chain3buts)
            {
                if (ele.image.overrideSprite.name.Contains("Good"))
                {
                    answered = true;
                }
            }
            if (answered == false)
            {
                bar_at_bottom = true;
                stop.sprite = redCircle;
                disBut();
                StartCoroutine(waitsec());
            }
            else
            {
                chain3.gameObject.transform.position = chainHomePos;
                resetChain(btn31, btn32, btn33, 3);
                if (count == 0)
                {
                    chain3.GetComponent<SpriteRenderer>().sprite = redChain;
                }
                else
                {
                    chain3.GetComponent<SpriteRenderer>().sprite = blackChain;
                }
            }
            if (pausing)
            {
                cur_pausing = 3;
            }
        }
        else if (col.gameObject.CompareTag("Chain4"))
        {
            bool answered = false;
            foreach (Button ele in chain4buts)
            {
                if (ele.image.overrideSprite.name.Contains("Good"))
                {
                    answered = true;
                }
            }
            if (answered == false)
            {
                bar_at_bottom = true;
                stop.sprite = redCircle;
                disBut();
                StartCoroutine(waitsec());
            }
            else
            {
                chain4.gameObject.transform.position = chainHomePos;
                resetChain(btn41, btn42, btn43, 4);
                if (count == 0)
                {
                    chain4.GetComponent<SpriteRenderer>().sprite = redChain;
                }
                else
                {
                    chain4.GetComponent<SpriteRenderer>().sprite = blackChain;
                }
            }
            if (pausing)
            {
                cur_pausing = 4;
            }
        }
        else if (col.gameObject.CompareTag("Chain5"))
        {
            bool answered = false;
            foreach (Button ele in chain5buts)
            {
                if (ele.image.overrideSprite.name.Contains("Good"))
                {
                    answered = true;
                }
            }
            if (answered == false)
            {
                bar_at_bottom = true;
                stop.sprite = redCircle;
                disBut();
                StartCoroutine(waitsec());
            }
            else
            {
                chain5.gameObject.transform.position = chainHomePos;
                resetChain(btn51, btn52, btn53, 5);
                if (count == 0)
                {
                    chain5.GetComponent<SpriteRenderer>().sprite = redChain;
                }
                else
                {
                    chain5.GetComponent<SpriteRenderer>().sprite = blackChain;
                }
            }
            if (pausing)
            {
                cur_pausing = 5;
            }
        }
        else if (col.gameObject.CompareTag("Chain6"))
        {
            bool answered = false;
            foreach (Button ele in chain6buts)
            {
                if (ele.image.overrideSprite.name.Contains("Good"))
                {
                    answered = true;
                }
            }
            if (answered == false)
            {
                bar_at_bottom = true;
                stop.sprite = redCircle;
                disBut();
                StartCoroutine(waitsec());
            }
            else
            {
                chain6.gameObject.transform.position = chainHomePos;
                resetChain(btn61, btn62, btn63, 6);
                if (count == 0)
                {
                    chain6.GetComponent<SpriteRenderer>().sprite = redChain;
                }
                else
                {
                    chain6.GetComponent<SpriteRenderer>().sprite = blackChain;
                }
            }
            if (pausing)
            {
                cur_pausing = 6;
            }
        }
        else if (col.gameObject.CompareTag("FallingCat"))
        {
            pausing = false;
        }
    }

    // wait 1.5 seconds then load the gameover scene
    IEnumerator waitsec()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }

}
