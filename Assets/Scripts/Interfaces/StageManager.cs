using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour {
    	private int weaponSelected = 0; //0 - none, 1/4 others;

        private Tool[] tools;

        public TouchManager touchManager;

	    public int numPlants; // Number of "bad" plants
        public bool gameOver = false;
        // Use this for initialization
        void Start()  {
            touchManager = new TouchManager(this); // Instantiate a new instance of a touch manager  

            tools = new Tool[4];
            
            tools[0] = new Bomb();
            tools[1] = new Axe(); 
            tools[2] = new Flame();
            tools[3] = new Mario();

            initializeVariables();
        }


        // Update is called once per frame
        void Update() {
            updateGameState();

            touchManager.updateTouch();

            handleDifficulty();
        }

        public void swiped(Vector2 swipe) {
            if (weaponSelected != 0) { // Weapon was selected
                tools[weaponSelected].activated();
            }
        }

        // Initializes all stage specific variables
        public abstract void initializeVariables();

        // Stage checks to see how challenging game is becoming, might come in handy.
        public abstract void handleDifficulty();

        // Updates all the necessary components in a frame
        public abstract void updateGameState();


        // To detect whether gameOver is reached. Varies from stage, override it in manager.
        public bool checkGameOver() {
            return numPlants == 0;
        }

       	public void SelectWeapon(int value) {
	    	weaponSelected = value;
	    }

		public void increaseNumPlants() { 
			numPlants += 1;
		}

		public void decreaseNumPlants() {
			numPlants -= 1;
		}

        public int getSelectedWeapon() {
            return weaponSelected;
        }
    }
}