using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour {
        public enum GameState
        {
            Starting,
            Playing,
            Paused,
            GameWon,
            GameLost
        }
        
    	private int m_weaponSelected = 0; //0 - none, 1/4 others;
        /// <summary>
        /// The current selected weapon. 0 is no weapon selected.
        /// </summary>
        public int WeaponSelected
        {
            get { return m_weaponSelected; }
            set { m_weaponSelected = value; }
        }

        /// <summary>
        /// The tools to be used in the current level.
        /// </summary>
        private Tool[] m_tools;

        /// <summary>
        /// The touch manager generic touch manager.
        /// </summary>
        public TouchManager m_touchManager; //Probably use a singleton shared object instead of a reference var.

        /// <summary>
        /// Current number of invading plants in the level.
        /// </summary>
	    public int m_currentNumInvadingPlants; // Number of "bad" plants

        /// <summary>
        /// Maximum number of invading plants in the level, before game over (if currentNumInvadingPlants == this = gameLost)
        /// </summary>
        public int m_maxNumInvadingPlants;
        public GameState m_currentGameState = GameState.Starting;
        // Use this for initialization
        void Start() {
            m_touchManager = new TouchManager(this); // Instantiate a new instance of a touch manager  

            m_tools = new Tool[4];
            
            //TODO: Remove this due to each level using separate tools.
            m_tools[0] = new Bomb();
            m_tools[1] = new Axe(); 
            m_tools[2] = new Flame();
            m_tools[3] = new Mario();

            InitializeVariables();
        }


        // Update is called once per frame
        void Update() {
            UpdateGameState();

            m_touchManager.updateTouch();

            HandleDifficulty();
        }

        // Possibly this has to go to the touch manager.
        public void Swiped(Vector2 swipe) {
            if (m_weaponSelected != 0) { // Weapon was selected
                m_tools[m_weaponSelected].activated();
            }
        }

        /// <summary>
        /// Initializes all stage specific variables.
        /// </summary>
        public abstract void InitializeVariables();

        /// <summary>
        /// Stage checks to see how challenging game is becoming. (might come in handy)
        /// </summary>
        public abstract void HandleDifficulty();

        /// <summary>
        /// Updates all the necessary components in a frame.
        /// </summary>
        public abstract void UpdateGameState();


        // To detect whether gameOver is reached. Varies from stage, override it in manager.
        public bool CheckGameOver() {
            return m_currentNumInvadingPlants == 0;
        }

       	public void SelectWeapon(int value) {
	    	m_weaponSelected = value;
	    }

		public void increaseNumPlants() {
            m_currentNumInvadingPlants += 1;
		}

		public void decreaseNumPlants() {
			m_currentNumInvadingPlants -= 1;
		}
    }
}