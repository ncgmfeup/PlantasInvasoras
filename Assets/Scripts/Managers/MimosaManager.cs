using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using StateNamespace;
public class MimosaManager : StateNamespace.StageManager {

	public int numMaxTrees = 50;
	public int maxDistanceReproduction = 2;
	public float distanceTrees;

	public GameObject noTreePrefab;
	public GameObject mimosaPrefab;

	private GameObject[] trees;
	public int[] initialTrees;


    public override void InitializeVariables() {
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
	public override void UpdateGameState() {

    }



	public void SpawnTreeNear(int treeID) {
		for (int i = 1; i < maxDistanceReproduction; i++) {
            if (treeID - i >= 0) {
                Mimosa mimosa = trees[treeID - i].GetComponent<Mimosa>();
                if (mimosa.currTreeState == Mimosa.MimosaState.NoTree)
                {
                    mimosa.PlantTree();
                    break;
                }
            }
            if (treeID + i < trees.Length)
            {
                Mimosa mimosa = trees[treeID + i].GetComponent<Mimosa>();
                if (mimosa.currTreeState == Mimosa.MimosaState.NoTree)
                {
                    mimosa.PlantTree();
                    break;
                }
            }
		}
        return;
	}

    public override void HandleDifficulty() {}

}
