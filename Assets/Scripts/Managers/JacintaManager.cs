using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JacintaManager : StateNamespace.StageManager {
	private SpriteRenderer waterRenderer;
	public float health;
    public Slider healthSlider;

	public int maxJacintas;
	private float rWater, gWater, bWater; // RGB Components for dead water
	public Color waterColor;

    private JacintaSoundManager soundManager;
	
	// Use this for initialization
	public override void InitializeVariables() {
		// WATER
		waterRenderer = GameObject.Find("Water").GetComponent<SpriteRenderer>();
		health = 100f;
		maxJacintas = 5;
		rWater=45; gWater=71; bWater=58; // Change here to alter water tint

		waterColor = new Color(1,1,1,1);
        // JACINTAS

        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<JacintaSoundManager>();
	}
	
	// Update is called once per frame
	public override void UpdateGameState() {
		updateHealth();
		updateWater();
        updateWaterLevel();
	}

	void updateHealth() {
		int invadingPlants = GameObject.FindGameObjectsWithTag("InvadingPlant").Length;
		health = Mathf.Lerp(100f, 0f, (float)invadingPlants/maxJacintas);
		if (health <= 0){
			//TODO Lose
		} else if (health >= 100f){
			//TODO Win
		}
	}

    void updateWater() {
		//health -= 0.02f*plants.Count;
		waterColor.r = (rWater + (100-rWater)*health/100f)/100f;
		waterColor.g = (gWater + (100-gWater)*health/100f)/100f;
		waterColor.b = (bWater + (100-bWater)*health/100f)/100f;
		
		waterRenderer.color= waterColor;
	}

    private void updateWaterLevel()
    {

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
