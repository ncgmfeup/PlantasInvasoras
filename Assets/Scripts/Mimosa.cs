using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimosa : MonoBehaviour {


	public int treeState; // 0 - No tree, 1 - normal, 2 - 1st stage (cut), 3 - 2nd stage (pants out), 4 - dried and ready to cut
	public int initialState = 0;
	public Sprite[] sprites;
	public double timeToDry;

	private double timeLeft;
	private Vector3 treePos;

	// Use this for initialization
	void Start () {
		treeState = initialState;
		treePos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Drying
		if (treeState == 4 && timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				treeState = 5;
			}
		}

		switch (treeState) {
		case 0:
			this.GetComponent<SpriteRenderer> ().sprite = sprites [0];
			break;
		case 1:
			this.GetComponent<SpriteRenderer> ().sprite = sprites [1];
			break;
		case 2:
			this.GetComponent<SpriteRenderer> ().sprite = sprites [2];
			break;
		case 3:
			this.GetComponent<SpriteRenderer> ().sprite = sprites [3];
			break;
		case 4:
			this.GetComponent<SpriteRenderer> ().sprite = sprites [4];
			break;
		default:
			break;
		}
	}

	public bool plantTree () {
		if (treeState == 0) {
			treeState = 1;
		}
		return false;
	}

	public void cutBark() {
		//TODO: Play animation and sound;
		treeState = 2;
	}

	public void takeBarkOff() {
		//TODO: Play animation and sound;
		treeState = 3;
		timeLeft = timeToDry;
	}
}
