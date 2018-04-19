using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;
using PlantNamespace;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour {
    	private int weaponSelected = -1; //0 - none, 0/3 others;

        // TODO Trocar aqui para um object pooler
        protected List<Plant> plants;

        private Tool[] tools;

        private TouchManager touchManager;

        public bool gameOver = false;
        // Use this for initialization
        void Start()  {
            touchManager = new TouchManager(this); // Instantiate a new instance of a touch manager  
    
            tools = new Tool[4];
            
            tools[0] = new Bomb();
            tools[1] = new Axe(); 
            tools[2] = new Flame();
            tools[3] = new Net();
            
            plants = new List<Plant>();

            initializeVariables();

        }


        // Update is called once per frame
        void Update() {
            updateGameState();

            touchManager.updateTouch();

            handleDifficulty();
        }

        public void touched(Vector2 touch) {
            if (weaponSelected != -1) { // Weapon was selected
                tools[weaponSelected].activated();
            }
            
            if (weaponSelected == 0) {
                // Bomb exploded
                for (int i = 0 ; i < plants.Count ; i++) {
                    // TODO Ver distância à planta e fazer cálculos para impacto
                    plants[i].bombed(touch.magnitude - plants[i].transform.position.magnitude);
                }

            } else if (weaponSelected == Utils.AXE_SEL) {
                // Axe chopped
            } else if (weaponSelected == Utils.FIRE_SEL) {
                // Fire burnt
            } else if (weaponSelected == Utils.NET_SEL) { 
                // Net caught
            }
        }

        // Initializes all stage specific variables
        public virtual void initializeVariables() {
            foreach(GameObject badPlant in GameObject.FindGameObjectsWithTag(Utils.BAD_PLANT_TAG))  {
                Plant plant = (Plant) badPlant.GetComponent("Plant");
                plants.Add(plant);
            }
         }

        // Stage checks to see how challenging game is becoming, might come in handy.
        public abstract void handleDifficulty();

        // Updates all the necessary components in a frame
        public abstract void updateGameState();


        // To detect whether gameOver is reached. Varies from stage, override it in manager.
        public bool checkGameOver() {
            return plants.Count == 0;
        }

       	public void SelectWeapon(int value) {
	    	weaponSelected = value;
	    }
        
        public void spawnAtPosition(Plant plant, Vector3 pos) {
            Instantiate(plant, pos, Quaternion.identity);
            plants.Add(plant);
        }

      public int getSelectedWeapon() {
            return weaponSelected;
        }
    }
}