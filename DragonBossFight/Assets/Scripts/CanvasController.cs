using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {


    private Crossbow[] crossbows = new Crossbow[4];
    public Canvas canvas;
    private SplineWalker SW;
    private float cooldownTime = 2;
    private float cooldownTimer = 0;

    private void Start()
    {
        GameObject[] bows = GameObject.FindGameObjectsWithTag("bow");
        for (int i = 0; i < bows.Length; i++)
        {
            crossbows[i] = bows[i].GetComponent<Crossbow>();
        }
        
    }

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
        foreach (Crossbow crossbow in crossbows)
        {
            crossbow.offsetRotation = 270;
        }
    }

    private void frontView()
    {
        canvas.targetDisplay = 1;
        foreach (Crossbow crossbow in crossbows)
        {
            crossbow.offsetRotation = 0;
        }
    }

    private void rightView()
    {
        canvas.targetDisplay = 2;
        foreach (Crossbow crossbow in crossbows)
        {
            crossbow.offsetRotation = 90;
        }
    }




}
