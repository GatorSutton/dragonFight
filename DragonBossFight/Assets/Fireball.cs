using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public Transform gameCenter;
    public Transform warn;
    public Transform fire;
    public float warnTicks;

    private float _currentScale;
    public float targetScale = 2f;
    public float initScale = 1f;
    private const float animationTimeSeconds = 2;
    private float _dt = 0;
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
        //Flicker Warn

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
            fire.localScale = Vector3.one * (targetScale / animationTimeSeconds);
            yield return null;
        }

        while(!_upScale)
        {
            if(_currentScale < initScale)
            {
                   
            }
        }
        //Move back to under the dragon


        yield return null;
    }
}
