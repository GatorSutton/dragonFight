using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    
    public enum States { NONE, WARN, FIRE};
    [System.NonSerialized]
    public States myState = States.NONE;
    public Material[] materials;
    public MeshRenderer rend;

    bool playerHere = false;

	// Use this for initialization
	void Start () {
        rend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        updateMaterial();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerHere = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerHere = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fire")
        {
            myState = States.FIRE;
        }

        if(other.tag == "warn")
        {
            flickerWarn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myState = States.NONE;
    }

    private void updateMaterial()
    {
        switch(myState)
        {
            case States.NONE:
                rend.material = materials[0];
                break;
            case States.FIRE:
                rend.material = materials[2];
                break;
            case States.WARN:
                rend.material = materials[3];
                break;
        }

        if(playerHere)
        {
            rend.material = materials[1];
        }
    }

    private IEnumerator flickerWarn()
    {
        WaitForSeconds wait = new WaitForSeconds(.5f);
        myState = States.WARN;
        yield return wait;
        myState = States.NONE;
        yield return wait;
        myState = States.WARN;
        yield return wait;
        myState = States.NONE;
        yield return wait;
        myState = States.WARN;
        yield return wait;
        myState = States.NONE;
    }

    
}
