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


    private dragonHealth dH;
    private playerHealth pH;

    bool skip = true;


	// Use this for initialization
	void Start () {

        StartCoroutine(gameLoop());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            skip = false;
        }
         
	}

    private IEnumerator gameLoop()
    {
        //need to spawn floor before beginning the game loop
        yield return StartCoroutine(waitForStepOntoTile());
        yield return StartCoroutine(fightLoop());
        yield return StartCoroutine(waitForReset());
        
    }


    private IEnumerator waitForStepOntoTile()
    {
       // yield return  new WaitForSeconds(2f);
        while (!floor.isPlayerOnAnyTile() && skip)
        {
            yield return null;
        }
        skip = true;
        //dragon entrance animation
        //dragon gameObject is spawned
        dH = GameObject.Instantiate(dragonPrefab).GetComponent<dragonHealth>();
    }

    private IEnumerator fightLoop()
    {

        while (dH.HP > 0 && skip)
        {
            yield return null;
        }
        skip = true;
        //dragon death/win animation
    }

    private IEnumerator waitForReset()
    {
        while (skip)
        {
            yield return null;
        }
        skip = true;
    }

    



   
}
