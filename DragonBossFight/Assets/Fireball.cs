using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public Transform gameCenter;
    public Transform warn;
    public Transform fire;

    public float targetScale;
    public float growFactor;
    public float waitTime;

    private float _currentScale = 1;
    private bool _upScale = true;

    void Update () {

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(Attack());
        }
	}

    private IEnumerator Attack()
    {
        //Move warn to the center
        warn.position = gameCenter.position;
        yield return null;
        //Move warn back
        warn.localPosition = new Vector3(0f, 0f, 0f);
        //Move fireball to the center
        fire.position = gameCenter.position;
        //grow and shrink the fireball
        while(_upScale)
        {
            if(_currentScale > targetScale)
            {
                _upScale = false;
                _currentScale = targetScale;
            }
            _currentScale *= growFactor;
            fire.localScale = Vector3.one * _currentScale;
            yield return new WaitForSeconds(waitTime);
        }

        //Move back to under the dragon
        fire.localPosition = new Vector3(0f, 0f, 0f);
        yield return null;

        _upScale = true;
        fire.localScale = new Vector3(2f, 2f, 2f);
    }
}
