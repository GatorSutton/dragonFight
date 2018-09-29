using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetFlash : MonoBehaviour {

    SpriteRenderer sR;
    public Color startColor;
    public Color flashColor;

	// Use this for initialization
	void Start () {
        sR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator flash()
    {
        float elapsedTime = 0;
        sR.color = flashColor;

        while (elapsedTime < .25)
        {
            sR.color = Color.Lerp(flashColor, startColor, elapsedTime*4);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
