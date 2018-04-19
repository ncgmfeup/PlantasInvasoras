using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Jacinta : Plant {

	public int Health = 0;
	public float secondsToDry = 1f;
	public float secondsToReproduce;

	public float minRange = 10f, maxRange = 20f;

	private Color jacintaColor;
	
	private PlantState currentState;

	public override void initializeVariables() {
		jacintaColor = new Color(1,1,1,1); // Updates when dried
		secondsToDry = 1;
		secondsToReproduce = Random.Range(minRange, maxRange);
		GameObject gameManager = GameObject.Find("GameManager");
		manager = (StateNamespace.StageManager) gameManager.GetComponent(typeof(JacintaManager));

		currentState = PlantState.DRYING;
	}
	
	// Update is called once per frame
	public override void updatePlantState () {
		secondsToReproduce -= Time.deltaTime;
		if (secondsToReproduce <= 0) {
			reproduce();
			secondsToReproduce = Random.Range(minRange, maxRange);
		}
	}

	void reproduce() {
		manager.spawnAtPosition(this, new Vector3(this.transform.position.x + Random.Range(-1f,1f), 
				this.transform.position.y + Random.Range(-0.3f, 0.3f), this.transform.position.z));
	}

	public override void cut() {
		
	}

	public override void burnt() {

	}

	public override void bombed(float impact) {
		Debug.Log("Affected with " + impact);

		// If was bombed closer, more seeds fly, more reproductions
		for (int i = 0 ; i < impact / 100f ; i++) {
			reproduce();
		}

	}

	public override void caught() {
		
	}
}
