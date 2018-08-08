using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour {

    public float speed;
    public Animator anim;
    public Transform gameCenter;
   


    private Floor floor;
    private AudioSource aS;
    private dragonHealth dH;


    // Use this for initialization
    void Start () {
        floor = GameObject.FindWithTag("floor").GetComponent<Floor>();
        gameCenter = GameObject.FindWithTag("center").transform;
        dH = GetComponent<dragonHealth>();
    }

    public IEnumerator pushWall(float targetTime)
    {
        var wall = new GameObject();
        wall.transform.parent = this.gameObject.transform;
        var boxCollider = wall.gameObject.AddComponent<BoxCollider>();
        boxCollider.tag = "warn";
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(floor.sizeX, 1, floor.sizeZ);
        boxCollider.transform.localPosition = new Vector3(0f, 0f, -floor.sizeZ/2);
        //boxCollider.center = new Vector3(0, 0, floor.sizeZ/2);
        boxCollider.center = new Vector3(0, 0, 0);


        while (Vector3.Magnitude(wall.transform.position - gameCenter.position) > floor.sizeZ)
        {
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, gameCenter.position, 5 * Time.deltaTime);
            print("moving");
            yield return new WaitForEndOfFrame();
        }

        boxCollider.transform.localPosition = new Vector3(0, 0, -floor.sizeZ/2);
        yield return new WaitForFixedUpdate();
        boxCollider.tag = "fire";
        yield return new WaitForSeconds(3f);
        
        while (Vector3.Magnitude(wall.transform.position - gameCenter.position)  > floor.sizeX/2)
        {
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, gameCenter.position, speed * Time.deltaTime);
            print("moving");
            yield return new WaitForEndOfFrame();
        }

        dH.setTargets();
        yield return new WaitForSeconds(targetTime);
        wall.transform.Translate(Vector3.down * 1000);
        yield return new WaitForFixedUpdate();
        Destroy(wall);
        
    }
}
