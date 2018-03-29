using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour {

	public int numMaxTrees = 50;
	public int maxDistanceReproduction = 2;
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
            newTree.GetComponent<Mimosa>().treeID = i;
			trees[i] = newTree;
			xPos += distanceTrees;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (CheckGameOver())
        {
            //TODO: GameOverScreen
        }
	}

	public void SelectWeapon(int value) {
		weaponSelected = value;
	}

	public void SpawnTreeNear(int treeID) {
		for (int i = 1; i < maxDistanceReproduction; i++) {
            if (treeID - i >= 0) {
                Mimosa mimosa = trees[treeID - i].GetComponent<Mimosa>();
                if (mimosa.treeState == 0)
                {
                    mimosa.PlantTree();
                    break;
                }
            }
            if (treeID + i < trees.Length)
            {
                Mimosa mimosa = trees[treeID + i].GetComponent<Mimosa>();
                if (mimosa.treeState == 0)
                {
                    mimosa.PlantTree();
                    break;
                }
            }
		}
        return;
	}

    bool CheckGameOver()
    {
        bool isFound = false;
        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i].GetComponent<Mimosa>().treeState == 0)
            {
                isFound = true;
                break;
            }
        }
        return isFound;
    }
}
