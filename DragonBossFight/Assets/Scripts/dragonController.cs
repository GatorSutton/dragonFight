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
	void Awake () {
        exitControl = GetComponent<dragonExitController>();
        attackControl = GetComponent<dragonAttackController>();
        enterControl = GetComponent<dragonEntranceController>();
	}
	
	// Update is called once per frame
	void Update () {

       
            
            switch(state)
            {
                case dragonState.enter:
                    {
                        enterControl.enabled = true;
                        attackControl.enabled = false;
                        exitControl.enabled = false;
                        break;
                    }
                case dragonState.attack:
                    {
                        enterControl.enabled = false;
                        attackControl.enabled = true;
                        exitControl.enabled = false;
                        break;
                    }
                case dragonState.exit:
                    {
                        enterControl.enabled = false;
                        attackControl.enabled = false;
                        exitControl.enabled = true;
                        break;
                    }
                
                case dragonState.wait:
                    {
                        enterControl.enabled = false;
                        attackControl.enabled = false;
                        exitControl.enabled = false;
                        break;
                    }
                    

        }
	}

    public void setState(dragonState currentState)
    {
        state = currentState;
    }

    public bool isActionComplete()
    {
        bool status = false;
        switch (state)
        {
            
            case dragonState.enter:
                {
                    status = enterControl.actionComplete;
                    break;
                }

            case dragonState.attack:
                {
                    status = (!attackControl.isAttacking());
                    
                    break;
                }

            case dragonState.exit:
                {
                    status = exitControl.actionComplete;
                    break;
                }

    }
    
        return status;
    }


}
