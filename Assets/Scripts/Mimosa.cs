using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimosa : MonoBehaviour {
    public enum TreeStates { NoTree, Normal, Stage1, Stage2, Dried };

	public int treeID; //Equivalent to the position on the manager's tree array
	public int treeState; // 0 - No tree, 1 - normal, 2 - 1st stage (cut), 3 - 2nd stage (pants out), 4 - dried and ready to cut
	public int initialState = (int)TreeStates.NoTree;
	public Sprite[] sprites;
	public double timeToDry;
    public double timeToReproduce;

    public double timeReproLeft;
    private double timeDryLeft;

	// Use this for initialization
	void Start () {
		treeState = initialState;
        timeReproLeft = timeToReproduce;
        timeDryLeft = timeToDry;
	}
	
	// Update is called once per frame
	void Update () {
        //Reproducing
        if (treeState == (int) TreeStates.Normal)
        {
            timeReproLeft -= Time.deltaTime;
            if (timeReproLeft <= 0)
            {
                GameObject.Find("GameManager").GetComponent<Manager>().SpawnTreeNear(treeID);
                timeReproLeft += timeToReproduce;
            }
        }

		//Drying
		if (treeState == (int)TreeStates.Stage2 && timeDryLeft > 0) {
			timeDryLeft -= Time.deltaTime;
			if (timeDryLeft <= 0) {
				treeState = (int)TreeStates.Dried;
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

	public bool PlantTree () {
		if (treeState == (int)TreeStates.NoTree) {
			treeState = (int)TreeStates.Normal;
            timeReproLeft = timeToReproduce;
            return true;
		}
		return false;
	}

	public void CutBark() {
		//TODO: Play animation and sound;
		treeState = (int)TreeStates.Stage1;
	}

	public void TakeBarkOff() {
		//TODO: Play animation and sound;
		treeState = (int)TreeStates.Stage2;
		timeDryLeft = timeToDry;
	}
}
