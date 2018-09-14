using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerResponseController : MonoBehaviour {

    public floorButton fBPrefab;
    public Floor floor;

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
        spawnFloorButton();
        playerText = this.GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        fB.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        fB.gameObject.SetActive(false);
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

    private void spawnFloorButton()
    {
        if (responseDirection == dialogController.ResponseDirection.left)
        {
            fB = Instantiate(fBPrefab, new Vector3(0f, 0f, floor.sizeX/2), Quaternion.identity, floor.transform);
        }
        else
        {
            fB = Instantiate(fBPrefab, new Vector3(0f, 0f, -floor.sizeX / 2), Quaternion.identity, floor.transform);
        }
    }
}
