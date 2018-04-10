using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour {

	    public int numPlants; // Number of "bad" plants
        public bool gameOver = false;
        // Use this for initialization
        void Start()  {
            initializeVariables();
        }


        // Update is called once per frame
        void Update() {
            updateGameState();

            handleDifficulty();
        }

        // Initializes all stage specific variables
        public abstract void initializeVariables();

        // Stage checks to see how challenging game is becoming, might come in handy.
        public abstract void handleDifficulty();

        // Updates all the necessary components in a frame
        public abstract void updateGameState();


        // To detect whether gameOver is reached. Varies from stage, override it in manager.
        public abstract bool checkGameOver();

		public void increaseNumPlants() { 
			numPlants += 1;
		}

		public void decreaseNumPlants() {
			numPlants -= 1;
		}
    }
}