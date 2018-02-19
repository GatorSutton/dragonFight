using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCenterSpin : MonoBehaviour
{

    public float rotationSpeed;

    private Transform target;



    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("center").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

}