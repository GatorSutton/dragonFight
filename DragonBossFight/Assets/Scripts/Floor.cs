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
    ArduinoCommunicator ACSensors;

	// Use this for initialization
	void Awake () {
        CreateFloor();
    }

    private void Start()
    {
        AC = GameObject.Find("ArduinoCommunicator").GetComponent<ArduinoCommunicator>();
        ACSensors = GameObject.Find("ArduinoCommunicatorSensors").GetComponent<ArduinoCommunicator>();
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
        bool[] list = ACSensors.getMessageIN();

        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].playerHere = list[(x * sizeZ) + z];
            }
            else
            {
            tiles[x,z].playerHere = list[(x * 2 + 1) - z];
            }

        }
        *


        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
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
        */

        if (list.Length == 64)
        {
            tiles[0, 0].playerHere = list[0];
            tiles[0, 1].playerHere = list[1];
            tiles[0, 2].playerHere = list[2];
            tiles[0, 3].playerHere = list[3];
            tiles[1, 0].playerHere = list[4];
            tiles[1, 1].playerHere = list[5];
            tiles[1, 2].playerHere = list[6];
            tiles[1, 3].playerHere = list[7];

            tiles[2, 0].playerHere = list[8];
            tiles[2, 1].playerHere = list[9];
            tiles[2, 2].playerHere = list[10];
            tiles[2, 3].playerHere = list[11];
            tiles[3, 0].playerHere = list[12];
            tiles[3, 1].playerHere = list[13];
            tiles[3, 2].playerHere = list[14];
            tiles[3, 3].playerHere = list[15];
        }
        


    }

    private void setFloorData()
    {
        byte[] list = new byte[sizeX * sizeZ];

        
        byte value;
        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                value = (byte)(tiles[x, z].myState + 48);

                if (tiles[x, z].myState == Tile.States.NONE && tiles[x, z].isPlayerHere())
                {
                    value = 70;
                }

                if(x % 2 == 0)
                {
                    list[(x * sizeZ) + z] = value;
                }
                else
                {
                    list[((x+1) * sizeZ)-z-1] = value;
                }

            }

        }
        */

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                value = (byte)(tiles[x, z].myState + 48);
                if (tiles[x, z].myState == Tile.States.NONE && tiles[x, z].isPlayerHere())
                {
                    value = 70;
                }
                list[(x * sizeZ) + z] = value;
            }
        }




                string output = Encoding.UTF8.GetString(list, 0, sizeX*sizeZ);
        AC.setMessageOUT(output);
    }
     
}
