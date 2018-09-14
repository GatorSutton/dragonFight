using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightAttack : MonoBehaviour {

    bool notHit = true;
    NotificationController nC;
    private Floor floor;
    public int snakeSize;
    List<Tile> snakeList = new List<Tile>();
    Animator anim;


    //attacks the players and starts back the dialog when done.
    public delegate void KnightAttackAction();
    public static event KnightAttackAction TaskComplete;

    // Use this for initialization
    void Start () {
        nC = GameObject.Find("Notification").GetComponent<NotificationController>();
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        anim = GetComponent<Animator>();
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
                snakeList.Add(floor.getTile(i % floor.sizeX, i / floor.sizeX));
            }
            else
            {
                snakeList.Add(floor.getTile(floor.sizeX - 1 - (i % floor.sizeX), i / floor.sizeX));
            }
        }       //add tiles to list in a snaking pattern
        

        do
        {
            notHit = true;
            StartCoroutine(moveSnake());
            yield return new WaitForSeconds(5f);
            StartCoroutine(moveSnake());
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(moveSnake());

            if (!notHit)
            {
                nC.flashMessage("TRY AGAIN");
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

    private IEnumerator moveSnake()
    {
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

            yield return new WaitForSeconds(.15f);
        }
    }



    
}
