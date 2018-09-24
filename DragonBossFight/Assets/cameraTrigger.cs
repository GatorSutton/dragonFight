using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTrigger : MonoBehaviour {
    private float cooldownTime = 2;
    private float cooldownTimer = 0;
    private CanvasController cC;

    // Use this for initialization
    void Start () {
        cC = transform.parent.gameObject.GetComponent<CanvasController>();
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dragon")
        {
            cC.updateCanvas();
        }

        if (other.name == "Cameras")
        {
            cC.frontView();
        }
    }

}
