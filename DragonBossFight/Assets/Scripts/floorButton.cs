using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class floorButton : MonoBehaviour {

    //create list of type tiles
    [SerializeField]
    List<Tile> restOfTiles = new List<Tile>();
    List<Tile> buttonTiles = new List<Tile>();
    Floor floor;
    bool playerOnButton = false;
    bool playerOffButton = false;
    public float percentage = 0;

    private void OnEnable()
    {
        percentage = 0;
        floor = GameObject.Find("Floor").GetComponent<Floor>();
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
        playerOffButton = false;

        if(buttonTiles.Count == 16 && restOfTiles.Count == 0)
        {
            initializeRestOfTiles();
        }

        foreach (Tile tile in restOfTiles)
        {
            if (tile.playerHere == true)
            {
                playerOffButton = true;
            }
        }

        foreach (Tile tile in buttonTiles)
        {
            if(tile.playerHere == true && !playerOffButton)
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

    private void initializeRestOfTiles()
    {
        restOfTiles = floor.getAllTiles();
        restOfTiles = restOfTiles.Except(buttonTiles).ToList();
    }

   



}
