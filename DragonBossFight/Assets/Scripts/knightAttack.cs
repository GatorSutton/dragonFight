using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightAttack : MonoBehaviour {

    public GameObject throwingSnakePrefab;
    public Transform throwingHand;


    private float snakeSpeed;
    bool notHit = true;
    NotificationController nC;
    private Floor floor;
    private int snakeSize;
    List<Tile> snakeList = new List<Tile>();
    Animator anim;


    //attacks the players and starts back the dialog when done.
    public delegate void KnightAttackAction();
    public static event KnightAttackAction TaskComplete;

    // Use this for initialization
    void Start () {
        snakeSpeed = .25f;
        nC = GameObject.Find("Notification").GetComponent<NotificationController>();
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        anim = GetComponent<Animator>();
        snakeSize = floor.sizeX / 2;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine("snakeSwing");
        }
	}

    private void OnEnable()
    {
        Tile.OnHit += playersHit;
    }

    private void OnDisable()
    {
        Tile.OnHit += playersHit;
    }

    private void playersHit()
    {
        print("player hit");
        notHit = false;
    }

    private IEnumerator snakeSwing()
    {

        for (int i = 0; i < floor.sizeX * floor.sizeZ; i++)
        {
            if (i / floor.sizeX % 2 == 0)
            {
                snakeList.Add(floor.getTile(i / floor.sizeX, i % floor.sizeX));
            }
            else
            {
                snakeList.Add(floor.getTile(i / floor.sizeX, floor.sizeX - 1 - (i % floor.sizeX)));
            }
        }       //add tiles to list in a snaking pattern




        do
        {
            notHit = true;
            StartCoroutine(moveSnake(snakeSpeed));
            yield return new WaitForSeconds(8f);
            StartCoroutine(moveSnake(snakeSpeed));
            yield return new WaitForSeconds(8f);
            yield return StartCoroutine(moveSnake(snakeSpeed));

            if (!notHit)
            {
                nC.flashMessage("TRY AGAIN");
                yield return new WaitForSeconds(2f);
                snakeSpeed += .05f;
                nC.flashMessage("Slower Snakes");
                yield return new WaitForSeconds(2f);
            }
            else
            {
                nC.flashMessage("Noice");
            }
        } while (!notHit);


        yield return new WaitForSeconds(5f);
        TaskComplete();
    }



    public void startSnakeSwing()
    {

        StartCoroutine("snakeSwing");
    }

    private IEnumerator moveSnake(float snakeSpeed)
    {
        for (int i = 0; i < snakeSize; i++)
        {
           StartCoroutine(snakeList[i].flickerWarn());
        }                       //Warn the snake entrance
        yield return new WaitForSeconds(3f);

        anim.SetTrigger("Swing");
        for (int i = 0; i < snakeList.Count + snakeSize; i++)
        {
            if (i < snakeList.Count)
            {
                snakeList[i].myState = Tile.States.FIRE;
            }
            if (i - snakeSize >= 0)
            {
                snakeList[i - snakeSize].myState = Tile.States.NONE;
            }

            yield return new WaitForSeconds(snakeSpeed);
        }
    }

    public void throwObject()
    {
        GameObject snake = Instantiate(throwingSnakePrefab, throwingHand.position, Quaternion.Euler(0, 90, 0));
        Rigidbody rb = snake.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(250f, 0, 0));
        Destroy(snake, 5f);
        

    }



    
}
