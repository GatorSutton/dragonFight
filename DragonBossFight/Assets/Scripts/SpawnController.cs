using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [HideInInspector]
    public float secondsPerRotation;

    private Floor floor;
    private targetController tC;
    private GameObject gameCenter;
    private GameObject FireHitBox;
    private GameObject WarnHitBox;

	// Use this for initialization
	void Start () {
        FireHitBox = transform.Find("FireHitBox").gameObject;
        WarnHitBox = transform.Find("WarnHitBox").gameObject;
        floor = GameObject.Find("Floor").GetComponent<Floor>();
        gameCenter = GameObject.FindGameObjectWithTag("center");
        tC = transform.GetComponentInChildren<targetController>();
        //StartCoroutine(moveToRandomSpot());
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent != null)
        {
            transform.RotateAround(transform.parent.transform.position, transform.parent.transform.TransformDirection(Vector3.back), Time.deltaTime * 360 / secondsPerRotation);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if(tC.dead == true)
        {
            Destroy(this.gameObject);
        }
	}

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tile")
        {
            StartCoroutine(kamikazee());
        }
    }
    */

    private IEnumerator kamikazee()
    {
        Destroy(transform.Find("Props_Skeleton_Skull").gameObject);
        WarnHitBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        WarnHitBox.SetActive(false);
        yield return new WaitForSeconds(2f);
        FireHitBox.SetActive(true);
        yield return new WaitForSeconds(3f);
        FireHitBox.transform.Translate(0f, -1000f, 0f);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        yield return null;
    }

    //pick random directions up/down/none left/right/none
    //move to towards that tile
    private Vector3 pickRandomSpot()
    {
        float[] offSetOptions = { -floor.sizeX/4, 0, floor.sizeX/4};
        float xOffset = offSetOptions[Random.Range(0, 3)];
        float zOffset = offSetOptions[Random.Range(0, 3)];

        Vector3 target = gameCenter.transform.position + new Vector3(xOffset, 0f, zOffset);
        print(target);
        return target;
    }

    public IEnumerator moveToRandomSpot(Vector3 spot)
    {
        transform.parent = null;
       // Vector3 randomSpot = pickRandomSpot();
        while (Vector3.Distance(transform.position, spot) >= .1f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, spot, Time.deltaTime * 5);
            yield return null;
        }
        StartCoroutine(kamikazee());
    }


    
}
