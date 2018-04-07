using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jacinta : MonoBehaviour {

	public int Health = 0;
	public float secondsToDry = 1f;
	public float secondsToReproduce;

	private JacintaManager manager;

	private Color jacintaColor;
	
	private enum JacintaState {
		DRYING, WATERED
	}

	// Use this for initialization
	void Start () {
		jacintaColor = new Color(1,1,1,1); // Updates when dried
		secondsToDry = 1;
		secondsToReproduce = Random.Range(1f, 2f);
		GameObject gameManager = GameObject.Find("GameManager");
		manager = (JacintaManager) gameManager.GetComponent(typeof(JacintaManager));
	}
	
	// Update is called once per frame
	void Update () {
		secondsToReproduce -= Time.deltaTime;
		if (secondsToReproduce <= 0) {
			reproduce();
			secondsToReproduce = Random.Range(1f, 2f);
		}
	}

	void reproduce() {
		manager.updateNumberJacintas();
		Instantiate(this, new Vector3(this.transform.position.x + Random.Range(-1f,1f), 
				this.transform.position.y + Random.Range(-0.3f, 0.3f), this.transform.position.z), Quaternion.identity);
	}
}
