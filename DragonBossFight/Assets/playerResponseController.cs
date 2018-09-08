using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerResponseController : MonoBehaviour {

    private TextMeshProUGUI playerText;
    private Slider slider;
    private floorButton fB;

    // Use this for initialization
    void Start () {
        playerText = this.GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
        setSentence("testing");
        //fB = GameObject.Find("FloorButton").GetComponent<floorButton>();
        fB = transform.Find("FloorButton").GetComponent<floorButton>();
        
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = fB.percentage;
	}

    private void setSentence(string words)
    {
        playerText.text = words;
    }
}
