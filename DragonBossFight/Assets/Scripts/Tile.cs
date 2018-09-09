using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public dragonHealth DH;
    public playerHealth PH;

    public float timeBetweenFlicker;
    public enum States { NONE, WARN, FLICKEROFF, FIRE, DAMAGE, SWITCH, FAKEFIRE, SELECTOR};
    [System.NonSerialized]
    public States myState = States.NONE;
    public Material[] materials;
    public MeshRenderer rend;

    public bool playerHere = false;
    bool warning = false;
    bool fire = false;
    bool takingDamage = false;

    public delegate void TileAction();
    public static event TileAction OnHit;

    // Use this for initialization
    void Start () {
        rend = GetComponent<MeshRenderer>();
       // DH = GameObject.FindGameObjectWithTag("dragon").GetComponent<dragonHealth>();
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        updateMaterial();
        checkForPlayerOnFire();
        checkForPlayerOnSwitch();
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
        if(other.tag == "fire"  && !takingDamage)
        {
            fire = true;
            myState = States.FIRE;
        }

        if(other.tag == "warn" && !warning)
        {
            StartCoroutine(flickerWarn());
        }

        if(other.tag == "fakefire")
        {
            myState = States.FAKEFIRE;
        }

        if(other.tag == "selector")
        {
            myState = States.SELECTOR;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fire")
        {
            fire = false;
            myState = States.NONE;
        }

        if (other.tag == "fakefire")
        {
            myState = States.NONE;
        }

        if (other.tag == "selector")
        {
            myState = States.NONE;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "fire" && !takingDamage)
        {
                fire = true;
                myState = States.FIRE;
        }

        if(other.tag == "warn" && !warning)
        {
            StartCoroutine(flickerWarn());
        }
    }

    private void updateMaterial()
    {
        switch(myState)
        {
            case States.NONE:
                rend.material = materials[0];
                break;
            case States.FIRE:
                rend.material = materials[1];
                break;
            case States.WARN:
                rend.material = materials[2];
                break;
            case States.DAMAGE:
                rend.material = materials[3];
                break;
            case States.SWITCH:
                rend.material = materials[4];
                break;
            case States.FLICKEROFF:
                rend.material = materials[0];
                break;
            case States.FAKEFIRE:
                rend.material = materials[1];
                break;
            case States.SELECTOR:
                rend.material = materials[6];
                break;
        }
        
        if(playerHere && myState != States.SELECTOR)
        {
            rend.material = materials[5];
        }
        
    }

    private IEnumerator flickerWarn()
    {
        warning = true;
        WaitForSeconds wait = new WaitForSeconds(timeBetweenFlicker);
        myState = States.WARN;
        yield return wait;
        myState = States.FLICKEROFF;
        yield return wait;
        myState = States.WARN;
        yield return wait;
        myState = States.FLICKEROFF;
        yield return wait;
        myState = States.WARN;
        yield return wait;
        myState = States.NONE;
        yield return wait;
        warning = false;
    }

    private void checkForPlayerOnFire()
    {
        if(playerHere && fire && !takingDamage)
        {
            StartCoroutine(flickerDamage());
            PH.takeDamage();
            OnHit();
        }
    }

    private IEnumerator flickerDamage()
    {
        takingDamage = true;
        WaitForSeconds wait = new WaitForSeconds(timeBetweenFlicker);
        myState = States.DAMAGE;
        yield return wait;
        myState = States.FLICKEROFF;
        yield return wait;
        myState = States.DAMAGE;
        yield return wait;
        myState = States.FLICKEROFF;
        yield return wait;
        myState = States.DAMAGE;
        yield return wait;
        myState = States.NONE;
        takingDamage = false;
    }

    public bool isPlayerHere()
    {
        return playerHere;
    }

    private void checkForPlayerOnSwitch()
    {
        if(playerHere && myState == States.SWITCH)
        {
            myState = States.NONE;
        }
    }

}
