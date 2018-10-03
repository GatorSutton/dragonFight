using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCubeController : MonoBehaviour {

    public int startingHP;
    public float lifeTime;
    public float speed;
    [HideInInspector]
    public bool triggered = false;
    public Color emissiveBase;
    public GameObject potionExplosionPrefab;
    public float timeToCollect;

    List<Tile> buttonTiles = new List<Tile>();
    private int buttonCount;
    private bool moveToCenter;
    private Transform startTransform;
    private int hp;
    private Renderer rend;
    private Transform gameCenter;
    Rigidbody rb;
    private playerHealth pH;
    private ScoreController sC;
    private CanvasController cC;
    [SerializeField]
    private float absorbPercent;
    private Material currentMaterial;
    float emission = 10;


    // Use this for initialization
    void Start () {
        Invoke("removeIfAlive", lifeTime);
        pH = GameObject.Find("Player").GetComponent<playerHealth>();
        sC = GameObject.Find("Score").GetComponent<ScoreController>();

      
        rend = GetComponent<Renderer>();
        currentMaterial = rend.material;

        absorbPercent = 0;
        moveToCenter = true;
        buttonCount = 0;
        gameCenter = GameObject.Find("GameCenter").transform;
        //Destroy(this, lifeTime);
        rb = GetComponent<Rigidbody>();
        hp = startingHP;
        startTransform = transform;
        cC = GameObject.Find("CanvasController").GetComponent<CanvasController>();
	}
	
	// Update is called once per frame
	void Update () {
    
        if (buttonTiles != null)
        {
            bool playerOnPotion = false;
            foreach (Tile tile in buttonTiles)
            {
                if (tile.playerHere == true)
                {
                    playerOnPotion = true;
                }
                if (absorbPercent > timeToCollect)
                {
                    collect();
                }
                if (tile.myState == Tile.States.FIRE)
                {
                    //call a different explosion prefab that is red and is only gravity
                    Destroy(this.gameObject);
                }
            }
            if(playerOnPotion)
            {
                absorbPercent += Time.deltaTime;
                emission += absorbPercent;
            }
        }



        Color finalColor = emissiveBase * Mathf.LinearToGammaSpace(emission);
        rend.material.SetColor("_EmissionColor", finalColor);
    }

    //increase the brightness and check for break
    public void hit()
    {
        if (hp > 0)
        {
            hp--;

            switch (hp)
            {
                case 2:
                    rb.AddRelativeTorque((gameCenter.transform.position - transform.position) * 100);
                    break;

                case 1:
                    rb.AddRelativeTorque((gameCenter.transform.position - transform.position) * 100);
                    break;

                case 0:
                    triggered = true;
                    cC.updateCanvas();
                    rb.AddRelativeTorque((gameCenter.transform.position - transform.position) * 100);
                    StartCoroutine(activate());
                    break;

                default:
                    break;
            }
        }
        print(hp);
    }

    IEnumerator activate()
    {

        float step = Time.deltaTime * speed;
        while (moveToCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameCenter.position, step);
            yield return null;
        }
    }

    private void collect()
    {
        pH.heal();
        sC.Score += 1000;
        Instantiate(potionExplosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "tile")
        {
            addToButtonCount();
            buttonTiles.Add(collider.gameObject.GetComponent<Tile>());
        }
    }

    private void addToButtonCount()
    {
        buttonCount++;
        if(buttonCount >= 4)
        {
            moveToCenter = false;
        }
    }

    private void OnDestroy()
    {
        foreach (Tile tile in buttonTiles)
        {
            tile.myState = Tile.States.NONE;
        }
        cC.updateCanvas();
    }

    private void removeIfAlive()
    {
        if(hp > 0)
        {
            Destroy(gameObject);
        }
    }

}
