using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogController : MonoBehaviour {

    //controls the knights questions and the players responses

    private GameObject knightResponse;
    private TextMeshProUGUI knightText;

	// Use this for initialization
	void Start () {
        knightResponse = GameObject.Find("KnightResponse");
        knightText = knightResponse.GetComponentInChildren<TextMeshProUGUI>();
        setSentence("Oh hello there. What brings you to the castle underground?");
        
	}

    private void setSentence(string words)
    {
        knightText.text = words;
    }
}
