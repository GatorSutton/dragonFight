
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour {

    public bool activeStatus = false;
    public int id;

    public virtual IEnumerator Attack()
    {
        return null;
    }

    public bool getStatus()
    {
        return activeStatus;
    }

    public IEnumerator checkForPotion()
    {
        if (GameObject.FindGameObjectWithTag("potion") != null)
        {
            yield return new WaitForSeconds(10f);
        }
    }

}
