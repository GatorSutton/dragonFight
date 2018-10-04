using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : FireAttack {

    public float speed;
    public float rotations;
    public Animator anim;
    public GameObject squareEffect;

    private Floor floor;
    private AudioSource aS;
    


	// Use this for initialization
	void Start () {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        id = 5;
    }

    public override IEnumerator Attack()
    {
        activeStatus = true;
        yield return checkForPotion();
        Instantiate(squareEffect, this.transform);
        yield return new WaitForSeconds(2f);
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


        //add speed up every rotation and high chance to change direction
        for (int i = 0; i < rotations; i++)
        {
            float dynamicSpeed = speed + i;
            if(Random.value > .5f)
            {
                yield return StartCoroutine(rotateClockwise(dynamicSpeed, orb));
            }
            else
            {
                yield return StartCoroutine(rotateCounterClockwise(dynamicSpeed, orb));
            }
        }

        orb.transform.Translate(0, 10000, 0);
        yield return new WaitForFixedUpdate();
        Destroy(orb);
        activeStatus = false;
    }

    IEnumerator rotateClockwise(float speed, GameObject orb)
    {
        while (orb.transform.position.z < floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }
        while (orb.transform.position.x < floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.right * Time.deltaTime * speed);
            yield return null;
        }
        while (orb.transform.position.z > -floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.back * Time.deltaTime * speed);
            yield return null;
        }
        while (orb.transform.position.x > -floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.left * Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator rotateCounterClockwise(float speed, GameObject orb)
    { 
        while (orb.transform.position.x < floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.right * Time.deltaTime * speed);
            yield return null;
        }
        while (orb.transform.position.z < floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }
        while (orb.transform.position.x > -floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.left * Time.deltaTime * speed);
            yield return null;
        }
        while (orb.transform.position.z > -floor.sizeX / 4)
        {
            orb.transform.Translate(Vector3.back * Time.deltaTime * speed);
            yield return null;
        }

        yield return new WaitForSeconds(2f);
    }
}
