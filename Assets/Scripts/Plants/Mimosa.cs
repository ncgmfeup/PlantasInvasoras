using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantNamespace;

public class Mimosa : Plant {
    public enum MimosaState { NoTree = 0, Normal, Cut, BarkOff, DriedAndReadyToCut};

	public int treeID; //Equivalent to the position on the manager's tree array
	public MimosaState currTreeState;
	public Sprite[] sprites;
	public double timeToDry;
    public double timeToReproduce;

    public double timeReproLeft;
    private double timeDryLeft;

	// Use this for initialization
	public override void initializeVariables() {
        currTreeState = MimosaState.NoTree;
        timeReproLeft = timeToReproduce;
        timeDryLeft = timeToDry;
	}
	
	// Update is called once per frame
	public override void updatePlantState() { 
        //Reproducing
        if (currTreeState == MimosaState.Normal)
        {
            timeReproLeft -= Time.deltaTime;
            if (timeReproLeft <= 0)
            {
                GameObject.Find("GameManager").GetComponent<MimosaManager>().SpawnTreeNear(treeID);
                timeReproLeft += timeToReproduce;
            }
        }

		//Drying
		if (currTreeState == MimosaState.BarkOff && timeDryLeft > 0) {
			timeDryLeft -= Time.deltaTime;
			if (timeDryLeft <= 0) {
                currTreeState = MimosaState.DriedAndReadyToCut;
			}
		}

        //Set the sprite to be the equivalent int enum.
        this.GetComponent<SpriteRenderer>().sprite = sprites[(int)currTreeState];

		/*switch (treeState) {
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
		}*/
	}

	public bool PlantTree () {
		if (currTreeState == MimosaState.NoTree) {
            currTreeState = MimosaState.Normal;
            timeReproLeft = timeToReproduce;
            return true;
		}
		return false;
	}

	/*
	public void CutBark() {
        //TODO: Play animation and sound;
        currTreeState = MimosaState.Cut;
	}

	public void TakeBarkOff() {
        //TODO: Play animation and sound;
        currTreeState = MimosaState.BarkOff;
		timeDryLeft = timeToDry;
	} 
	*/

	public override void bombed() {}

	public override void cut() {}

	public override void burnt() {}

	public override void caught() {}
}
