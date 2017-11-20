using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonController : MonoBehaviour
{

    public Floor floor;

    public FireAttack flameSweep;
    public FireAttack fireBlast;
    public FireAttack fireBallThrower;
    public FireAttack flameWave;

    public List<FireAttack> fireAttacks = new List<FireAttack>();

    private FireAttack currentAttack;
    private SplineWalker SW;

    private float waitTime = 10f;
    private Action action;
    private bool actionComplete = true;


    // Use this for initialization
    void Start()
    {
        SW = GetComponent<SplineWalker>();
        action = Action.wait;
        resetAttacks();

    }

    // Update is called once per frame
    void Update()
    {

        if (actionComplete)
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
        checkForEmptyAttackList();
    }

    private void changePosition()
    {
        SW.setRandomPosition();
    }

    private void attack()
    {
        int index = Random.Range(0, fireAttacks.Count);
        currentAttack = fireAttacks[index];
        StartCoroutine(currentAttack.Attack());
        fireAttacks.RemoveAt(index);
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
                if (SW.isPositionReached())
                {
                    actionComplete = true;
                    action = Action.attack;
                }
                break;
            case Action.attack:
                if (!currentAttack.getStatus())
                {
                    actionComplete = true;
                    action = Action.move;
                }
                break;
            case Action.wait:
                if(floor.isPlayerOnAnyTile())
                {
                    actionComplete = true;
                    action = Action.move;
                }
                break;
            default:
                break;
        }
    }

    private void checkForEmptyAttackList()
    {
        if (fireAttacks.Count == 0)
        {
            resetAttacks();
        }
    }

    private void resetAttacks()
    {
        fireAttacks.Add(flameSweep);
        fireAttacks.Add(fireBlast);
        fireAttacks.Add(fireBallThrower);
        fireAttacks.Add(flameWave);
    }



}
