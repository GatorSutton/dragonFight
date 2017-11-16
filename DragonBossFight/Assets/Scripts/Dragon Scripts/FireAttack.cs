
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour {

    public bool activeStatus = false;

    public virtual IEnumerator Attack()
    {
        return null;
    }

    public bool getStatus()
    {
        return activeStatus;
    }

}
