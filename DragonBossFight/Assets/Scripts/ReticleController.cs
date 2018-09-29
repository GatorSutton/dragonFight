using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour {

    Animator anim;
    Slider slider;
    Outline outline;
    float reloadPercentage;

    public float ReloadPercentage
    {
        set { reloadPercentage = value; }
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        slider = transform.GetComponentInChildren<Slider>();
        outline = GetComponent<Outline>();
	}
	
	// Update is called once per frame
	void Update () {
        setReloadBar();
	}

    public void reticleFire()
    {
        anim.SetTrigger("Fire");
    }

    private void setReloadBar()
    {
        if(reloadPercentage == 1)
        {
            outline.enabled = true;
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
            slider.value = reloadPercentage;
            outline.enabled = false;
        }
    }
}
