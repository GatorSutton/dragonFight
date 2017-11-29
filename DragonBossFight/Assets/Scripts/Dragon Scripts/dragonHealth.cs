using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonHealth : MonoBehaviour {

    public int startingHealth;
    public bool vulnerable = false;

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }


	// Use this for initialization
	void Awake () {
        hp = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F) && vulnerable)
        {
            hp--;
            print(hp);
        }
	}

    public void healToFull()
    {
        hp = startingHealth;
    }

}
