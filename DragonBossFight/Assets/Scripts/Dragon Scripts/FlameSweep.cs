using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSweep : FireAttack {

    public Transform fire;
    public Transform warn;
    public float sweepSpeed;
    public float sweepRotation;

	// Update is called once per frame
	void Update ()
     {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
	}

    public override IEnumerator Attack()
    {
        activeStatus = true;
        bool secondWarning = false;
        warn.localPosition = new Vector3(-3.5f, 0f, 0f);
        yield return new WaitForSeconds(.5f);
        warn.localPosition = new Vector3(-10f, 0f, 0f);

        //fire sweep
        float timeLeft = 6/sweepSpeed;
        float t = -Mathf.PI / (2*sweepSpeed);
        while (timeLeft > 0)
        {

            if(secondWarning == false && timeLeft < 3/sweepSpeed)
            {
                secondWarning = true;
                warn.localPosition = new Vector3(3.5f, 0f, 0f);
                yield return new WaitForSeconds(.5f);
                warn.localPosition = new Vector3(-10f, 0f, 0f);
            }

            fire.transform.localRotation = Quaternion.Euler(new Vector3(90f, sweepRotation * Mathf.Sin(t*sweepSpeed), 0f));
            timeLeft -= Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
        activeStatus = false;
    }
}


