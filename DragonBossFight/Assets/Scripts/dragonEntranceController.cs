using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonEntranceController : MonoBehaviour {

    private SplineWalker SW;
    public bool actionComplete = false;
    public Transform startingLocation;
    public float timeBeforeAttacking;

	// Use this for initialization
	void Awake () {
        SW = GetComponent<SplineWalker>();
        actionComplete = false;
	}
	
	// Update is called once per frame
    /*
	void Update ()
    {
	    if(SW.isPositionReached())
        {
            actionComplete = true;
        }
	}
    */

    private void OnEnable()
    {
        StartCoroutine(enterScene());
        //SW.assignPath("entrance");
        //SW.setProgress(0f);
        //SW.setPosition(SplineWalker.Position.Right);
    }

    private void OnDisable()
    {
        actionComplete = false;
        StopAllCoroutines();
    }

     private IEnumerator enterScene()
    {
        SW.assignPath("entrance");
        SW.setProgress(0f);
        SW.setPosition(SplineWalker.Position.Right);
        while (!SW.isPositionReached())
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timeBeforeAttacking);
        actionComplete = true;
    }
}
