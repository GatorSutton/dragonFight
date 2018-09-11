using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorButton : MonoBehaviour {

    //create list of type tiles
    List<Tile> buttonTiles = new List<Tile>();
    bool playerOnButton = false;
    public float percentage = 0;

    private void OnEnable()
    {
        percentage = 0;
    }

    private void OnDisable()
    {
        foreach(Tile tile in buttonTiles)
        {
            tile.myState = Tile.States.NONE;
        }
    }


    // Update is called once per frame
    void Update()
    {
        playerOnButton = false;

        foreach(Tile tile in buttonTiles)
        {
            if(tile.playerHere == true)
            {
                playerOnButton = true;
            }
        }

        if(playerOnButton && percentage < 1)
        {
            percentage += .005f;
        }
        else if(percentage > 0)
        {
            percentage -= .01f;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "tile")
        {
            buttonTiles.Add(collider.gameObject.GetComponent<Tile>());
        }
    }

    //method to set tiles to active



}
