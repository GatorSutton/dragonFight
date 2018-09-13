using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightTarget : MonoBehaviour {

    //spawns a target on the knight and starts back up the dialog when done

    public targetController tC;
    public Transform[] targetSpots;
    dialogController dC;
    [SerializeField]
    List<targetController> testTargets = new List<targetController>();
    NotificationController nC;
    int counter = 0;

    public delegate void KnightAction();
    public static event KnightAction taskComplete;

    private void Start()
    {
        nC = GameObject.Find("Notification").GetComponent<NotificationController>();
        //testTarget = Instantiate(tC, head);
        foreach(Transform spot in targetSpots)
        {
            testTargets.Add(Instantiate(tC, spot));
        }
    }

    // Update is called once per frame
    void Update () {
        //print(counter);
	}

    private void OnEnable()
    {
        targetController.TargetShot += addToCountandCheck;
        targetController.TargetMissed += failure;
    }

    private void OnDisable()
    {
        targetController.TargetShot -= addToCountandCheck;
        targetController.TargetMissed -= failure;
    }

    private void addToCountandCheck()
    {
        counter++;
        if(counter == testTargets.Count)
        {
            taskComplete();
        }
    }

    private void failure()
    {
        restartTargets();
    }

    public void spawnTargets()
    {
        counter = 0;
        //testTarget.startTarget();
        foreach(targetController tC in testTargets)
        {
            tC.startTarget();
        }

    }

    private void restartTargets()
    {
        counter = 0;
        nC.flashMessage("Try Again");
        //testTarget.startTarget();
        foreach (targetController tC in testTargets)
        {
            tC.startTarget();
        }
    }


}
