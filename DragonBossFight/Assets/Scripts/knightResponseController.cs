using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class knightResponseController : MonoBehaviour {

    //controls the knights questions and the players responses

    private GameObject knightResponse;
    private TextMeshProUGUI knightText;

	// Use this for initialization
	void Awake () {
        knightResponse = GameObject.Find("KnightResponse");
        knightText = knightResponse.GetComponentInChildren<TextMeshProUGUI>();
	}

    public void setSentence(string words)
    {
        knightText.text = words;
    }
}
