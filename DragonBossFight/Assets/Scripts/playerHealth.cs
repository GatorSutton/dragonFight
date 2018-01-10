using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public int startingHealth;

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
	}
	
	// Update is called once per frame
	void Update () {
        hpController.setHealth((float)hp / (float)startingHealth);
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
}
