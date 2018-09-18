using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private particlesController PC;
    private int position = 0;
    float time;
    Transform target;
    bool vulnerable = false;
    bool finalTarget = false;
    public int counter = 0;
    Transform center;
    AudioSource audioSource;
    NotificationController nC;
    SphereCollider sC;

    public delegate void TargetAction();
    public static event TargetAction TargetShot;
    public static event TargetAction TargetMissed;

    private void Awake()
    {
        sC = GetComponent<SphereCollider>();
        PC = this.GetComponentInChildren<particlesController>();
        nC = GameObject.FindGameObjectWithTag("notification").GetComponent<NotificationController>();
        weakSpots = GameObject.FindGameObjectsWithTag("weakParent").ToList();
        audioSource = GetComponent<AudioSource>();
        target = transform.Find("target").transform;
        //target.gameObject.SetActive(false);
        targetOff();
        center = GameObject.FindGameObjectWithTag("center").transform;
    }

    // Use this for initialization
    void Start() {

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
        dead = false;
        // target.gameObject.SetActive(true);
        targetOn();
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
        //target.gameObject.SetActive(false);
        targetOff();
        if(!dead)
        {
            TargetMissed?.Invoke();
        }
        print("just dissapated");
    }

    private IEnumerator shrinkFinalTarget()
    {
        nC.flashMessage("FINISH HIM");
        //set position at one of the four random on list
        print(weakSpots.Count);
        transform.SetParent(weakSpots[position].transform);
        transform.localPosition = new Vector3(0f, 0f, 0f);
        finalTarget = true;
       // target.gameObject.SetActive(true);
        targetOn();
        target.localScale = new Vector3(startScale, startScale, 1f);
        while (target.localScale.x > targetScale)
        {
            float scale = Time.deltaTime * shrinkSpeed;
            target.localScale -= new Vector3(scale, scale, 0);
            yield return null;
        }
        vulnerable = true;
        yield return new WaitForSeconds(20f);
        vulnerable = false;
        //target.gameObject.SetActive(false);
        targetOff();
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
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (!finalTarget)
            {
                if (!dead)
                {
                    target.gameObject.SetActive(false);
                    dead = true;
                    if (TargetShot != null)
                    {
                        TargetShot();
                    }
                }
            }
            else
            {
                StartCoroutine(moveTarget());
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

    private IEnumerator moveTarget()
    {
        counter++;
        PC.spawnSparks();
        //nC.flashMessage(counter++.ToString());
        vulnerable = false;
        position = differentRandomPosition(position);
        transform.SetParent(weakSpots[position].transform);            //finds a different random weakpoint
        //transform.localPosition = new Vector3(0f, 0f, 0f);
        
        var startPosition = transform.localPosition;
        float t = 0;

        while (t < 1)
        {
            transform.localPosition = Vector3.Lerp(startPosition, new Vector3(0f, 0f, 0f), t);
            t += Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = new Vector3(0f, 0f, 0f);
        vulnerable = true;
    }

    private int differentRandomPosition(int current)        //pick a position that the target is not currently at
    {
        var list = new List<int> { 0, 1, 2, 3 };
        list.Remove(current);
        return list[Random.Range(0, 3)];    
    }

    private void targetOff()
    {
        target.gameObject.SetActive(false);
        sC.enabled = false;
        
    }

    private void targetOn()
    {
        target.gameObject.SetActive(true);
        sC.enabled = true;

    }





}
