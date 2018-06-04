using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public int startingHealth;
    private Slider healthBar;
    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }

    private AudioSource AS;

	// Use this for initialization
	void Start () {
        hp = startingHealth;
        healthBar = GameObject.Find("PlayerHPBar").GetComponent<Slider>();
        AS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        setHealthBarSlider();
    }

    public void takeDamage()
    {
        if (hp > 0)
        {
            hp--;
            AS.Play();
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
