using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallThrower : FireAttack {

    public FireBall fireball;
    public int numOfFireballs;
    public float timeBetweenAttacks;
    public Animator anim;

    private int fireballCount;
    private List<int> usedValues = null;

    private Floor floor;

    private void Start()
    {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        id = 4;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
    }

    public override IEnumerator Attack()
    {
        activeStatus = true;
        initList();
        anim.SetInteger("attack", id);
        while (fireballCount < numOfFireballs)
        {
            throwOneFireball(uniqueRandomNumber());
            fireballCount++;
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        fireballCount = 0;
        yield return new WaitForSeconds(9f);
        activeStatus = false;
    }

    private void initList()
    {
        usedValues = new List<int>(numOfFireballs);
        for (int i = 0; i < numOfFireballs; i++)
        {
            usedValues.Add(i);
        }
    }

    private void throwOneFireball(float position)
    {
        fireball.position = (position%(floor.sizeX)) - (floor.sizeX / 2f) + .5f;
        Instantiate(fireball, this.transform);
        fireball.floorLength = floor.sizeX;
    }

    private int uniqueRandomNumber()
    {
        int i = Random.Range(0, usedValues.Count-1);
        int temp = usedValues[i];
        usedValues.RemoveAt(i);
        return temp;
    }


}
