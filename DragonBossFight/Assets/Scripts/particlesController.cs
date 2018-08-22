using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlesController : MonoBehaviour {

    private ParticleSystem PS;

    // Use this for initialization
    void Start()
    {
        PS = this.GetComponentInChildren<ParticleSystem>();
        PS.Stop();
    }

        
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            PS.Stop();
            PS.Play();   
        }
	}

    public void spawnSparks()
    {
        PS.Stop();
        PS.Play();
    }
}

