using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSweep : MonoBehaviour {

    public Transform warn;
    public float sweepSpeed;
    public float sweepRotation;

	// Update is called once per frame
	void Update ()
     {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Attack());
        }
	}

    private IEnumerator Attack()
    {
        float timeLeft = 6/sweepSpeed;
        float t = -Mathf.PI / (2*sweepSpeed);
        while (timeLeft > 0)
        {

            transform.localRotation = Quaternion.Euler(new Vector3(90f, sweepRotation * Mathf.Sin(t*sweepSpeed), 0f));
            timeLeft -= Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
    }
}
