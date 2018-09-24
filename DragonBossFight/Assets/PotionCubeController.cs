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
    


    // Use this for initialization
    void Start () {
        moveToCenter = true;
        buttonCount = 0;
        gameCenter = GameObject.Find("GameCenter").transform;
        //Destroy(this, lifeTime);
        rb = GetComponent<Rigidbody>();
        hp = startingHP;
        rend = GetComponent<Renderer>();
        startTransform = transform;
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
                    rb.AddTorque(transform.right * 100);
                    break;

                case 1:
                    rb.AddTorque(transform.right * 100);
                    break;

                case 0:
                    rb.AddTorque(transform.right * 100);
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
    }

}
