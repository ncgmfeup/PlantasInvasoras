using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolNamespace;
using PlantNamespace;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour
    {
        public enum GameState
        {
            Starting,
            Playing,
            Paused,
            GameWon,
            GameLost
        }

        public static StageManager sharedInstance;

        public GameState m_gameState { get; set; }

        public Player m_scenePlayer;

        [SerializeField]
        private int m_maxInvadingPlantsBeforeGameLost;

        private TouchManager touchManager;

        private void Awake()
        {
            if (!sharedInstance)
                sharedInstance = this;
            else
            {
                Debug.LogWarning("Found an extra StageManager in the scene! Destroying GameObject: " + gameObject.name + "...");
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            touchManager = new TouchManager(this); // Instantiate a new instance of a touch manager
            
            //CheckIfPlayerExists();
            InitializeVariables();
        }


        // Update is called once per frame
        void Update() {
            UpdateGameState();

            touchManager.updateTouch();

            HandleDifficulty();
        }

        // To detect the win/loss condition.
        private void CheckGameState()
        {
            if (m_gameState == GameState.Paused || m_gameState == GameState.Starting)
                return;

            int currentlySpawnedInvadingPlants = PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants();

            //If all invading plants in the plant pooler are inactive
            if (currentlySpawnedInvadingPlants == 0)
            {
                m_gameState = GameState.GameWon;
            }
            else if (currentlySpawnedInvadingPlants > m_maxInvadingPlantsBeforeGameLost)
            {
                m_gameState = GameState.GameLost;
            }
        }

        // Initializes all stage specific variables
        public abstract void InitializeVariables();

        // Stage checks to see how challenging game is becoming, might come in handy.
        public abstract void HandleDifficulty();

        // Updates all the necessary components in a frame
        public abstract void UpdateGameState();

        public void touched(Vector2 touch) {
            if (m_scenePlayer.SelectedTool != -1) { // Weapon was selected
                //m_scenePlayer.m_tools[m_scenePlayer.SelectedTool].activated();
            }
            
            if (m_scenePlayer.SelectedTool == 0)
            {
                
            }
            else if (m_scenePlayer.SelectedTool == Utils.AXE_SEL)
            {
                // Axe chopped
            }
            else if (m_scenePlayer.SelectedTool == Utils.FIRE_SEL)
            {
                // Fire burnt
            }
            else if (m_scenePlayer.SelectedTool == Utils.NET_SEL)
            { 
                // Net caught
            }
        }

        public void SpawnInvadingPlant(Vector2 pos)
        {
            PlantObjectPooler.sharedInstance.SpawnInvadingPlantAtPosition(pos);
        }
        
    }
}