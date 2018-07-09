

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class WiimoteTest : MonoBehaviour {

    public RectTransform ir_pointer;
    public RectTransform ir_pointer2;
    public RectTransform ir_pointer3;
    public RectTransform ir_pointer4;

    private Wiimote wiimote;
    private Wiimote wiimote2;
    private Wiimote wiimote3;
    private Wiimote wiimote4;
    private bool init = true;
    private Vector3 wmpOffset = Vector3.zero;

    // Use this for initialization
    void Start () {
        WiimoteManager.FindWiimotes();
    }
	
	// Update is called once per frame
	void Update () {
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];
        wiimote2 = WiimoteManager.Wiimotes[1];
        wiimote3 = WiimoteManager.Wiimotes[2];
        wiimote4 = WiimoteManager.Wiimotes[3];

        if(init)
        {
            init = false;
            wiimote.SetupIRCamera(IRDataType.BASIC);
            wiimote2.SetupIRCamera(IRDataType.BASIC);
            wiimote3.SetupIRCamera(IRDataType.BASIC);
            wiimote4.SetupIRCamera(IRDataType.BASIC);
        }

        int ret, ret2, ret3, ret4;
        do
        {
            ret = wiimote.ReadWiimoteData();
            ret2 = wiimote2.ReadWiimoteData();
            ret3 = wiimote3.ReadWiimoteData();
            ret4 = wiimote4.ReadWiimoteData();
        } while (ret > 0  || ret2 > 0 || ret3 > 0 || ret4 > 0);
        

        float[] pointer = wiimote.Ir.GetPointingPosition();
        ir_pointer.anchorMin = new Vector2(pointer[0], pointer[1]);
        ir_pointer.anchorMax = new Vector2(pointer[0], pointer[1]);


        float[] pointer2 = wiimote2.Ir.GetPointingPosition();
        ir_pointer2.anchorMin = new Vector2(pointer2[0], pointer2[1]);
        ir_pointer2.anchorMax = new Vector2(pointer2[0], pointer2[1]);

        float[] pointer3 = wiimote3.Ir.GetPointingPosition();
        ir_pointer3.anchorMin = new Vector2(pointer3[0], pointer3[1]);
        ir_pointer3.anchorMax = new Vector2(pointer3[0], pointer3[1]);

        float[] pointer4 = wiimote4.Ir.GetPointingPosition();
        ir_pointer4.anchorMin = new Vector2(pointer4[0], pointer4[1]);
        ir_pointer4.anchorMax = new Vector2(pointer4[0], pointer4[1]);
    }

    void OnApplicationQuit()
    {
        if (wiimote != null)
        {
            WiimoteManager.Cleanup(wiimote);
            wiimote = null;
        }

        if (wiimote2 != null)
        {
            WiimoteManager.Cleanup(wiimote);
            wiimote2 = null;
        }

        if (wiimote3 != null)
        {
            WiimoteManager.Cleanup(wiimote);
            wiimote3 = null;
        }

        if (wiimote4 != null)
        {
            WiimoteManager.Cleanup(wiimote);
            wiimote4 = null;
        }
    }
}
