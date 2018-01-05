using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonEntranceController : MonoBehaviour {

    private SplineWalker SW;
    public bool actionComplete = false;
    public Transform startingLocation;

	// Use this for initialization
	void Awake () {
        SW = GetComponent<SplineWalker>();
        actionComplete = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(SW.isPositionReached())
        {
            actionComplete = true;
        }
	}

    private void OnEnable()
    {
        SW.assignPath("entrance");
        SW.setProgress(0f);
        SW.setPosition(SplineWalker.Position.Right);
    }

    private void OnDisable()
    {
        actionComplete = false;
    }
}
