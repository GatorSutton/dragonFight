using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonController : MonoBehaviour {

    public List<FireAttack> fireAttacks = new List<FireAttack>();
    private SplineWalker SW;
    

	// Use this for initialization
	void Start () {
        SW = GetComponent<SplineWalker>();
	}
	
	// Update is called once per frame
	void Update () {
            if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            int index = Random.Range(0, 3);
            StartCoroutine(fireAttacks[index].Attack());
        }
        
            if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SW.setRandomPosition();
        }



    }




}
