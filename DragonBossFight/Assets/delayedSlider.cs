using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class delayedSlider : MonoBehaviour {

    private Slider parentSlider;
    private Slider thisSlider;
    private bool draining = false;

	// Use this for initialization
	void Awake () {
        parentSlider = transform.parent.GetComponent<Slider>();
        thisSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!Mathf.Approximately(parentSlider.value,thisSlider.value) && !draining)
        {
            StartCoroutine(drainHP());
        }
	}

    private IEnumerator drainHP()
    {
        print(parentSlider.value);
        print(thisSlider.value);
        draining = true;
        float difference = thisSlider.value - parentSlider.value;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 100; i++)
        {
            thisSlider.value -= difference / 100;
            yield return new WaitForSeconds(.01f);
        }
        draining = false;

    }
}
