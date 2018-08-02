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
        transform.LookAt(target);
        transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));

    }
}
