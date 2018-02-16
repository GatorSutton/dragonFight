using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController : MonoBehaviour {

    public float shrinkSpeed;
    public float targetScale;
    public float startScale;
    private float vulnerableTime = 10;
    public bool start;

    Transform target;
    bool vulnerable = false;

	// Use this for initialization
	void Start () {
        target = transform.Find("target").transform;
        target.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(start)
        {
            start = false;
            StartCoroutine(shrinkTarget());
        }
	}

    private IEnumerator shrinkTarget()
    {
        target.gameObject.SetActive(true);
        target.localScale = new Vector3(3f, 3f, 1f);
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
    }

    public void startTarget()
    {
        StartCoroutine(shrinkTarget());
    }

    public void takeHit()
    {
        if(vulnerable)
        {
            target.gameObject.SetActive(false);
            print("hit motherfucker");
        }
    }
}
