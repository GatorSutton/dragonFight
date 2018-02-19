using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragonHealth : MonoBehaviour {

    public int startingHealth;
    public HPController hpController;
    public Animator anim;
    public List<targetController> targets = new List<targetController>();
    private Slider healthBar;

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }

    private dragonAttackController dAC;
    public Material material;

	// Use this for initialization
	void Awake () {
        hp = startingHealth;
       dAC = GetComponent<dragonAttackController>();
        hpController = GameObject.Find("Front Camera").transform.FindChild("Canvas").FindChild("Healthbar").GetComponent<HPController>();
        healthBar = GameObject.Find("DragonHPBar").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {  
            hp--;
            print(hp);
        }
        hpController.setHealth(((float)hp / (float)startingHealth));

        checkAllTargets();

    }

    public void healToFull()
    {
        hp = startingHealth;
        setHealthBarSlider();
    }

    public void takeDamage(int amount)
    {
        hp = hp - amount;
        anim.SetTrigger("hit");
        print(hp);
        setHealthBarSlider();
    }
    
    public void setTargets()
    {
        foreach(var target in targets)
        {
            target.startTarget();
        }
    }

    private void checkAllTargets()
    {
        float count = 0;
        foreach(var target in targets)
        {
            if(target.dead)
            {
                count++;
            }

        }
        if (count == targets.Count)
        {
            takeDamage(10);
            resetTargets();
        }

    }

    private void resetTargets()
    {
        foreach(var target in targets)
        {
            target.reset();
        }
    }

    private void setHealthBarSlider()
    {
        healthBar.value = ((float)hp / (float)startingHealth);
    }

}
