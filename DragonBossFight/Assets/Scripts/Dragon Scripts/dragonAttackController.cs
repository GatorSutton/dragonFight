using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonAttackController : MonoBehaviour
{

    public Floor floor;
    public FireAttack[] allPossibleAttacks;
    public dragonHealth dH;

    public Animator anim;

    [SerializeField]
    private List<FireAttack> fireAttacks = new List<FireAttack>();
    private FireAttack currentAttack;
    private SplineWalker SW;
    private Action action;
    private bool actionComplete;
    public bool attacking = true;
    private PushBack pB;

    public float vulnerableTime;
    public bool resting = false;
    private float restTimer;
    private playerHealth pH;

    public delegate void AttackAction();
    public static event AttackAction attackBegin;
    public static event AttackAction attackEnd;


    // Use this for initialization
    void Awake()
    {
        pB = GetComponent<PushBack>();
        actionComplete = true;
        floor = GameObject.FindGameObjectWithTag("floor").GetComponent<Floor>();
        SW = GetComponent<SplineWalker>();
        action = Action.attack;
        restTimer = vulnerableTime;
        dH = GetComponent<dragonHealth>();
        pH = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
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
        attackBegin();                                                      //event
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
                    attackEnd();                                                //event

                    if (fireAttacks.Count == 0 && pH.HP > 0)
                    {
                        actionComplete = true;
                        action = Action.rest;
                        StartCoroutine(pB.pushWall(10f));
                        anim.speed = .2f;
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
        
        fireAttacks.Clear();
        
        foreach (FireAttack f in allPossibleAttacks)
         {
             fireAttacks.Add(f);
         }
         fireAttacks.RemoveAt(Random.Range(0, fireAttacks.Count));
         fireAttacks.RemoveAt(Random.Range(0, fireAttacks.Count));
        
        //fireAttacks.Add(allPossibleAttacks[4]);

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

    public int numOfAttacksLeft()
    {
        return fireAttacks.Count;
    }
    








}
