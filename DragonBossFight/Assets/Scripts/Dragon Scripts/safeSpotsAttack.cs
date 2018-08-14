using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeSpotsAttack : FireAttack {

    public int safeSpaces;

    private Floor floor;
    private List<GameObject> spots = null;

    // Use this for initialization
    void Start () {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        id = 6;
    }


    public override IEnumerator Attack()
    {
        activeStatus = true;
        spots = new List<GameObject>();
        for (int i = 0; i < floor.sizeX; i++)
        {
            for (int j = 0; j < floor.sizeZ; j++)
            {
                var spot = new GameObject();
                spots.Add(spot);
                var boxCollider = spot.gameObject.AddComponent<BoxCollider>();
                boxCollider.tag = "warn";
                boxCollider.isTrigger = true;
                boxCollider.size = new Vector3(.5f, .5f, .5f);
                spot.transform.position = new Vector3(i-floor.sizeX/2+.5f, 1000, j-floor.sizeZ/2+.5f);
            }
        }
        for (int i = 0; i < safeSpaces; i++)
        {
            int random = Random.Range(0, spots.Count);
            Destroy(spots[random]);
            spots.RemoveAt(random);
        }
         yield return new WaitForFixedUpdate();
        foreach(var spot in spots)
        {
            spot.transform.Translate(0, -1000, 0);
        }
        yield return new WaitForSeconds(5f);

        foreach(var spot in spots)
        {
            spot.gameObject.tag = "fire";
        }
        yield return new WaitForSeconds(5f);

        foreach (var spot in spots)
        {
            spot.transform.Translate(0, 1000, 0);
        }
        yield return new WaitForFixedUpdate();

        foreach (var spot in spots)
        {
            Destroy(spot);
        }
        activeStatus = false;
    }
}
