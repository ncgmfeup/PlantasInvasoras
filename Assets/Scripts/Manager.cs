using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour {

	public int numMaxTrees = 50;
	public float distanceTrees;

	public GameObject noTreePrefab;
	public GameObject mimosaPrefab;

	private GameObject[] trees;
	public int[] initialTrees;

	public int weaponSelected = 0; //0 - none, 1/4 others;

	// Use this for initialization
	void Awake () {
		trees = new GameObject[numMaxTrees];

		float xPos = 0;
		for (int i = 0; i < numMaxTrees; i++) {
			GameObject newTree; 
			if (initialTrees.Contains(i))
				newTree = Instantiate (mimosaPrefab);
			else
				newTree = Instantiate (noTreePrefab);
			newTree.transform.position = new Vector3 (xPos, 0.0f, 0.0f);
			trees[i] = newTree;
			xPos += distanceTrees;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectWeapon(int value) {
		weaponSelected = value;
	}
}
