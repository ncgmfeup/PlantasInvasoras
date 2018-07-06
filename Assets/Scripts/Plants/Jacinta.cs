using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Jacinta : Plant {

	public float m_secondsToDry;

	public float m_secondsToStayDried;
	public float m_secondsToReproduce;

	public Vector2 m_cutImpulse;

	public float minRange = 10f, maxRange = 20f;
	
	private PlantState currentState;

	public Sprite[] healthySprites;
	public Sprite[] witeredSprites; 
	private int chosenSpriteIndex;
	private SpriteRenderer spriteRenderer; 

	private JacintaShader jacintaShader; 

    public override void initializeVariables() {
		m_secondsToReproduce = Random.Range(minRange, maxRange);
		m_cutImpulse = new Vector2(0f, -1f);
		GameObject gameManager = GameObject.Find("GameManager");
		manager = (StateNamespace.StageManager) gameManager.GetComponent(typeof(JacintaManager));

		currentState = PlantState.WATERED;
		chosenSpriteIndex = Random.Range(0,healthySprites.Length);
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = healthySprites[chosenSpriteIndex];

		Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector2(0,-2);

		jacintaShader = GetComponent<JacintaShader>();
	}
	
	// Update is called once per frame
	public override void updatePlantState () {
		switch(currentState) {
			case PlantState.WATERED:
				m_secondsToReproduce -= Time.deltaTime;
				if (m_secondsToReproduce <= 0) {
					reproduce();
					m_secondsToReproduce = Random.Range(minRange, maxRange);		
				}
				break;
			case PlantState.DRYING:
				StartCoroutine("Die");

				// RETORNAR À POOL
				//currentState = PlantState.WATERED;

				break;

		}
	}

	public override IEnumerator Die() {
		//SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		yield return new WaitForSeconds(m_secondsToDry);
		spriteRenderer.sprite = witeredSprites[chosenSpriteIndex];
		yield return new WaitForSeconds(m_secondsToStayDried);
		
		DeSpawn();
        currentState = PlantState.WATERED;
		spriteRenderer.sprite = healthySprites[chosenSpriteIndex];
	}

	public void reproduce() {
		manager.SpawnInvadingPlant(new Vector3(this.transform.position.x + Random.Range(-1f,1f), 
				this.transform.position.y + Random.Range(-0.3f, 0.3f), this.transform.position.z));
        soundManager.playPopSound();
    }

	public override void cut() {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.AddForce(m_cutImpulse, ForceMode2D.Impulse);
        soundManager.playCutSound();
    }

	public override void burnt() {
        soundManager.playFireSound();
    }

	public override void bombed(float impact) {
		Debug.Log("Affected with " + impact);

		// If was bombed closer, more seeds fly, more reproductions
		for (int i = 0 ; i < (int) (impact / 100f) ; i++) {
			reproduce();
		}
    }
    
	public override void caught() {
		currentState = PlantState.DRYING;
        soundManager.playNetSound();
    }

	public override void Touch() {
		jacintaShader.TurnOnEvilAura();
    }
}
