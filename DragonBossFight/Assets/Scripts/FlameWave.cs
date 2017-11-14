using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWave : MonoBehaviour {

    public Floor floor;
    public float timeBetweenFire;
    public float timeBetweenWarning;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(LeftAttack());
        }
	}

    private IEnumerator LeftAttack()
    {

        int z = Random.Range(1, floor.sizeZ - 2);

        for (int j = 0; j < 6; j++)
        {
            for (int i = z - 1; i <= z + 1; i++)                                     //flicker a warning
            {
                if (floor.getTile(0, i).myState == Tile.States.NONE)
                {
                    floor.setTile(0, i, Tile.States.WARN);
                }
                else
                {
                    floor.setTile(0, i, Tile.States.NONE);
                }
            }
            yield return new WaitForSeconds(timeBetweenWarning);
        }

        for (int x = 0; x < floor.sizeX; x++)                           //move the fire
        {
            floor.setTile(x, z + 1, Tile.States.FIRE);
            floor.setTile(x, z, Tile.States.FIRE);
            floor.setTile(x, z - 1, Tile.States.FIRE);
            yield return new WaitForSeconds(timeBetweenFire);
        }
        for (int x = 0; x < floor.sizeX; x++)                           //clear the fire
        {
            floor.setTile(x, z + 1, Tile.States.NONE);
            floor.setTile(x, z, Tile.States.NONE);
            floor.setTile(x, z - 1, Tile.States.NONE);
        }

        print("Attack");
    }
}
