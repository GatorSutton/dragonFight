using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorButton : MonoBehaviour {

    //create list of type tiles
    List<Tile> buttonTiles = new List<Tile>();
    bool playerOnButton = false;
    public float percentage = 0;
    

    // Use this for initialization
    void Start()
    {
        //find all tiles within the box collider and add to list

    
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

        print(percentage);
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
