using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Jacinta : Plant {

	public float m_secondsToDry;
	public float m_secondsToReproduce;

	public float minRange = 10f, maxRange = 20f;

    public AudioClip bombedClip;
    public AudioClip fireClip;
    public AudioClip cutClip;
    public AudioClip popClip;
    public AudioClip netClip;

	private Color m_jacintaColor;
	
	private PlantState currentState;

    public override void initializeVariables() {
		m_jacintaColor = new Color(1,1,1,1); // Updates when dried
		m_secondsToDry = 1;
		m_secondsToReproduce = Random.Range(minRange, maxRange);
		GameObject gameManager = GameObject.Find("GameManager");
		manager = (StateNamespace.StageManager) gameManager.GetComponent(typeof(JacintaManager));

		currentState = PlantState.WATERED;

		Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector2(0,-2);
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
				currentState = PlantState.WATERED;
				break;

		}
	}

	public override IEnumerator Die() {
		Debug.Log("Is dying, poor thing");

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		m_jacintaColor = spriteRenderer.color;

		yield return new WaitForSeconds(1f);

		float elapsedTime = 0;
		while (elapsedTime < m_secondsToDry) 	{
			m_jacintaColor.a = Mathf.Lerp(1f, 0f, (elapsedTime / m_secondsToDry));
			spriteRenderer.color = m_jacintaColor; 

          	elapsedTime += Time.deltaTime;
        	yield return new WaitForEndOfFrame();
      	}

		yield return null;
	}

	public void reproduce() {
        //Pop Sound
        plantAudio.clip = popClip;
        plantAudio.Play();
		manager.SpawnInvadingPlant(new Vector3(this.transform.position.x + Random.Range(-1f,1f), 
				this.transform.position.y + Random.Range(-0.3f, 0.3f), this.transform.position.z));
	}

	public override void cut() {
        //Cut Sound
        plantAudio.clip = cutClip;
        plantAudio.Play();
    }

	public override void burnt() {
        //Burn Sound
        plantAudio.clip = fireClip;
        plantAudio.Play();
    }

	public override void bombed(float impact) {
		Debug.Log("Affected with " + impact);

		// If was bombed closer, more seeds fly, more reproductions
		for (int i = 0 ; i < impact / 100f ; i++) {
			reproduce();
		}

        //Bomb Sound
        plantAudio.clip = bombedClip;
        plantAudio.Play();
    }
    
	public override void caught() {
		currentState = PlantState.DRYING;

        //Pop Sound
        plantAudio.clip = netClip;
        plantAudio.Play();
    }
}
