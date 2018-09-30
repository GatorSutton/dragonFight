using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour {

    Animator anim;
    Slider slider;
    float reloadPercentage;

    public List<GameObject> shots = new List<GameObject>();

    public float ReloadPercentage
    {
        set { reloadPercentage = value; }
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        slider = transform.GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        setReloadBar();
	}

    public void reticleFire()
    {
        anim.SetTrigger("Fire");
    }

    public void setShots(int numOfShotsLeft)
    {
        if (numOfShotsLeft == 3)
        {
            foreach(GameObject shot in shots)
            {
                shot.SetActive(true);
            }
        }
        else
        {
            shots[numOfShotsLeft].SetActive(false);
        }
    }

    private void setReloadBar()
    {
        if(Mathf.Approximately(reloadPercentage, 1) || Mathf.Approximately(reloadPercentage, 0))
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
            slider.value = reloadPercentage;
        }
    }
}
