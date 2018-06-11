using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : FireAttack {

    public float speed;
    public float rotations;

    private Floor floor;


	// Use this for initialization
	void Start () {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override IEnumerator Attack()
    {
        activeStatus = true;
        var orb = new GameObject();
        var boxCollider = orb.gameObject.AddComponent<BoxCollider>();
        boxCollider.tag = "warn";
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(floor.sizeX/2-.5f, 1, floor.sizeZ/2-.5f);
        boxCollider.transform.position = new Vector3(-floor.sizeX / 4, 1000, -floor.sizeZ / 4);
        yield return new WaitForFixedUpdate();
        boxCollider.transform.Translate(new Vector3(0, -1000, 0));
        yield return new WaitForSeconds(3f);
        boxCollider.tag = "fire";
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < rotations; i++)
        {

            while (orb.transform.position.z < floor.sizeX / 4)
            {
                orb.transform.Translate(Vector3.forward * Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            while (orb.transform.position.x < floor.sizeX / 4)
            {
                orb.transform.Translate(Vector3.right * Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            while (orb.transform.position.z > -floor.sizeX / 4)
            {
                orb.transform.Translate(Vector3.back * Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            while (orb.transform.position.x > -floor.sizeX / 4)
            {
                orb.transform.Translate(Vector3.left * Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }

        orb.transform.Translate(0, 10000, 0);
        yield return new WaitForFixedUpdate();
        Destroy(orb);
        activeStatus = false;
    }
}
