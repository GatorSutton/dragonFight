using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Wait until a player steps on a tile to start the game.
 * Animation introduction to the game
 * The game starts over with a resurrection of sorts if the players die
 * End game animation if the dragon dies
 * Reset the game with a button input
 */

public class GameController : MonoBehaviour {


    public GameObject dragonPrefab;
    public GameObject player;
    public Floor floor;
    public Transform startingLocation;
    public Material material;
    public GameObject initialFloor;
    public Canvas canvas;
    public GameObject menu;
    public NotificationController nC;

    private GameObject dragon;
    private dragonHealth dH;
    private dragonController dC;
    public playerHealth pH;
    private ScoreController sC;

    bool skip = true;

    public delegate void GameAction();
    public static event GameAction gameOver;



    // Use this for initialization
    void Start () {

        StartCoroutine(gameLoop());
        sC = GameObject.Find("Score").GetComponent<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            skip = false;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }

    }

    private IEnumerator gameLoop()
    {
        //need to spawn floor before beginning the game loop
        yield return StartCoroutine(waitForStepOntoTile());
        yield return StartCoroutine(fightLoop());
        yield return StartCoroutine(waitForFinishHim());
        yield return StartCoroutine(waitForReset());
        
    }


    


    private IEnumerator waitForStepOntoTile()
    {
        floor.setSwitchTiles();
        while (!floor.isEverySwitchOff() && skip)
        {
            yield return null;
        }
        floor.clearAllTiles();
        initialFloor.SetActive(false);
        skip = true;

        //dragon entrance animation
        //dragon gameObject is spawned
        dragon = GameObject.Instantiate(dragonPrefab, startingLocation.position, startingLocation.rotation);
        material.color = Color.red;
        dH = dragon.GetComponent<dragonHealth>();
        dC = dragon.GetComponent<dragonController>();
        dC.setState(dragonController.dragonState.enter);
        while (!dC.isActionComplete())
        {
            yield return null;
        }

    }

    private IEnumerator fightLoop()
    {
        dC.setState(dragonController.dragonState.attack);
        while (dH.HP > 0 && skip)
        {
            if(pH.HP <= 0)
            {
                nC.flashMessage("YOU DIED");
                //finish current attack
                while(!dC.isActionComplete())
                {
                    yield return null;
                }
                //fly back to home
                nC.flashMessage("REWINDING TIME");
                sC.Score = 0;
                dC.setState(dragonController.dragonState.exit);
                while (!dC.isActionComplete())
                {
                    yield return null;
                }
                //reset all hp
                resetAllHp();

                floor.setSwitchTiles();
                while (!floor.isEverySwitchOff() && skip)
                {
                    yield return null;
                }
                skip = true;
                floor.clearAllTiles();

                dC.setState(dragonController.dragonState.enter);
                while (!dC.isActionComplete())
                { 
                    yield return null;
                }
                dC.setState(dragonController.dragonState.attack);
            }
            yield return null;
        }
        skip = true;


        haltDragon();
        //clear tiles

        //dragon death/win animation
    }

    private IEnumerator waitForFinishHim()
    {
        print("starting waitForfinish");
        while (!dH.isDragDead())
        {
            yield return new WaitForEndOfFrame();
        }
        print("ending waitForfinish");
    }

    private IEnumerator waitForReset()
    {
        print("gameover");
        gameOver();
        while (skip)
        {
            yield return null;
        }
        skip = true;
        Destroy(dragon, 1);
        SceneManager.LoadScene("MainMenu");
    }

    private void haltDragon()
    {
        dragon.GetComponent<SplineWalker>().enabled = false;
        dC.setState(dragonController.dragonState.wait);
    }

    private void resetAllHp()
    {

        pH.healToFull();
        dH.healToFull();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Screenmanager Resolution Width", 1600);
        PlayerPrefs.SetInt("Screenmanager Resolution Height", 900);
        PlayerPrefs.SetInt("Screenmanager Is Fullscreen mode", 1);

        PlayerPrefs.DeleteKey("difficulty");
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.DeleteKey("score");
    }







}
