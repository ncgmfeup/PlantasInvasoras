using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : MonoBehaviour {

	public double timeToReproduce;

	private double timeLeft;

	// Use this for initialization
	void Start () {
		timeLeft = timeToReproduce;
	}
	
	// Update is called once per frame
	void Update () {
        
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
            int treeID = this.GetComponent<Mimosa>().treeID;
            GameObject.Find("GameManager").GetComponent<MimosaManager>().SpawnTreeNear(treeID);
			timeLeft += timeToReproduce;
		}
	}
}
