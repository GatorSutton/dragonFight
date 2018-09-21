  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Crossbow : MonoBehaviour
{

    public float coolDownTime;

    private Wiimote wiimote;
    private float cdRemaining;
    private float[] pointer;
    public float offsetRotation;
    public int wiiMoteNumber;

    // Use this for initialization
    void Start()
    {
        cdRemaining = coolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!WiimoteManager.HasWiimote()) { return; }


        wiimote = WiimoteManager.Wiimotes[wiiMoteNumber];
        pointer = wiimote.Ir.GetPointingPosition();


        if (wiimote.Button.b && cdRemaining < 0 && pointer[0] > -.5)
        {
            cdRemaining = coolDownTime;
            Debug.DrawRay(transform.position, calculateArrowVector() * 100, Color.green);

            RaycastHit hit;
            Ray ray = new Ray(transform.position, calculateArrowVector());

            if (Physics.Raycast(ray, out hit, 1000))
            {
                print(hit.collider.name);
                if (hit.collider.tag == "target")
                {
                    //hit.collider.transform.root.GetComponent<dragonHealth>().takeDamage();
                    hit.collider.gameObject.GetComponent<targetController>().takeHit();
                }
                if (hit.collider.name == "knight")
                {
                    hit.collider.gameObject.GetComponent<dialogController>().startDialog();
                }
            }
        }

        cdRemaining -= Time.deltaTime;





    }

    Vector3 calculateArrowVector()
    {
        //Vector3 vector = new Vector3((pointer[0] - .5f) * 2f * .736f, (pointer[1] - .5f) * 2f * .414f, 1f) - this.transform.position;    //scaled with the camera size change with variables to have 
        Vector3 vector = new Vector3((pointer[0] - .5f) * 2f * .736f, (pointer[1] - .5f) * 2f * .414f, 1f);
        vector = Quaternion.Euler(0, offsetRotation, 0) * vector;                                                                        //it work with different aspect ratios. Need aspect ratio to
        return vector;                                                                                                                //determine horizontal field of view angle. Then use that with 1
                                                                                                                                      //as adjacent triangle length to determine opposite for both vert
    }                                                                                                                                 // and horizontal to get the scalars.


}
