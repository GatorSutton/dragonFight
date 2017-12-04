using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonExitController : MonoBehaviour {

    private SplineWalker SW;
    public bool actionComplete = false;
    public Material material;

    // Use this for initialization
    void Awake () {
        SW = GetComponent<SplineWalker>();
        actionComplete = false;
    }

    private void OnEnable()
    {
         StartCoroutine(beginExit());
    }

    private void OnDisable()
    {
        actionComplete = false;
    }

    private IEnumerator beginExit()
    {
        material.color = Color.red;
        SW.assignPath("path");
        SW.setPosition(SplineWalker.Position.Front);

        while (!SW.isPositionReached())
        {
            yield return null;
        }
        SW.assignPath("entrance");
        SW.setPosition(SplineWalker.Position.Left);
        SW.setProgress(1f);
        while (!SW.isPositionReached())
        {
            yield return null;
        }
        actionComplete = true;
    }

}
