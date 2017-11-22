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
    public Action action;
    private bool actionComplete = true;

    public bool attacking = true;


    // Use this for initialization
    void Start()
    {
        floor = GameObject.FindGameObjectWithTag("floor").GetComponent<Floor>();
        SW = GetComponent<SplineWalker>();
        action = Action.wait;
        resetAttacks();

    }

    // Update is called once per frame
    void Update()
    {
            attackController();
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

    public enum Action
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

    private void attackController()
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
                case Action.wait:
                    floor.setStartingTiles();
                    break;
                default:
                    break;
            }
        }

        checkForCompleteAction();
        checkForEmptyAttackList();
    }


    




}
