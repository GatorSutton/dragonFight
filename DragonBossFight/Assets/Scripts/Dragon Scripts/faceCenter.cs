using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCenter : MonoBehaviour {

    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("center").transform;
	}
	
	// Update is called once per frame
	void Update () {
        focusAtCenter();
    }

    private void focusAtCenter()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;
    }
}
