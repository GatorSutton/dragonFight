﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
