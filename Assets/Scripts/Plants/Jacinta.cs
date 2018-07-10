using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Jacinta : Plant
{

  public float m_secondsToDry;

  private float dryingTimer = 0;

  public float m_secondsToStayDried;
  public float m_secondsToReproduce;

  public Vector2 m_cutImpulse;

  public float min_drying_height;

  public float minRange = 10f, maxRange = 20f;


  public Sprite[] healthySprites;
  public Sprite[] witeredSprites;

  public GameObject fire_prefab;
  public float m_secondsToBurn;
  private GameObject fire;

  private int chosenSpriteIndex;
  private SpriteRenderer spriteRenderer;

  private JacintaShader jacintaShader;
  private Rigidbody2D rb;

  private float buoyancyOffset;

 

  public override void initializeVariables()
  {
    m_secondsToReproduce = Random.Range(minRange, maxRange);
    m_cutImpulse = new Vector2(0f, -1f);
    GameObject gameManager = GameObject.Find("GameManager");
    manager = (StateNamespace.StageManager)gameManager.GetComponent(typeof(JacintaManager));

    currentState = PlantState.WATERED;
    chosenSpriteIndex = Random.Range(0, healthySprites.Length);
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = healthySprites[chosenSpriteIndex];

    rb = GetComponent<Rigidbody2D>();
    rb.centerOfMass = new Vector2(0, -2);
    buoyancyOffset = Random.Range(0f, Mathf.PI*2);

    jacintaShader = GetComponent<JacintaShader>();
  }

  // Update is called once per frame
  public override void updatePlantState()
  {
    switch (currentState)
    {
      case PlantState.WATERED:
        if (transform.position.y > min_drying_height){
          currentState = PlantState.DRYING;
        }

        m_secondsToReproduce -= Time.deltaTime;
        if (m_secondsToReproduce <= 0)
        {
          reproduce();
          m_secondsToReproduce = Random.Range(minRange, maxRange);
        }

        dryingTimer -= Time.deltaTime;
        dryingTimer = Mathf.Max(dryingTimer, 0);
        break;
      case PlantState.DRYING:
        if (transform.position.y < min_drying_height){
          currentState = PlantState.WATERED;
          m_secondsToReproduce = Random.Range(minRange, maxRange);
        }

        dryingTimer += Time.deltaTime;
        if(dryingTimer > m_secondsToDry)
          StartCoroutine("Die");
        break;

    }

    jacintaShader.SetDecay(dryingTimer/m_secondsToDry);

    rb.mass = 1.25f + Mathf.Sin(Time.time + buoyancyOffset)/4;
  }

  public override IEnumerator Die()
  {
    //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    //yield return new WaitForSeconds(m_secondsToDry);
    spriteRenderer.sprite = witeredSprites[chosenSpriteIndex];
    yield return new WaitForSeconds(m_secondsToStayDried);

    DeSpawn();
    currentState = PlantState.WATERED;
    spriteRenderer.sprite = healthySprites[chosenSpriteIndex];
    dryingTimer = 0;
  }

  public void reproduce()
  {
    manager.SpawnInvadingPlant(new Vector3(this.transform.position.x + Random.Range(-1f, 1f),
        this.transform.position.y + Random.Range(-0.3f, 0.3f), this.transform.position.z));
    soundManager.SchedulePop();
  }

  public override void cut()
  {
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.AddForce(m_cutImpulse, ForceMode2D.Impulse);
    //soundManager.playCutSound();
  }

  public override void burnt()
  {
    if (currentState == PlantState.WATERED)
    {
      fire = Instantiate(fire_prefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
      fire.transform.parent = transform;
      currentState = PlantState.BURNING;
      StartCoroutine(Burn());
    }
    soundManager.PlayFire();
  }

  public override void bombed(float impact)
  {
    //Debug.Log("Affected with " + impact);

    // If was bombed closer, more seeds fly, more reproductions
    for (int i = 0; i < (int)(impact / 100f); i++)
    {
      reproduce();
    }
  }

  public override void caught()
  {
    currentState = PlantState.DRYING;
    //soundManager.playNetSound();
  }

  public override void Touch()
  {
    jacintaShader.TurnOnEvilAura();
  }

  IEnumerator Burn()
  {
    yield return new WaitForSeconds(m_secondsToBurn);
    if (fire != null)
    {
      Destroy(fire);
      fire = null;
      currentState = PlantState.WATERED;
    }

  }

  void OnActivate()
  {
    if(jacintaShader != null)
      jacintaShader.TurnOnEvilAura();
  }
}
