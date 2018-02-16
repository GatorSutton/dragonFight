using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonHealth : MonoBehaviour {

    public int startingHealth;
    public HPController hpController;
    public Animator anim;

    public targetController headTarget;
    public targetController leftWingTarget;
    public targetController rightWingTarget;

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
        hpController = GameObject.Find("Front Camera").transform.FindChild("Canvas").FindChild("Healthbar").GetComponent<HPController>();
	}
	
	// Update is called once per frame
	void Update () {
        checkedForVulnerable();
        if (Input.GetKeyDown(KeyCode.F) && vulnerable)
        {
         
            hp--;
            print(hp);

        }
        hpController.setHealth(((float)hp / (float)startingHealth));

    }

    public void healToFull()
    {
        hp = startingHealth;
    }

    public void takeDamage()
    {
        hp--;
        anim.SetTrigger("hit");
        print(hp);
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

    public void setTargets()
    {
        headTarget.startTarget();
        leftWingTarget.startTarget();
        rightWingTarget.startTarget();
    }

}
