using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : FireAttack {

    public SpawnController spawnPrefab;
    public float numOfSpawns;
    public float secondsPerRotation;

    GameObject gameCenter;
    List<SpawnController> spawnList = new List<SpawnController>();

	// Use this for initialization
	void Start () {
        gameCenter = GameObject.FindGameObjectWithTag("center");
        id = 6;
	}
	
	// Update is called once per frame
	void Update ()  {
		//remove spawns from list if they are destroyed
        foreach(SpawnController spawn in spawnList)
        {
            if (spawn == null)
            {
                spawnList.Remove(spawn); 
            }
        }
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
        yield return new WaitForSeconds(5f);

        //add targets to all spawns
        foreach(SpawnController spawn in spawnList)
        {
            spawn.GetComponentInChildren<targetController>().startTarget();
        }
        yield return new WaitForSeconds(10f);

        foreach (SpawnController spawn in spawnList)
        {
            yield return StartCoroutine(spawn.moveToRandomSpot());
        }
        //kamikazee all spawns at random until list is empty

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
}
