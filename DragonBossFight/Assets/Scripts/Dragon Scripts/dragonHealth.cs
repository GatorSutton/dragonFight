using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragonHealth : MonoBehaviour {

    public Animator anim;
    public int startingHealth;
    public HPController hpController;
    public List<targetController> targets = new List<targetController>();
    public AudioClip audioHit;
    public AudioClip audioDead;

    private Slider healthBar;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = audioHit;
        if (hp > 0)
        {
            audioSource.Play();
            anim.SetTrigger("hit");
        }
        print(hp);
        setHealthBarSlider();

        if(hp <= 0)
        {
            anim.SetTrigger("dead");
            audioSource.clip = audioHit;
            audioSource.Play();
        }
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
            anim.speed = 1;
            anim.SetTrigger("hit");
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
