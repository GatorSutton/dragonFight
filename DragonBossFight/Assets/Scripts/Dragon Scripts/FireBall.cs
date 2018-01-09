using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public Transform warn;
    public Transform fire;
    private float distanceToFirstSquare;
    private float distanceToLastSquare;
    public float speed;

    private Vector3 homePosition = new Vector3(0f, 0f, 0f);
    public float position;
    public float floorLength;

	// Use this for initialization
	void Awake () {
        distanceToFirstSquare = 10 - (floorLength / 2f) + .5f;
        distanceToLastSquare = 10 + (floorLength / 2f) - .5f;
        StartCoroutine(Attack());
    }
	

    private IEnumerator Attack()
    {
        Vector3 startingPosition = new Vector3(position, 0f, 0f);
        //Set warn and fire to initial position
        warn.localPosition = startingPosition + new Vector3(0f, 0f, distanceToFirstSquare);
        fire.localPosition = startingPosition;
        //Move warn to first square and then reset
        yield return new WaitForSeconds(.5f);
        warn.localPosition = homePosition;
        //Move fireball through all square and back
        while (fire.localPosition.z < distanceToLastSquare)
        {
            fire.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            print(Vector3.forward * Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2f);
        fire.localPosition = homePosition;
        Destroy(gameObject, 1);
    }

    public void setStartPosition(int random)
    {
        position = random;
    }
}
