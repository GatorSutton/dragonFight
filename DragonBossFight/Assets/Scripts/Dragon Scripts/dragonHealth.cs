using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragonHealth : MonoBehaviour {

    public Animator anim;
    public int startingHealth;
    public List<targetController> targets = new List<targetController>();
    public AudioClip audioHit;
    public AudioClip audioDead;
    public AudioClip audioSlow;
    public NotificationController nC;
    public float finishHimTime;
    public targetController weakPointPrefab;

    private Slider healthBar;
    private AudioSource audioSource;
    ScoreController sC;

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
    }

    private dragonAttackController dAC;
    public Material material;

    public delegate void HealthAction();
    public static event HealthAction takeHit;

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
        hp = startingHealth;
       dAC = GetComponent<dragonAttackController>();
        healthBar = GameObject.Find("DragonHPBar").GetComponent<Slider>();
        nC = GameObject.FindGameObjectWithTag("notification").GetComponent<NotificationController>();
        sC = GameObject.Find("Score").GetComponent<ScoreController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            takeDamage(100);
        }

        checkAllTargets();

    }

    public void healToFull()
    {
        hp = startingHealth;
        setHealthBarSlider();
    }

    public void takeDamage(int amount)
    {
        hp = hp - amount;
        audioSource.clip = audioHit;
        if (hp > 0)
        {
            audioSource.Play();
            anim.SetTrigger("hit");
            anim.speed = 1;
        }
        print(hp);
        setHealthBarSlider();

        if(hp <= 0)
        {
            StartCoroutine(finishHim());
            //anim.SetTrigger("dead");
            //audioSource.clip = audioHit;
            //audioSource.Play();
        }
    }
    
    public void setTargets()
    {
        audioSource.clip = audioSlow;
        audioSource.Play();
        foreach(var target in targets)
        {
            target.startTarget();
        }
    }

    private void checkAllTargets()
    {
        float count = 0;
        foreach(var target in targets)
        {
            if(target.dead)
            {
                count++;
            }

        }
        if (count == targets.Count)
        {
            resetTargets();
            takeHit();
            takeDamage(100);
        }

    }

    private void resetTargets()
    {
        foreach(var target in targets)
        {
            target.reset();
        }
    }

    private void setHealthBarSlider()
    {
        healthBar.value = ((float)hp / (float)startingHealth);
    }
    
    
    private IEnumerator finishHim()
    {
        {
            //make player invulnerable
            // remove all other weakpoints from scene
            var oldTargets = GameObject.FindGameObjectsWithTag("target");
            print(oldTargets.Length);
            foreach(var target in oldTargets)
            {
                print(target.gameObject.name);
                Destroy(target);
            }


            audioSource.clip = audioSlow;
            audioSource.Play();
            anim.speed = .2f;
            targetController finalTarget = Instantiate(weakPointPrefab);
            finalTarget.startFinalTarget();
            //starting the final target should set its parent to one of the weakpoints in the list on the targetControllers ... moving should reparent and home in

            yield return new WaitForSeconds(finishHimTime);
            nC.flashMessage(buildMessage(finalTarget.counter));
            sC.Score += finalTarget.counter * 1000;
            // show a counter through notification
            //circle the floor with fire for cool effect
        }
        anim.SetTrigger("dead");
        // fall until hit the ground
    }

    private string buildMessage(int hits)
    {
        string message = hits.ToString() + " HITS!";
        return message;
       
    }
    





}
