using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallThrower : FireAttack {

    public FireBall fireball;
    public int numOfFireballs;
    public float timeBetweenAttacks;

    private int fireballCount;
    private List<int> usedValues = null;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        initList();
        while(fireballCount < numOfFireballs)
        {
            throwOneFireball(uniqueRandomNumber());
            fireballCount++;
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        fireballCount = 0;
    }

    private void initList()
    {
        usedValues = new List<int>(8);
        for (int i = 0; i < 8; i++)
        {
            usedValues.Add(i);
        }
    }

    private void throwOneFireball(int position)
    {
        fireball.position = position;
        Instantiate(fireball, this.transform);
    }

    private int uniqueRandomNumber()
    {
        int i = Random.Range(0, usedValues.Count-1);
        int temp = usedValues[i];
        usedValues.RemoveAt(i);
        print(temp);
        return temp;
    }
}
