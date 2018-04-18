using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JacintaManager : StateNamespace.StageManager {
	private SpriteRenderer waterRenderer;
	public float health;
	public int numberJacintas;

	private float rWater, gWater, bWater; // RGB Components for dead water
	public Color waterColor;
	
	// Use this for initialization
	public override void initializeVariables() {
		// WATER
		waterRenderer = GameObject.Find("Water").GetComponent<SpriteRenderer>();
		health = 100f;

		rWater=45; gWater=71; bWater=58; // Change here to alter water tint

		waterColor = new Color(1,1,1,1);				
		// JACINTAS

		numberJacintas = 0;
	}
	
	// Update is called once per frame
	public override void updateGameState() {
		updateWater();	
	}

	void updateWater() {
		health -= 0.02f*numberJacintas;
		waterColor.r = (rWater + (100-rWater)*health/100f)/100f;
		waterColor.g = (gWater + (100-gWater)*health/100f)/100f;
		waterColor.b = (bWater + (100-bWater)*health/100f)/100f;
		
		waterRenderer.color= waterColor;
	}

	public void updateNumberJacintas() {
		numberJacintas++;
	}

    public override void handleDifficulty() {}
}
