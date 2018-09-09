using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerResponseController : MonoBehaviour {

    [SerializeField]
    private dialogController.ResponseDirection responseDirection;
    private TextMeshProUGUI playerText;
    private Slider slider;
    private floorButton fB;

    /*
    public delegate void responseAction(dialogController.ResponseDirection direction);
    public event responseAction ResponseChosen;
    */

    public delegate void responseAction(dialogController.ResponseDirection direction);
    public static event responseAction ResponseChosen;

    private void Awake()
    {
        playerText = this.GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
        fB = transform.Find("FloorButton").GetComponent<floorButton>();
    }

    // Use this for initialization
    void Start () {
  
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = fB.percentage;
        if (Mathf.Approximately(fB.percentage, 1f))
        {
            fB.percentage = 0;
            slider.value = 0;
            ResponseChosen(responseDirection);
        }
	}

    public void setSentence(string words)
    {
        playerText.text = words;
    }
}
