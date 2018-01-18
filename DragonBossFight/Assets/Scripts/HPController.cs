
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour {

    private Image healthbar;
    float tmpHealth = 1;

	// Use this for initialization
	void Start () {
        healthbar = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        healthbar.fillAmount = tmpHealth;
	}

    public void setHealth(float hp)
    {
        tmpHealth = hp;
    }
}
