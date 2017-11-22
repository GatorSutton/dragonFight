﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public int startingHealth;

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }

	// Use this for initialization
	void Start () {
        hp = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage()
    {
        hp--;
        print(hp);
    }

    public void healToFull()
    {
        hp = startingHealth;
    }
}
