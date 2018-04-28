using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public float minX, maxX, minY, maxY, minMoveTime, maxMoveTime;

    private SpriteRenderer spriteRenderer;
    private bool dead = false;

    private float startTime;
    private float moveTime;
    private float speed;
    private float fractionTravel;
    private Vector3 posInic;
    private Vector3 posFim;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startTime = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        speed = 1 / moveTime;
        posInic = posFim = this.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        getNewDestination();
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            fractionTravel = (Time.time - startTime) * speed;
            if (fractionTravel > 1)
            {
                getNewDestination();
                startTime = Time.time;
                fractionTravel = 0.0f;
            }
            this.transform.position = Vector3.Lerp(posInic, posFim, fractionTravel); 
        }
	}

    private void getNewDestination()
    {
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        speed = 1 / moveTime;

        posInic = posFim;
        posFim = new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY) , 0);

        //Sprite flip for direction of movement
        if (posInic.x < posFim.x) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }

    public bool Die()
    {
        if (!dead)
        {
            spriteRenderer.flipY = true;
            dead = true;
            return true;
        }
        return false;
    }
}
