using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : FireAttack {

    public SpawnController spawnPrefab;
    public float numOfSpawns;
    public float secondsPerRotation;

    GameObject gameCenter;
    List<SpawnController> spawnList = new List<SpawnController>();
    List<Vector3> spotList = new List<Vector3>();
    Floor floor;

	// Use this for initialization
	void Start () {
        floor = GameObject.FindGameObjectWithTag("floor").GetComponent<Floor>();
        gameCenter = GameObject.FindGameObjectWithTag("center");
        id = 6;
        buildSpotList();
	}
	
	// Update is called once per frame
	void Update ()  {
		//remove spawns from list if they are destroyed
        /*
        foreach(SpawnController spawn in spawnList)
        {
            if (spawn == null)
            {
                spawnList.Remove(spawn); 
            }
        }
        */
	}


    //Spawn the minis and have them circle the dragon send them off one at a time
    public override IEnumerator Attack()
    {
        //spawn all spawns
        activeStatus = true;
        for (int i = 0; i < numOfSpawns; i++)
        {
            spawnList.Add(spawnTheSpawn(secondsPerRotation));
            yield return new WaitForSeconds(secondsPerRotation/numOfSpawns);
        }
        yield return new WaitForSeconds(2f);

        //add targets to all spawns
        foreach(SpawnController spawn in spawnList)
        {
            spawn.GetComponentInChildren<targetController>().startTarget();
        }
        yield return new WaitForSeconds(11f);

        foreach (SpawnController spawn in spawnList)
        {
            StartCoroutine(spawn.moveToRandomSpot(randomFromSpotListAndRemove()));
           // spawnList.Remove(spawn);
            yield return new WaitForSeconds(1f);
        }
        //kamikazee all spawns at random until list is empty
        yield return new WaitForSeconds(10f);

        //Wait until the last mini has exploded
        activeStatus = false;
    }

    private SpawnController spawnTheSpawn(float secondPerRotation)
    {
        SpawnController sC = Instantiate(spawnPrefab, transform.parent);
        sC.transform.localPosition = new Vector3(0f, 5f, -5f);
        sC.secondsPerRotation = secondPerRotation;
        return sC;
    }

    private void buildSpotList()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                spotList.Add(new Vector3((i*floor.sizeX/4), 0f, (j*floor.sizeX/4)));
            }
        }
    }

    private Vector3 randomFromSpotListAndRemove()
    {
        int random = Random.Range(0, spotList.Count);
        Vector3 result = spotList[random];
        spotList.RemoveAt(random);
        return result;

    }

}
