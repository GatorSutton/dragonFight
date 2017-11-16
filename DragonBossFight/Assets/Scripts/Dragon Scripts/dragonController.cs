using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonController : MonoBehaviour {

    public List<FireAttack> fireAttacks = new List<FireAttack>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            int index = Random.Range(0, 3);
            StartCoroutine(fireAttacks[index].Attack());
        }
    }


    private void fight()
    {
        
    }


}
