using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Floor : MonoBehaviour {

    public Tile tilePrefab;
    public int sizeX;
    public int sizeZ;


    [System.NonSerialized]
    private Tile[,] tiles;
    ArduinoCommunicator AC;

	// Use this for initialization
	void Awake () {
        CreateFloor();
    }

    private void Start()
    {
        AC = GameObject.Find("ArduinoCommunicator").GetComponent<ArduinoCommunicator>();
    }

    private void Update()
    {
        checkForRealPlayer();
        setFloorData();
    }


    private void CreateTile(int x, int z)
    {
        Tile newTile = Instantiate(tilePrefab);
        tiles[x, z] = newTile;
        newTile.name = "Tile " + x + ", " + z;
        newTile.transform.localPosition = new Vector3(x - sizeX * 0.5f + 0.5f, 0f, z - sizeZ * 0.5f + 0.5f);
        newTile.transform.parent = transform;
    }

    private void CreateFloor()
    {
        tiles = new Tile[sizeX, sizeZ];
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                CreateTile(x, z);
            }
        }
    }

    public Tile getTile(int x, int z)
    {
        return tiles[x, z];
    }

    public void setTile(int x, int z, Tile.States state)
    {
        tiles[x, z].myState = state;
    }

    public bool isPlayerOnAnyTile()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                if(tiles[x, z].isPlayerHere())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void setSwitchTiles()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].myState = Tile.States.SWITCH;
            }
        }
    }

    public bool isEverySwitchOff()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                if (tiles[x, z].myState == Tile.States.SWITCH)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void clearAllTiles()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].myState = Tile.States.NONE;
            }
        }
    }

    private void checkForRealPlayer()
    {
        //   bool[] list = hPD.getPlayerData();
        bool[] list = AC.getMessageIN();

        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].playerHere = list[(x * sizeZ) + z];
            }
        }
        */

        for (int x = 0; x < 2; x++)
        {
            for (int z = 0; z < 2; z++)
            {
                if (x % 2 == 0)
                {
                    tiles[x, z].playerHere = list[(x * 2) + z];
                }
                else
                {
                    tiles[x,z].playerHere = list[(x * 2 + 1) - z];
                }
            }
        }

        /*
            tiles[0, 0].playerHere = list[0];
            tiles[1, 0].playerHere = list[1];
            tiles[1, 1].playerHere = list[2];
            tiles[0, 1].playerHere = list[3];
            */

    }

    private void setFloorData()
    {
        //byte[] list = new byte[sizeX * sizeZ];
        byte[] list = new byte[4];

        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                list[(x * sizeZ) + z] = (byte)tiles[x, z].myState;

                if (tiles[x, z].myState == Tile.States.NONE && tiles[x, z].isPlayerHere())
                {
                    list[(x * sizeZ) + z] = 0xFF;
                }

            }

        }
        */

        for (int x = 0; x < 2; x++)
        {
            for (int z = 0; z < 2; z++)
            {
                list[(x * 2) + z] = (byte)(tiles[x, z].myState + 48);

                if (tiles[x, z].myState == Tile.States.NONE && tiles[x, z].isPlayerHere())
                {
                    list[(x * 2) + z] = 70;
                }

            }

        }

        /*
        byte[] list = new byte[4];
        list[0] = (byte)(tiles[0, 0].myState + 48);
        list[1] = (byte)(tiles[1, 0].myState + 48);
        list[2] = (byte)(tiles[1, 1].myState + 48);
        list[3] = (byte)(tiles[0, 1].myState + 48);
        */
        string output = Encoding.UTF8.GetString(list, 0, 4);
        AC.setMessageOUT(output);
        Debug.Log(output);
    }
     
}
