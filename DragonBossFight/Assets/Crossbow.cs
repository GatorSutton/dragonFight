using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Crossbow : MonoBehaviour {

    public float coolDownTime;

    private Wiimote wiimote;
    private float cdRemaining;
    private float[] pointer;
    public float offsetRotation = 0;

    // Use this for initialization
    void Start () {
        cdRemaining = coolDownTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (!WiimoteManager.HasWiimote()) { return; }


        wiimote = WiimoteManager.Wiimotes[0];
        pointer = wiimote.Ir.GetPointingPosition();

        if (wiimote.Button.b && cdRemaining < 0 && pointer[0] > -.5)
        {
            cdRemaining = coolDownTime;
            Debug.DrawRay(transform.position, calculateArrowVector()*50, Color.green);

            RaycastHit hit;
            Ray ray = new Ray(transform.position, calculateArrowVector());

            if (Physics.Raycast(ray, out hit, 20))
            {
                // hit.collider.gameObject.GetComponent<dragonHealth>().takeDamage();
                if (hit.collider.tag == "headshot")
                {
                    hit.collider.transform.root.GetComponent<dragonHealth>().takeDamage();
                }
            }
        }

        cdRemaining -= Time.deltaTime;



    }

    Vector3 calculateArrowVector()
    {

        Vector3 vector = new Vector3(pointer[0] - .5f, pointer[1] - .5f, 1f) - this.transform.position;
        vector = Quaternion.Euler(0, offsetRotation, 0) * vector;
        return vector;
 
    }


}
