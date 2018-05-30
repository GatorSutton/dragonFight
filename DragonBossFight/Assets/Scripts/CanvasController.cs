using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public Crossbow crossbow;
    public Canvas canvas;
    private SplineWalker SW;
    private float cooldownTime = 2;
    private float cooldownTimer = 0;
	
	// Update is called once per frame
	void Update () {
        cooldownTimer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dragon")
        {
            if (cooldownTimer > cooldownTime)
            {
                cooldownTimer = 0;
                SW = other.GetComponent<SplineWalker>();

                switch (SW.position)
                {
                    case SplineWalker.Position.Left:
                        canvas.targetDisplay = 0;
                        crossbow.offsetRotation = -90;
                        break;
                    case SplineWalker.Position.Front:
                        canvas.targetDisplay = 1;
                        crossbow.offsetRotation = 0;
                        break;
                    case SplineWalker.Position.Right:
                        canvas.targetDisplay = 2;
                        crossbow.offsetRotation = 90;
                        break;
                }
            }
        }


    }




}
