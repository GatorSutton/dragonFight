using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAnimationEnder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
       foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
