using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightTarget : MonoBehaviour {

    //spawns a target on the knight and starts back up the dialog when done

    public targetController tC;
    public Transform head;
    dialogController dC;
    targetController testTarget;
    NotificationController nC;

    public delegate void KnightAction();
    public static event KnightAction taskComplete;

    private void Start()
    {
        nC = GameObject.Find("Notification").GetComponent<NotificationController>();
        testTarget = Instantiate(tC, head);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnEnable()
    {
        targetController.TargetShot += success;
        targetController.TargetSelfDestruct += failure;
    }

    private void OnDisable()
    {
        targetController.TargetShot -= success;
        targetController.TargetSelfDestruct -= failure;
    }

    private void success()
    {
        taskComplete();
    }

    private void failure()
    {
        restartTarget();
    }

    public void spawnTarget()
    {
        testTarget.startTarget();
    }

    private void restartTarget()
    {
        nC.flashMessage("Try Again");
        testTarget.startTarget();
    }


}
