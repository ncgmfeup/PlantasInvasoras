using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JacintaManager : StateNamespace.StageManager {
	
	public int maxJacintas;
    public float delayGameOverTime = 8.0f;
	
	private JacintaSoundManager soundManager;

	private WaterShaderScript waterController;

    // Use this for initialization
    public override void InitializeVariables() {
		// WATER
		health = 100f;
		maxJacintas = 2;
		
		waterController = GameObject.Find("Water").GetComponent<WaterShaderScript>();

        // JACINTAS
		PlantObjectPooler.sharedInstance.SpawnInvadingPlantAtPosition(new Vector3(-1.04f, 1.46f, -3.25f));

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
	public override void UpdateGameState() {
		waterController.UpdateHealth(health);
		//updateWaterLevel();
		updateHealth();
		//updateWater();
        updateWaterLevel();
	}

	void updateHealth() {
		int invadingPlants = PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants();
		//Debug.Log("Updating with numPlants " + invadingPlants + " health " + health + " max " + maxJacintas);
		health = health + (((float)(maxJacintas-invadingPlants)/maxJacintas) * 100f - health)*0.1f;
        if (health <= 0)
            StartCoroutine("DelayGameOver");
        else StopCoroutine("DelayGameOver");
	}

    private void updateWaterLevel() {
        healthSlider.value = health/100f;
    }

    public override void HandleDifficulty() {}

	public override void HitSomething(GameObject obj) {
		//Debug.Log("Cenas");
        if (obj.tag == Utils.BAD_PLANT_TAG || obj.tag == Utils.NORMAL_PLANT_TAG) {
            if (m_scenePlayer.GetSelectedWeapon() == Utils.NET_SEL && canUseTool) {
                // Instantiate net
                m_scenePlayer.UseToolOnObject(obj);
                StartCoroutine("NetCooldown");
            } else if (m_scenePlayer.GetSelectedWeapon() == Utils.AXE_SEL && canUseTool) {
				// Instantiate axe
            	m_scenePlayer.UseToolOnObject(obj);
                StartCoroutine("ToolCooldown");
            } else if (m_scenePlayer.GetSelectedWeapon() == Utils.FIRE_SEL && canUseTool) {
				// Instantiate flame
            	m_scenePlayer.UseToolOnObject(obj);
                StartCoroutine("ToolCooldown");
            } else {
               obj.SendMessage("Touch", null, SendMessageOptions.DontRequireReceiver);
            }
		}
    }

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
}
