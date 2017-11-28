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

    private GameObject dragon;
    private dragonHealth dH;
    private dragonController dC;
    public playerHealth pH;
    private Renderer r;


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
        dragon = GameObject.Instantiate(dragonPrefab, startingLocation);
        dH = dragon.GetComponent<dragonHealth>();
        dC = dragon.GetComponent<dragonController>();
        r = dragon.GetComponent<MeshRenderer>();
        r.material = r.materials[0];
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
                //finish current attack
                while(dC.state == dragonController.dragonState.attack)
                {
                    yield return null;
                }
                //fly back to home
                dC.setState(dragonController.dragonState.exit);
                while (dC.state == dragonController.dragonState.exit) 
                {
                    yield return null;
                }
                //reset all hp
                resetAllHp();
            }
            yield return null;
        }
        skip = true;


        killDragon();
        //clear tiles

        //dragon death/win animation
    }

    private IEnumerator waitForReset()
    {
        while (skip)
        {
            yield return null;
        }
        skip = true;
        Destroy(dragon, 1);
        StartCoroutine(gameLoop());
    }

    private void killDragon()
    {
        r.material = r.materials[1];
        dragon.GetComponent<SplineWalker>().enabled = false;
    }

    private void resetAllHp()
    {

        pH.healToFull();
        dH.healToFull();
    }

    



   
}
