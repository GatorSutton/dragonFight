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
                        Invoke("leftView", 2);
                            break;

                    case SplineWalker.Position.Front:
                        Invoke("frontView", 2);
                        break;

                    case SplineWalker.Position.Right:
                        Invoke("rightView", 2);
                        break;
                   }
            }
        }

        if(other.name == "Cameras")
        {
            Invoke("frontView", 1);
        }

        


    }

    private void leftView()
    {
        canvas.targetDisplay = 0;
        crossbow.offsetRotation = 270;
    }

    private void frontView()
    {
        canvas.targetDisplay = 1;
        crossbow.offsetRotation = 0;
    }

    private void rightView()
    {
        canvas.targetDisplay = 2;
        crossbow.offsetRotation = 90;
    }




}
