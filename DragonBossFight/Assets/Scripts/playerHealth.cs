﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public int startingHealth;
    private Slider healthBar;
    private HPController hpController;
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
        hpController = GameObject.Find("Front Camera").transform.FindChild("Canvas").FindChild("PlayerHealth").GetComponent<HPController>();
        healthBar = GameObject.Find("PlayerHPBar").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        hpController.setHealth((float)hp / (float)startingHealth);
        setHealthBarSlider();
    }

    public void takeDamage()
    {
        if (hp > 0)
        {
            hp--;
            print("player health is: " + hp);
        }
    }

    public void healToFull()
    {
        hp = startingHealth;
    }

    private void setHealthBarSlider()
    {
        healthBar.value = ((float)hp / (float)startingHealth);
    }
}
