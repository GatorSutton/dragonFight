using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpBarsController : MonoBehaviour {

    public GameObject dragonHPBar;
    public GameObject playerHPBar;

	// Use this for initialization
	void Start () {
        toggleHPBars();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void toggleHPBars()
    {
        dragonHPBar.SetActive(!dragonHPBar.activeSelf);
        playerHPBar.SetActive(!playerHPBar.activeSelf);
    }
}
