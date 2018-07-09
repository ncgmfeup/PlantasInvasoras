using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

  public float minX, maxX, minY, maxY, minMoveTime, maxMoveTime, minHealth;

  private SpriteRenderer spriteRenderer;
  private bool dead = false;

  private float startTime;
  private float moveTime;
  private float speed;
  private float fractionTravel;
  private Vector3 posInic;
  private Vector3 posFim;
  private ParticleSystem bubblesSystem;
	private Transform bubbles;

	private Collider2D collider;
  private float lastDead, lastRebirth;

	public Sprite[] sprites;

	public float timeBeforeFade, fadeTime = 1;

  // Use this for initialization
  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    bubbles = transform.Find("Bubbles");
		bubblesSystem = bubbles.GetComponent<ParticleSystem>();
		collider = GetComponent<BoxCollider2D>();
    startTime = Time.time;
    moveTime = Random.Range(minMoveTime, maxMoveTime);
    speed = 1 / moveTime;
    posInic = posFim = this.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
    getNewDestination();

		int chosenSpriteIndex = Random.Range(0, sprites.Length);
    spriteRenderer.sprite = sprites[chosenSpriteIndex];
  }

  // Update is called once per frame
  void Update()
  {
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
    UpdateTransparency();
  }

  private void getNewDestination()
  {
    moveTime = Random.Range(minMoveTime, maxMoveTime);
    speed = 1 / moveTime;

    posInic = posFim;
    posFim = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

    //Sprite flip for direction of movement
    if (posInic.x < posFim.x)
    {
      spriteRenderer.flipX = true;
      bubbles.localPosition = new Vector3(3, -0.25f, 0);
    }
    else
    {
      spriteRenderer.flipX = false;
      bubbles.localPosition = new Vector3(-3, -0.25f, 0);
    }
  }

  public bool Die()
  {
    if (!dead)
    {
      lastDead = Time.time + timeBeforeFade;
      //collider.enabled = false;
      var emission = bubblesSystem.emission;
      emission.rateOverTime = 0;
      spriteRenderer.flipY = true;
      dead = true;
      return true;
    }
    return false;
  }

  public bool Revive()
  {
    if (dead)
    {
      lastRebirth = Time.time;
			//collider.enabled = true;
      var emission = bubblesSystem.emission;
      emission.rateOverTime = 2;
      spriteRenderer.flipY = false;
      dead = false;
			int chosenSpriteIndex = Random.Range(0, sprites.Length);
    	spriteRenderer.sprite = sprites[chosenSpriteIndex];
      return true;
    }
    return false;
  }

  public void UpdateHealth(float health)
  {
    if (health < minHealth)
    {
      Die();
    }
    else
    {
      Revive();
    }
  }

  public void UpdateTransparency()
  {
    float alphaLevel;
    if (dead)
      alphaLevel = Mathf.Lerp(1, 0, (Time.time - lastDead) / fadeTime);
    else
      alphaLevel = Mathf.Lerp(0, 1, (Time.time - lastRebirth) / fadeTime);
    spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);
  }
}
