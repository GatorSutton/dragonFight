﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : FireAttack {

   
    public Transform warn;
    public Transform fire;
    public ParticleSystem ps;
    public Animator anim;
    public AudioSource audioSource;

    private ParticleSystem.EmissionModule em;
    private Vector3 gameCenter = new Vector3(0f, 0f, 0f);
    public float targetScale;
    public float initScale;
    public float growFactor;
    public float waitTime;

    private float _currentScale = 1f;
    private bool _upScale = true;

    private Floor floor;

    private void Start()
    {
        em = ps.emission;
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        targetScale = floor.sizeX - 3f;
        id = 3;
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(Attack());
        }
	}

    public override IEnumerator Attack()
    {
        anim.SetInteger("attack", id);
        audioSource.Play();
        em.enabled = true;
        activeStatus = true;
        _currentScale = initScale;
        //Move warn to the center
        warn.position = gameCenter;
        yield return new WaitForSeconds(.5f);
        //Move warn back
        warn.localPosition = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(1f);
        //Move fireball to the center
        fire.position = gameCenter;
        //grow fireball
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
        //wait 2 seconds as large fireball
        yield return new WaitForSeconds(2f);
        //shrink fireball
        while(!_upScale)
        {
            if(_currentScale < initScale)
            {
                _upScale = true;
                _currentScale = initScale;
            }
            _currentScale /= growFactor;
            fire.localScale = Vector3.one * _currentScale;
            yield return new WaitForSeconds(waitTime);
        }
        //Move back to under the dragon
        audioSource.Stop();
        fire.localPosition = new Vector3(0f, 0f, 0f);
        activeStatus = false;
        em.enabled = false;

    }

}
