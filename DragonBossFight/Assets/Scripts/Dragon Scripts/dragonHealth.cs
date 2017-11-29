using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonHealth : MonoBehaviour {

    public int startingHealth;

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }
    private bool vulnerable;

    private dragonAttackController dAC;
    public Material material;

	// Use this for initialization
	void Awake () {
        hp = startingHealth;
       dAC = GetComponent<dragonAttackController>();
	}
	
	// Update is called once per frame
	void Update () {

         checkedForVulnerable();
        if (Input.GetKeyDown(KeyCode.F) && vulnerable)
        {
            print(hp);
            hp--;
        }

	}

    public void healToFull()
    {
        hp = startingHealth;
    }
    
    private void checkedForVulnerable()
    {
        if (dAC.enabled)
        {
            if (dAC.isResting())
            {
                vulnerable = true;
                material.color = Color.white;
            }
            else
            {
                vulnerable = false;
                material.color = Color.red;
            }
        }
    }

}
