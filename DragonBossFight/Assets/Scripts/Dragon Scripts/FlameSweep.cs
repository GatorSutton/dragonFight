using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSweep : FireAttack {

    public Transform fire;
    public Transform warn;
    public float sweepSpeed;
    public float sweepRotation;
    public Animator anim;
    public Transform dragon;
    public float rotateSpeed;

    private Floor floor;
    private float firstWarnPosition;
    private float secondWarnPosition;

    private void Start()
    {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        firstWarnPosition = -(floor.sizeX * .5f) + .5f;
        secondWarnPosition = (floor.sizeX * .5f) - .5f;
        id = 2;
    }

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

        //Spin the dragon around
        while (dragon.localEulerAngles.y < 180  || dragon.localEulerAngles.y > 350)
        {
            dragon.Rotate(0, Time.deltaTime * 60, 0, Space.Self);
            yield return null;
        }

        anim.SetInteger("attack", id);
        warn.localPosition = new Vector3(firstWarnPosition, 0f, 0f);
        yield return new WaitForSeconds(.5f);
        warn.localPosition = new Vector3(-10f, 0f, 0f);

        //fire sweep
        float timeLeft = 6/sweepSpeed;
        float t = -Mathf.PI / (2*sweepSpeed);
        while (timeLeft > 0)
        {

            if(secondWarning == false && timeLeft < 3/sweepSpeed)
            {
                anim.SetBool("nextTailWhip", true);
                dragon.localScale = new Vector3(-1, 1, 1);
                secondWarning = true;
                warn.localPosition = new Vector3(secondWarnPosition, 0f, 0f);
                yield return new WaitForSeconds(.5f);
                warn.localPosition = new Vector3(-10f, 0f, 0f);
            }

            fire.transform.localRotation = Quaternion.Euler(new Vector3(90f, sweepRotation * Mathf.Sin(t*sweepSpeed), 0f));
            timeLeft -= Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }

        //Unspin the dragon
        dragon.localScale = new Vector3(1, 1, 1);
        while (dragon.localEulerAngles.y < 185)
        {
            dragon.Rotate(0, -Time.deltaTime * 60, 0, Space.Self);
            yield return null;
        }

        anim.SetBool("nextTailWhip", false);
        activeStatus = false;
    }

}


