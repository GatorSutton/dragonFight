using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonController : MonoBehaviour {

    public List<FireAttack> fireAttacks = new List<FireAttack>();
    private FireAttack currentAttack;
    private SplineWalker SW;

    private Action action;
    private bool actionComplete = true;
    

	// Use this for initialization
	void Start () {
        SW = GetComponent<SplineWalker>();
        action = Action.attack;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(actionComplete)
        {
            actionComplete = false;
            switch (action)
            {
                case Action.move:
                    changePosition();
                    break;
                case Action.attack:
                    attack();
                    break;
                default:
                    break;
            }
        }

        checkForCompleteAction();
    }

    private void changePosition()
    {
        SW.setRandomPosition();
    }

    private void attack()
    {
        currentAttack = fireAttacks[Random.Range(0, fireAttacks.Count)];
        StartCoroutine(currentAttack.Attack());
    }

    private enum Action
    {
        attack,
        move,
        wait
    }

    private void checkForCompleteAction()
    {
        switch (action)
        {
            case Action.move:
                if(SW.isPositionReached())
                {
                    actionComplete = true;
                    action = Action.attack;
                }
                break;
            case Action.attack:
                if(!currentAttack.getStatus())
                {
                    actionComplete = true;
                    action = Action.move;
                }
                break;
            default:
                break;
        }
    }



}
