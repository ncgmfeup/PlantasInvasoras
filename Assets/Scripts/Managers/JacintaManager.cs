using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JacintaManager : StateNamespace.StageManager {
	
	public int maxJacintas;
	
	private JacintaSoundManager soundManager;

	private WaterShaderScript waterController;
	
	// Use this for initialization
	public override void InitializeVariables() {
		// WATER
		health = 100f;
		maxJacintas = 5;
		
		waterController = GameObject.Find("Water").GetComponent<WaterShaderScript>();
        // JACINTAS
		PlantObjectPooler.sharedInstance.SpawnInvadingPlantAtPosition(new Vector3(-1.04f, 1.46f, -3.25f));

        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<JacintaSoundManager>();
	}

	protected sealed override void CheckGameState()
	{
		if (PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants() > m_maxInvadingPlantsBeforeGameLost)
		{
			m_gameState = GameState.GameLost;
			m_currentHUD.SetGameLostScreenVisibility(true);
		}
	}
	
	// Update is called once per frame
	public override void UpdateGameState() {
		waterController.UpdateHealth(health);
		updateWaterLevel();
		updateHealth();
		//updateWater();
        updateWaterLevel();
	}

	void updateHealth() {
		int invadingPlants = PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants();
		Debug.Log("Updating with numPlants " + invadingPlants + " health " + health + " max " +maxJacintas);
		health = health + (((float)(maxJacintas-invadingPlants)/maxJacintas) * 100f - health)*0.1f;
	}

    private void updateWaterLevel() {
        healthSlider.value = health/100f;
    }

    public override void HandleDifficulty() {}

	public override void HitSomething(GameObject obj) {
        if (obj.tag == Utils.BAD_PLANT_TAG || obj.tag == Utils.NORMAL_PLANT_TAG) {
			if (m_scenePlayer.GetSelectedWeapon() == Utils.NET_SEL) {
				// Instantiate net
            	m_scenePlayer.UseToolOnObject(obj);
			} else if (m_scenePlayer.GetSelectedWeapon() == Utils.AXE_SEL) {
				// Instantiate axe
            	m_scenePlayer.UseToolOnObject(obj);
			} else if (m_scenePlayer.GetSelectedWeapon() == Utils.FIRE_SEL) {
				// Instantiate flame
            	m_scenePlayer.UseToolOnObject(obj);
			}
		}
    }
}
