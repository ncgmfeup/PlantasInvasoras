using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JacintaManager : StateNamespace.StageManager
{

  //public int maxJacintas;
  public float delayGameOverTime = 8.0f;

  private JacintaSoundManager soundManager;

  private WaterShaderScript waterController;

  private ParticleSystem bubblesSystem;

  private float healthRegen;

  private float maxBubblesSpawnRate;

  public GameObject[] fishes;

  // Use this for initialization
  public override void InitializeVariables()
  {
    // WATER
    health = 100f;
    //m_maxInvadingPlantsBeforeGameLost = 2;

    var water = GameObject.Find("Water");
    waterController = water.GetComponent<WaterShaderScript>();

    bubblesSystem = water.transform.Find("Bubbles").GetComponent<ParticleSystem>();
    var emission = bubblesSystem.emission;
    var rOt = emission.rateOverTime;
    maxBubblesSpawnRate = rOt.constant;

    // JACINTAS
    for(int i = 0; i < m_initialInvadingPlants; i++){
      PlantObjectPooler.sharedInstance.SpawnInvadingPlantAtPosition(new Vector3(Random.Range(-2f, 2f), 1.46f, -3.25f));
    }
    
    soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<JacintaSoundManager>();
  }

  protected sealed override void CheckGameState()
  {
    base.CheckGameState();
    if (PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants() > m_maxInvadingPlantsBeforeGameLost)
    {
      //m_gameState = GameState.GameLost;
      //m_currentHUD.SetGameLostScreenVisibility(true);
      StartCoroutine("DelayGameOver");
    }
    else StopCoroutine("DelayGameOver");
  }

  // Update is called once per frame
  public override void UpdateGameState()
  {
    waterController.UpdateHealth(health);
    updateHealth();
    updateWaterLevel();
    //waterController.UpdateHealth(health);
    updateBubbles();
    foreach (GameObject fish in fishes)
      fish.GetComponent<Fish>().UpdateHealth(health);
  }

  void updateHealth()
  {
    int invadingPlants = PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants();
    //Debug.Log("Updating with numPlants " + invadingPlants + " health " + health + " max " + maxJacintas);
    float regen = ((float)(m_maxInvadingPlantsBeforeGameLost - invadingPlants) / m_maxInvadingPlantsBeforeGameLost) * 100f;
    health = Mathf.SmoothDamp(health, regen, ref healthRegen, 1);
    
    //health = health + (((float)(maxJacintas - invadingPlants) / maxJacintas) * 100f - health) * 0.1f;
    if (health < 1)
      StartCoroutine("DelayGameOver");
    else StopCoroutine("DelayGameOver");
  }

  private void updateWaterLevel()
  {
    healthSlider.value = health / 100f;
  }

  private void updateBubbles()
  {
    var emission = bubblesSystem.emission;
    emission.rateOverTime = Mathf.Lerp(0, maxBubblesSpawnRate, health/100f);
  }

  public override void HandleDifficulty() { }


  private IEnumerator DelayGameOver()
  {
    yield return new WaitForSeconds(delayGameOverTime);
    m_gameState = GameState.GameLost;
    m_currentHUD.SetGameLostScreenVisibility(true);
    yield return null;
  }

  IEnumerator NetCooldown()
  {
    canUseTool = false;
    yield return new WaitForSeconds(4.0f);
    canUseTool = true;
  }

  IEnumerator ToolCooldown()
  {
    canUseTool = false;
    yield return new WaitForSeconds(1.0f);
    canUseTool = true;
  }




  public override void TouchStart(Vector2 position)
  {
    //Debug.Log("Start Touch");
  }
  public override void TouchContinue(Vector2 position)
  {
    //Debug.Log("Continue Touch");

    if (m_scenePlayer.tool != Player.ToolType.None)
      m_scenePlayer.UpdateToolPosition(position);
  }
  public override void TouchEnd(Vector2 position)
  {
    //Debug.Log("End Touch");

    RaycastHit2D hit;

    switch (m_scenePlayer.tool){
      case Player.ToolType.None:
        if (hit = Physics2D.Raycast(position, Vector2.zero))
          hit.collider.gameObject.SendMessage("Touch", null, SendMessageOptions.DontRequireReceiver);
        break;
      case Player.ToolType.Bomb:
        m_scenePlayer.UseTool(position);
        break;
      default:
        m_scenePlayer.ResetTool();
        break;
    }
    
  }
}
