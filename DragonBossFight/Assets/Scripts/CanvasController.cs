using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {


    private Crossbow[] crossbows = new Crossbow[4];
    public Canvas canvas;
    private SplineWalker SW;


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
  
	}

    public void updateCanvas()
    {
        if(SW == null)
        {
            SW = GameObject.FindGameObjectWithTag("dragon").GetComponent<SplineWalker>();
        }
        if (GameObject.FindGameObjectWithTag("potion") == null)
        {
            switch (SW.position)
            {

                case SplineWalker.Position.Left:
                    leftView();
                    break;

                case SplineWalker.Position.Front:
                    frontView();
                    break;

                case SplineWalker.Position.Right:
                    rightView();
                    break;
            }

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

    public void frontView()
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
