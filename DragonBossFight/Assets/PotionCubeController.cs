using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCubeController : MonoBehaviour {

    public int startingHP;
    public float lifeTime;
    public float speed;

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
    


    // Use this for initialization
    void Start () {
        Destroy(this.gameObject, lifeTime);
        pH = GameObject.Find("Player").GetComponent<playerHealth>();
        sC = GameObject.Find("Score").GetComponent<ScoreController>();
        
        moveToCenter = true;
        buttonCount = 0;
        gameCenter = GameObject.Find("GameCenter").transform;
        //Destroy(this, lifeTime);
        rb = GetComponent<Rigidbody>();
        hp = startingHP;
        rend = GetComponent<Renderer>();
        startTransform = transform;
        cC = GameObject.Find("CanvasController").GetComponent<CanvasController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (buttonTiles != null)
        {
            foreach (Tile tile in buttonTiles)
            {
                if (tile.playerHere == true)
                {
                    StartCoroutine(collect());
                }
                if (tile.myState == Tile.States.FIRE)
                {
                    Destroy(this.gameObject);
                }
            }
        }
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
                    rb.AddRelativeTorque((gameCenter.transform.position - transform.position) * 100);
                    StartCoroutine(activate());
                    break;

                default:
                    break;
            }
        }
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

    IEnumerator collect()
    {

        pH.heal();
        sC.Score += 1000;
        Destroy(this.gameObject);
        yield return null;
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

}
