using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCenter : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
		
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
