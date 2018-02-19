using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController : MonoBehaviour {

    public float rotateSpeed;
    public float shrinkSpeed;
    public float targetScale;
    public float startScale;
    private float vulnerableTime = 10;
    public bool start;
    private Vector3 rotatingVector;
    public bool dead;

    float time;
    Transform target;
    bool vulnerable = false;
    Transform center;

    // Use this for initialization
    void Start() {
        target = transform.Find("target").transform;
        target.gameObject.SetActive(false);
        center = GameObject.FindGameObjectWithTag("center").transform;
    }

    // Update is called once per frame
    void Update() {
        if (start)
        {
            start = false;
            StopAllCoroutines();
            StartCoroutine(shrinkTarget());
        }
        time += Time.deltaTime;
        rotatingVector = Mathf.Sin(time) * transform.up + Mathf.Cos(time) * transform.right;

        transform.LookAt(center);

    }

    private IEnumerator shrinkTarget()
    {
        target.gameObject.SetActive(true);
        target.localScale = new Vector3(startScale, startScale, 1f);
        while (target.localScale.x > targetScale)
        {
            float scale = Time.deltaTime * shrinkSpeed;
            target.localScale -= new Vector3(scale, scale, 0);
            yield return null;
        }
        vulnerable = true;
        yield return new WaitForSeconds(vulnerableTime);
        vulnerable = false;
        target.gameObject.SetActive(false);
        dead = false;
    }

    public void startTarget()
    {
        StopAllCoroutines();
        StartCoroutine(shrinkTarget());
    }

    public void takeHit()
    {
        if (vulnerable)
        {
            target.gameObject.SetActive(false);
            dead = true;
        }
    }

    public Vector3 getRotatingVector()
    {
        return rotatingVector;
    }

    public void reset()
    {
        dead = false;
    }
}
