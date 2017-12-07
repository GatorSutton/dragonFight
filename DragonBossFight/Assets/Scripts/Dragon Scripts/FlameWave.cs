﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWave : FireAttack {

    public Transform warn;
    public Transform fire;
    public float speed;

    private Vector3 homePosition = new Vector3(0f, 0f, 0f);
    private Floor floor;
    private float distanceToFirstSquare;
    private float distanceToLastSquare;

    void Start()
    {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        distanceToFirstSquare = 10 - (floor.sizeX / 2f) + .5f;
        distanceToLastSquare = 10 + (floor.sizeZ/ 2f) - .5f;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
    }

    public override IEnumerator Attack()
    {
        activeStatus = true;
        Vector3 startingPosition = new Vector3(-3.5f + Random.Range(1,6), 0f, 0f);
        //Set warn and fire to initial position
        warn.localPosition = startingPosition + new Vector3(0f, 0f, distanceToFirstSquare);
        fire.localPosition = startingPosition;
        //Move warn to first square and then reset
        yield return new WaitForSeconds(.5f);
        warn.localPosition = homePosition;
        //Move fireball through all square and back
        yield return null;

        while (fire.localPosition.z < distanceToLastSquare)
        {
            fire.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2f);

        fire.localPosition = homePosition;
        activeStatus = false;
    }
}
