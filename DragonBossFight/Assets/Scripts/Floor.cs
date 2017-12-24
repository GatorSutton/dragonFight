using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public Tile tilePrefab;
    public int sizeX;
    public int sizeZ;
    public SerialController serialController;

    [System.NonSerialized]
    private Tile[,] tiles;
    private string message;

	// Use this for initialization
	void Awake () {
        CreateFloor();
     
    }

    private void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    private void Update()
    {
        checkForRealPlayer();
        updateSerialData();
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

        /*
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                tiles[x, z].playerHere = list[(x * sizeZ) + z];
            }
        }
        */
        /*
            tiles[0, 0].playerHere = list[0];
            tiles[1, 0].playerHere = list[1];
            tiles[1, 1].playerHere = list[2];
            tiles[0, 1].playerHere = list[3];
            */
    }

    public byte[] getFloorData()
    {
        byte[] list = new byte[sizeX * sizeZ];


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
        return list;
    }

    private void updateSerialData()
    {
        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);

        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        serialController.SendSerialMessage(message);
    }
     
}
