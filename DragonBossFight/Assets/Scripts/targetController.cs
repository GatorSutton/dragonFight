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
    public List<GameObject> weakSpots = new List<GameObject>();

    float time;
    Transform target;
    bool vulnerable = false;
    bool finalTarget = false;
    int counter = 1;
    Transform center;
    AudioSource audioSource;
    NotificationController nC;
 

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
        target = transform.Find("target").transform;
        target.gameObject.SetActive(false);
        center = GameObject.FindGameObjectWithTag("center").transform;
        nC = GameObject.FindGameObjectWithTag("notification").GetComponent<NotificationController>();
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

    private IEnumerator shrinkFinalTarget()
    {
        nC.flashMessage("FINISH HIM");
        //set position at one of the four random on list
        finalTarget = true;
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

    public void startFinalTarget()
    {
        StopAllCoroutines();
        StartCoroutine(shrinkFinalTarget());
    }

    public void takeHit()
    {
        if (vulnerable)
        {
            if (!finalTarget)
            {
                audioSource.Play();
                target.gameObject.SetActive(false);
                dead = true;
            }
            else
            {
                audioSource.Play();
                nC.flashMessage("HIT X" + counter++);
                vulnerable = false;
                moveTarget();
            }
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

    private void moveTarget()
    {

        //add possiblie move locations
        //enable vulnerability at end of move
        //randomly selected one of the other move locations'
        //transform.position = Vector3.Lerp(currentPosition, nextPosition, t);
    }

    

}
