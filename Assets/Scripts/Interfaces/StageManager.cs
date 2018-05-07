using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ToolNamespace;
using PlantNamespace;

namespace StateNamespace {
	public abstract class StageManager : MonoBehaviour    {
        public enum GameState {
            Starting, Playing, Paused, GameWon, GameLost
        }

        public static StageManager sharedInstance;

        protected float health;
        protected Slider healthSlider;

        public GameState m_gameState { get; set; }

        public Player m_scenePlayer;

        [SerializeField]
        private int m_maxInvadingPlantsBeforeGameLost;

        private TouchManager touchManager;


        private float m_timeBetweenTaps = 0.5f;
        protected bool canUseTool;

        private void Awake()
        {
            if (!sharedInstance) {
                sharedInstance = this;
            }
            else
            {
                Debug.LogWarning("Found an extra StageManager in the scene! Destroying GameObject: " 
                    + gameObject.name + "...");
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            healthSlider = GameObject.Find("Slider").GetComponent<Slider>();
            touchManager = new TouchManager(this); // Instantiate a new instance of a touch manager
            
            canUseTool = true;

            CheckIfPlayerExists();

            InitializeVariables();
        }

        void CheckIfPlayerExists() {
            GameObject gameManager = GameObject.Find("GameManager");
            m_scenePlayer = gameManager.GetComponent<Player>();
        }


        // Update is called once per frame
        void Update() {
            UpdateGameState();

            touchManager.updateTouch();

            HandleDifficulty();
        }

        // To detect the win/lose condition.
        private void CheckGameState()
        {
            if (m_gameState == GameState.Paused || m_gameState == GameState.Starting)
                return;

            int currentlySpawnedInvadingPlants = 
                PlantObjectPooler.sharedInstance.GetNumberOfActiveInvadingPlants();

            //If all invading plants in the plant pooler are inactive
            if (currentlySpawnedInvadingPlants == 0) {
                m_gameState = GameState.GameWon;
            }
            else if (currentlySpawnedInvadingPlants > m_maxInvadingPlantsBeforeGameLost) {
                m_gameState = GameState.GameLost;
            }
        }

        // Initializes all stage specific variables
        public abstract void InitializeVariables();

        // Stage checks to see how challenging game is becoming, might come in handy.
        public abstract void HandleDifficulty();

        // Updates all the necessary components in a frame
        public abstract void UpdateGameState();
        public void touched(Vector3 touch) {
            if (m_scenePlayer.GetSelectedWeapon() == Utils.BOMB_SEL && canUseTool) {

                canUseTool = false;
                
                GameObject newTool = Instantiate(m_scenePlayer.GetTool(Utils.BOMB_SEL),
                    touch, Quaternion.identity);
                
                newTool.GetComponent<Bomb>().UseTool(touch);

                StartCoroutine("DecreaseTime");
                

                //Play Sound
                //soundManager.playBombSound();
            }
        }
        public abstract void HitSomething(GameObject obj); 

        IEnumerator DecreaseTime() {
            yield return new WaitForSeconds(m_timeBetweenTaps);
            canUseTool = true;
        }

        public void SpawnInvadingPlant(Vector3 pos) {
            PlantObjectPooler.sharedInstance.SpawnInvadingPlantAtPosition(pos);
        }

        public void SpawnNativePlant(Vector3 pos) {
            PlantObjectPooler.sharedInstance.SpawnNativePlantAtPosition(pos);
        }
        
    }
}