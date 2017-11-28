using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonController : MonoBehaviour {

    private dragonExitController exitControl;
    private dragonAttackController attackControl;
    private dragonEntranceController enterControl;

    public enum dragonState
    {
        enter, 
        attack,
        exit,
        wait
    }

    public dragonState state = dragonState.enter;

	// Use this for initialization
	void Start () {
        exitControl = GetComponent<dragonExitController>();
        attackControl = GetComponent<dragonAttackController>();
        enterControl = GetComponent<dragonEntranceController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H))
        {
            
            switch(state)
            {
                case dragonState.enter:
                    {
                        enterControl.enabled = true;
                        attackControl.enabled = false;
                        exitControl.enabled = false;
                        state = dragonState.attack;
                        break;
                    }
                case dragonState.attack:
                    {
                        enterControl.enabled = false;
                        attackControl.enabled = true;
                        exitControl.enabled = false;
                        state = dragonState.exit;
                        break;
                    }
                case dragonState.exit:
                    {
                        enterControl.enabled = false;
                        attackControl.enabled = false;
                        exitControl.enabled = true;
                        state = dragonState.enter;
                        break;
                    }
                case dragonState.wait:
                    {
                        enterControl.enabled = false;
                        attackControl.enabled = false;
                        exitControl.enabled = false;
                        state = dragonState.wait;
                        break;
                    }

            }
        }
	}

    public void setState(dragonState currentState)
    {
        state = currentState;
    }


}
