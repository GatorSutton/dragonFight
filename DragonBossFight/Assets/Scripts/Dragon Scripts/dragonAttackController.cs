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

    public dragonHealth dH;

    public List<FireAttack> fireAttacks = new List<FireAttack>();
    public Animator anim;

    private FireAttack currentAttack;
    private SplineWalker SW;
    private Action action;
    private bool actionComplete;
    public bool attacking = true;

    public float vulnerableTime;
    public bool resting = false;
    private float restTimer;


    // Use this for initialization
    void Awake()
    {
        actionComplete = true;
        floor = GameObject.FindGameObjectWithTag("floor").GetComponent<Floor>();
        SW = GetComponent<SplineWalker>();
        action = Action.attack;
        restTimer = vulnerableTime;
        dH = GetComponent<dragonHealth>();
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
                    if (fireAttacks.Count == 0)
                    {
                        actionComplete = true;
                        action = Action.rest;
                        anim.speed = .2f;
                        dH.setTargets();
                        print("resting");
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
                    resting = false;
                    action = Action.move;
                    anim.speed = 1f;
                }
                break;
            default:
                break;
        }
    }

    private void resetAttacks()
    {
     //  fireAttacks.Add(flameSweep);
     //  fireAttacks.Add(fireBlast);
     //  fireAttacks.Add(fireBallThrower);
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
                case Action.rest:
                    resetTimer();
                    resetAttacks();
                    break;
                default:
                    break;
            }
        }

        checkForCompleteAction();
    }

    private void OnEnable()
    {
        SW.assignPath("path");
        SW.setProgress(.5f);
        SW.setPosition(SplineWalker.Position.Front);
        resetAttacks();
        action = Action.attack;
    }

    private void OnDisable()
    {
        
    }


    public bool isAttacking()
    {
        return (action == Action.attack);
    }

    private void resetTimer()
    {
        restTimer = vulnerableTime;
        resting = true;
    }

    private float updateTimer()
    {
        return restTimer -= Time.deltaTime;
    }
    
    public bool isResting()
    {
        return (action == Action.rest);
    }








}
