using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonAttackController : MonoBehaviour
{

    public Floor floor;

    public FireAttack flameSweep;
    public FireAttack fireBlast;
    public FireAttack fireBallThrower;
    public FireAttack flameWave;

    public List<FireAttack> fireAttacks = new List<FireAttack>();

    private FireAttack currentAttack;
    private SplineWalker SW;
    private Action action;
    private bool actionComplete;
    public bool attacking = true;

    public float vulnerableTime;
    private bool resting;
    private float restTimer;


    // Use this for initialization
    void Awake()
    {
        actionComplete = true;
        floor = GameObject.FindGameObjectWithTag("floor").GetComponent<Floor>();
        SW = GetComponent<SplineWalker>();
        action = Action.attack;
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

    private enum Action
    {
        attack,
        move,
        rest,
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
                    if (fireAttacks.Count == 1)
                    {
                        actionComplete = true;
                        action = Action.rest;
                    }
                    else
                    {
                        actionComplete = true;
                        action = Action.move;
                    }
                }
                break;
            case Action.rest:
                if(updateTimer() < 0)
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
       // fireAttacks.Add(fireBlast);
        //fireAttacks.Add(fireBallThrower);
        //fireAttacks.Add(flameWave);
    }

    private void attackController()
    {
        print(action);
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
                case Action.rest:
                    resetTimer();
                    break;
                default:
                    break;
            }
        }

        checkForCompleteAction();
        checkForEmptyAttackList();
    }

    private void OnEnable()
    {
        SW.assignPath("path");
        SW.setProgress(.5f);
        SW.setPosition(SplineWalker.Position.Front);
    }

    public bool isAttacking()
    {
        return (action == Action.attack);
    }

    private void resetTimer()
    {
        restTimer = vulnerableTime;
    }

    private float updateTimer()
    {
        return restTimer -= Time.deltaTime;
    }








}
