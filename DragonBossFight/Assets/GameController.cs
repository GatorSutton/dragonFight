using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Wait until a player steps on a tile to start the game.
 * Animation introduction to the game
 * The game starts over with a resurrection of sorts if the players die
 * End game animation if the dragon dies
 * Reset the game with a button input
 */

public class GameController : MonoBehaviour {


    public GameObject dragon;
    public GameObject player;
    public Floor floor;


    private dragonHealth dH;
    private playerHealth pH;

    bool skip = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            skip = false;
        }
         
	}

    private void gameLoop()
    {
        waitForStepOntoTile();
        fightLoop();
        endGame();
    }


    private IEnumerator waitForStepOntoTile()
    {
        while (!floor.isPlayerOnAnyTile() || skip)
        {
            yield return null;
        }
    }

    private IEnumerator fightLoop()
    {
        while (dH.HP > 0)
        {
            yield return null;
        }
    }

    private IEnumerator endGame()
    {

        yield return null;
    }


    



   
}
