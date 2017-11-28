using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonEntranceController : MonoBehaviour {

    private SplineWalker SW;
    private bool actionComplete = false;
    public Transform startingLocation;

	// Use this for initialization
	void Awake () {
        SW = GetComponent<SplineWalker>();
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    private void OnEnable()
    {
        SW.assignPath("entrance");
        SW.setProgress(0f);
        SW.setPosition(SplineWalker.Position.Right);
    }
}
